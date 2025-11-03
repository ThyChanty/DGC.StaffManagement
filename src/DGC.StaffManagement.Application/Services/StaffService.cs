using DGC.Staff_Management.Domain.Enums;
using DGC.StaffManagement.Application.Features.Staff.Command;
using DGC.StaffManagement.Application.Features.Staff.Commands;
using DGC.StaffManagement.Application.Features.Staff.Queries;
using DGC.StaffManagement.Application.Interfaces;
using DGC.StaffManagement.Domain.Entities;
using DGC.StaffManagement.Shared.Commons;
using DGC.StaffManagement.Shared.Extensions;
using DGC.StaffManagement.Shared.Paging;
using LinqKit;
using System.Linq.Expressions;

namespace DGC.StaffManagement.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StaffService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IPaginate<GetStaffViewModel>>> GetStaffAsync(GetStaffQuery query)
        {
            var paginatedStaff = await _unitOfWork.GetReadOnlyRepositoryAsync<Domain.Entities.Staff>()
                .GetListAsync(
                    predicate: GetStaffPredicate(
                        query),
                    selector: s => new GetStaffViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        BirthDate = s.BirthDate,
                        IsActive = s.IsActive,
                        Gender = s.Gender.ToString(),
                    },
                    index: query.Index,
                    size: query.Size,
                    orderBy: SortExtension<Domain.Entities.Staff>.GetOrderBy(query.OrderBy, query.OrderDir!),
                    cancellationToken: CancellationToken.None);

            return await Result<IPaginate<GetStaffViewModel>>.SuccessAsync(paginatedStaff);
        }

        public async Task<Result<GetStaffViewModel>> GetStaffByIdAsync(int id)
        {
            var staff = await _unitOfWork.GetReadOnlyRepositoryAsync<Domain.Entities.Staff>()
    .FirstOrDefaultAsync(x => x.Id == id);

            if (staff == null)
                return await Result<GetStaffViewModel>.FailAsync("Staff not found");

            // Map Domain Entity to ViewModel
            var staffViewModel = new GetStaffViewModel
            {
                Id = staff.Id,
                Name = staff.Name,
                BirthDate = staff.BirthDate,
                IsActive = staff.IsActive,
                Gender = staff.Gender.ToString()
            };

            return await Result<GetStaffViewModel>.SuccessAsync(staffViewModel);
        }

        public async Task<Result<GetStaffViewModel>> CreateStaffAsync(CreateStaffCommand command)
        {
            var staff = new Domain.Entities.Staff
            {
                Name = command.Name,
                BirthDate = command.BirthDate,
                Gender = (GenderEnum)command.Gender,
                IsActive = true
            };

            var created = await _unitOfWork.GetRepositoryAsync<Domain.Entities.Staff>().InsertAsync(staff);
            await _unitOfWork.CommitAsync();
            var createdStaff = created.Entity;

            var result = new GetStaffViewModel
            {
                Id = createdStaff.Id,
                Name = createdStaff.Name,
                BirthDate = createdStaff.BirthDate,
                IsActive = createdStaff.IsActive,
                Gender = createdStaff.Gender.ToString()
            };

            return await Result<GetStaffViewModel>.SuccessAsync(result);
        }

        public async Task<Result<GetStaffViewModel>> UpdateStaffAsync(int id, UpdateStaffCommand command)
        {
            var staff = await _unitOfWork.GetRepositoryAsync<Domain.Entities.Staff>().FirstOrDefaultAsync(x => x.Id == id);
            if (staff == null)
                return await Result<GetStaffViewModel>.FailAsync("Staff not found");

            staff.Name = command.Name;
            staff.BirthDate = command.BirthDate;
            staff.Gender = (GenderEnum)command.Gender;
            staff.IsActive = command.IsActive;


            await _unitOfWork.GetRepositoryAsync<Domain.Entities.Staff>().InsertAsync(staff);
            await _unitOfWork.CommitAsync();

            var result = new GetStaffViewModel
            {
                Id = staff.Id,
                Name = staff.Name,
                BirthDate = staff.BirthDate,
                IsActive = staff.IsActive,
                Gender = staff.Gender.ToString()
            };

            return await Result<GetStaffViewModel>.SuccessAsync(result);
        }

        public async Task<Result<bool>> DeleteStaffAsync(int id)
        {
            var staff = await _unitOfWork.GetRepositoryAsync<Domain.Entities.Staff>().FirstOrDefaultAsync(x => x.Id == id);
            if (staff == null)
                return await Result<bool>.FailAsync("Staff not found");

            _unitOfWork.DeleteRepository<Domain.Entities.Staff>().Delete(staff);
            await _unitOfWork.CommitAsync();

            return await Result<bool>.SuccessAsync(true);
        }

        private Expression<Func<Domain.Entities.Staff, bool>> GetStaffPredicate(GetStaffQuery request)
        {
            Console.WriteLine($"=== BUILDING PREDICATE ===");
            Console.WriteLine($"StaffId: '{request.StaffId}', Gender: {request.Gender}, FullName: '{request.FullName}'");

            var predicate = PredicateBuilder.New<Domain.Entities.Staff>(true);

            // Filter by StaffId
            if (!string.IsNullOrWhiteSpace(request.StaffId))
            {
                Console.WriteLine($"Applying StaffId filter: '{request.StaffId}'");

                // Try different approaches for StaffId filtering
                if (int.TryParse(request.StaffId, out int staffIdInt))
                {
                    // If it's a valid integer, search by exact ID
                    predicate = predicate.And(x => x.Id == staffIdInt);
                    Console.WriteLine($"StaffId parsed as integer: {staffIdInt}");
                }
                else
                {
                    // If it's not a valid integer, search in Name
                    predicate = predicate.And(x => x.Name.Contains(request.StaffId));
                    Console.WriteLine($"StaffId treated as name search: '{request.StaffId}'");
                }
            }

            // Filter by Gender
            if (request.Gender.HasValue)
            {
                Console.WriteLine($"Applying Gender filter: {request.Gender.Value}");

                // Convert the int to GenderEnum
                try
                {
                    var genderEnum = (GenderEnum)request.Gender.Value;
                    predicate = predicate.And(x => x.Gender == genderEnum);
                    Console.WriteLine($"Gender converted to enum: {genderEnum}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error converting gender: {ex.Message}");
                    // If conversion fails, don't apply the filter
                }
            }

            // Filter by FullName
            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                Console.WriteLine($"Applying FullName filter: '{request.FullName}'");
                predicate = predicate.And(x => x.Name.Contains(request.FullName));
            }

            // Filter by birthday range
            if (request.BirthdayFrom.HasValue)
            {
                var from = request.BirthdayFrom.Value.Date;
                Console.WriteLine($"Applying BirthdayFrom filter: {from}");
                predicate = predicate.And(x => x.BirthDate.Date >= from);
            }

            if (request.BirthdayTo.HasValue)
            {
                var to = request.BirthdayTo.Value.Date;
                Console.WriteLine($"Applying BirthdayTo filter: {to}");
                predicate = predicate.And(x => x.BirthDate.Date <= to);
            }

            Console.WriteLine($"Predicate built successfully");
            return predicate;
        }
    }
    }
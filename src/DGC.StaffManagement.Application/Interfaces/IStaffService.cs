using DGC.StaffManagement.Application.Features.Staff.Command;
using DGC.StaffManagement.Application.Features.Staff.Commands;
using DGC.StaffManagement.Application.Features.Staff.Queries;
using DGC.StaffManagement.Domain.Entities;
using DGC.StaffManagement.Shared.Commons;
using DGC.StaffManagement.Shared.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGC.StaffManagement.Application.Interfaces
{
    public interface IStaffService
    {
        Task<Result<IPaginate<GetStaffViewModel>>> GetStaffAsync(GetStaffQuery query);
        Task<Result<GetStaffViewModel>> GetStaffByIdAsync(int id);
        Task<Result<GetStaffViewModel>> CreateStaffAsync(CreateStaffCommand command);
        Task<Result<GetStaffViewModel>> UpdateStaffAsync(int id,UpdateStaffCommand command);
        Task<Result<bool>> DeleteStaffAsync(int id);
    }
}


using MediatR;

using DGC.StaffManagement.Shared.Commons;
using DGC.StaffManagement.Shared.Paging;

namespace DGC.StaffManagement.Application.Features.Staff.Queries
{
    public class GetStaffQuery: APaginateQuery
    {
        public string? StaffId { get; set; }
        public int? Gender { get; set; }
        public string? FullName { set; get; }
        public DateTime? BirthdayFrom { get; set; }
        public DateTime? BirthdayTo { get; set; }
    }
}

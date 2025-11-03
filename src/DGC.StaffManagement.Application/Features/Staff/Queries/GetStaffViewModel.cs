namespace DGC.StaffManagement.Application.Features.Staff.Queries
{
    public class GetStaffViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }

        public bool IsActive { get; set; }

        public DateTime BirthDate { get; set; }
    }
}

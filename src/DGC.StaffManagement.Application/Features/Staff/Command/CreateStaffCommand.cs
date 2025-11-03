namespace DGC.StaffManagement.Application.Features.Staff.Commands
{
    public class CreateStaffCommand
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
    }
}

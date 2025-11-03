using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGC.StaffManagement.Application.Features.Staff.Command
{
    public class UpdateStaffCommand
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
    }
}

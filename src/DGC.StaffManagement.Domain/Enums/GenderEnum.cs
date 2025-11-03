using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DGC.Staff_Management.Domain.Enums
{
    public enum GenderEnum
    {
        [EnumMember(Value = "MALE")]
        Male = 1,
        [EnumMember(Value = "FEMALE")]
        Female =2
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGC.Staff_Management.Infrastructure
{
    public interface IDefaultDbContextFactory
    {
        ApplicationDbContext CreateContext();
    }
}

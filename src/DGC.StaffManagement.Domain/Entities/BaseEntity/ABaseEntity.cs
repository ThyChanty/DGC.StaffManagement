using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGC.Staff_Management.Domain.Entities.BaseEntity
{
    public abstract class ABaseEntity<T> where T : struct,
        IComparable,
        IComparable<T>,
        IConvertible,
        IEquatable<T>,
        IFormattable
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public virtual T Id { get; set; }
    }
}

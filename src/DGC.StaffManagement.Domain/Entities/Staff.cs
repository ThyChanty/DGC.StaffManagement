using DGC.Staff_Management.Domain.Entities.BaseEntity;
using DGC.Staff_Management.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DGC.StaffManagement.Domain.Entities
{
    public class Staff: ABaseEntity<int>
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required]
        [StringLength(125)]
        public string? Name { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        [Required]
        public GenderEnum Gender { set; get; }

        [Required]
        public bool IsActive { get; set; } = false;
    }
}

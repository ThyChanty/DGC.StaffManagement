using DGC.Staff_Management.Infrastructure.EntityTypeConfigurations;
using DGC.StaffManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace DGC.Staff_Management.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext
    {
        public const string DefaultSchema = "DGC";
        public const string DefaultMigrationsHistoryTable = "__EFMigrationsHistory";


        public DbSet<Staff> Staff { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // Ref: https://github.com/dotnet/efcore/issues/19786
            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(x =>
                x.MigrationsHistoryTable(DefaultMigrationsHistoryTable, DefaultSchema));
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());

        }

    }
}

using Microsoft.EntityFrameworkCore;

namespace Api1.Models
{
    public class ResidentManagementDbContext : DbContext
    {
        public ResidentManagementDbContext(DbContextOptions<ResidentManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<ResidentInfor> Residents { get; set; }
        public DbSet<ApartmentsInfor> Apartments { get; set; }
        public DbSet<ApartmentsOwner> ApartmentsOwners { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ApartmentsOwner>()
        //        .HasKey(ao => new { ao.OwnerId, ao.ApartmentId });

        //    //modelBuilder.Entity<ApartmentsOwner>()
        //    //    .HasOne(ao => ao.Owners)
        //    //    .WithMany(r => r.ApartmentsOwners)
        //    //    .HasForeignKey(ao => ao.OwnerId);

        //    //modelBuilder.Entity<ApartmentsOwner>()
        //    //    .HasOne(ao => ao.Apartments)
        //    //    .WithMany(a => a.ApartmentsOwners)
        //    //    .HasForeignKey(ao => ao.ApartmentId);

        //    modelBuilder.Entity<ResidentInfor>()
        //        .HasMany(r => r.ApartmentsOwners)
        //        .WithOne(ao => ao.Owners)
        //        .HasForeignKey(ao => ao.OwnerId)
        //        .OnDelete(DeleteBehavior.Cascade); // Cascade delete behavior for ResidentInfor

        //    modelBuilder.Entity<ApartmentsInfor>()
        //        .HasMany(a => a.ApartmentsOwners)
        //        .WithOne(ao => ao.Apartments)
        //        .HasForeignKey(ao => ao.ApartmentId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}

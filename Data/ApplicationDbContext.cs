using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLessProject.Models;

namespace QLessProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Counter> Counters { get; set; }

        public DbSet<QueueTicket> QueueTickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Department relationship with QueueTicket
            modelBuilder.Entity<QueueTicket>()
                .HasOne(q => q.Department)
                .WithMany()
                .HasForeignKey(q => q.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Counter relationship with QueueTicket
            modelBuilder.Entity<QueueTicket>()
                .HasOne(q => q.Counter)
                .WithMany()
                .HasForeignKey(q => q.CounterId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
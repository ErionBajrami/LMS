using EcommerceDomain.LeaveAllovations;
using EcommerceDomain.LeaveRequests;
using EcommerceDomain.LeaveTypes;
using EcommercePersistence.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LMS.Application.Interfaces;
using LMS.Domain.Employees;
using LMS.Persistence.Employees;
using EcommerceDomain.Holiday;


namespace EcommercePersistence
{
    public class DatabaseService : DbContext, IDatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public void Save()
        {
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=LifeLmss;Username=postgres;Password=postgres;Include Error Detail=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new EmployeeConfiguration().Configure(builder.Entity<Employee>());
            
            DataSeeder.SeedEmployees(builder);
            SeedLeaveTypes.LeaveTypes(builder);
        }
    }
}

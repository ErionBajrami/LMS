using EcommerceDomain.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace EcommercePersistence.Seeders;

public static class SeedLeaveTypes
{
    public static void LeaveTypes(ModelBuilder builder)
    {
        builder.Entity<LeaveType>()
            .HasData(
                new LeaveType
                {
                    Id = 1,
                    Name = "Annual",
                    DefaultDays = 0,
                    DateCreated = DateTime.UtcNow
                },
                new LeaveType
                {
                    Id = 2,
                    Name = "Sick",
                    DefaultDays = 20,
                    DateCreated = DateTime.UtcNow
                },
                new LeaveType
                {
                    Id = 3,
                    Name = "Replacement",
                    DateCreated = DateTime.UtcNow
                },
                new LeaveType
                {
                    Id = 4,
                    Name = "Unpaid",
                    DefaultDays = 10,
                    DateCreated = DateTime.UtcNow
                }
            );
    }
}

// [Key]
// public int Id { get; set; }
// public string Name { get; set; }
// public int DefaultDays { get; set; }
// public DateTime DateCreated { get; set; }
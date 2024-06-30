using LMS.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace EcommercePersistence.Seeders;

public static class DataSeeder
{
    public static void SeedEmployees(ModelBuilder DataSeeder)
    {
        DataSeeder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                Firstname = "John",
                Lastname = "Doe",
                Position = "HR",
                ReportsTo = null,
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1990, 2, 2), DateTimeKind.Utc),
                DateJoined = DateTime.SpecifyKind(new DateTime(2010, 5, 1), DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 2,
                Firstname = "Jane",
                Lastname = "Smith",
                Position = "Developer",
                ReportsTo = "3",
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1990, 2, 2), DateTimeKind.Utc),
                DateJoined = DateTime.SpecifyKind(new DateTime(2015, 6, 15), DateTimeKind.Utc)
            },
             new Employee
             {
                 Id = 3,
                 Firstname = "Jane",
                 Lastname = "Smith",
                 Position = "Lead",
                 ReportsTo = "1",
                 DateOfBirth = DateTime.SpecifyKind(new DateTime(1555, 2, 2), DateTimeKind.Utc),
                 DateJoined = DateTime.SpecifyKind(new DateTime(2000, 6, 15), DateTimeKind.Utc)
             }
        // Add more employees as needed
        );
    }
}
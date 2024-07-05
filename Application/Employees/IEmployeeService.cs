using EcommerceApplication.Employees;
using EcommerceDomain.LeaveAllovations;
using LMS.Domain.Employees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task CreateEmployee(EmployeeCreateDto employeeCreateDto);

        Task<List<LeaveAllocation>> getAllAllocationsByUserId(int id);
    }
}

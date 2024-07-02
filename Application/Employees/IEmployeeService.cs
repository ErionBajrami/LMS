using EcommerceApplication.Employees;
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
        Task UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto);
        Task DeleteEmployee(int id);
    }
}

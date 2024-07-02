using EcommerceDomain.LeaveAllovations;
using EcommerceDomain.LeaveTypes;
using LMS.Application.Interfaces;
using LMS.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _unitOfWork.Repository<Employee>().GetAll().ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _unitOfWork.Repository<Employee>().GetById(id).FirstOrDefaultAsync();
        }

        public async Task CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var employee = new Employee
            {
                Firstname = employeeCreateDto.Firstname,
                Lastname = employeeCreateDto.Lastname,
                Email = employeeCreateDto.Email,
                Position = employeeCreateDto.Position,
                ReportsTo = employeeCreateDto.ReportsTo,
                DateOfBirth = employeeCreateDto.DateOfBirth,
                DateJoined = DateTime.UtcNow
            };

            _unitOfWork.Repository<Employee>().Create(employee);
            _unitOfWork.Complete();

            await InitializeLeaveAllocations(employee.Id);

            _unitOfWork.Complete();
        }

        private async Task InitializeLeaveAllocations(int employeeId)
        {
            var leaveTypes = _unitOfWork.Repository<LeaveType>().GetAll();

            foreach (var leaveType in leaveTypes)
            {
                var allocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    DateCreated = DateTime.UtcNow,
                    Period = DateTime.UtcNow.Year
                };

                if (leaveType.Name == "Annual" || leaveType.Name == "Replacement")
                {
                    allocation.NumberOfDays = 0;
                }
                else if (leaveType.Name == "Sick")
                {
                    allocation.NumberOfDays = 20;
                }
                else
                {
                    allocation.NumberOfDays = 10;
                }

                _unitOfWork.Repository<LeaveAllocation>().Create(allocation);
            }
        }
    }
}

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

        public async Task<List<LeaveAllocation>> getAllAllocationsByUserId(int id)
        {
            var employee = _unitOfWork.Repository<Employee>().GetById(x => x.Id == id).FirstOrDefault();

            var allocations = _unitOfWork.Repository<LeaveAllocation>()
                                .GetByCondition(a => a.EmployeeId == id)
                                .ToList();
            return allocations;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _unitOfWork.Repository<Employee>().GetById(x => x.Id == id).FirstOrDefaultAsync();
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
                    DateCreated = DateTime.UtcNow,
                    Period = DateTime.UtcNow.Year
                };

                switch (leaveType.Name)
                {
                    case "Annual":
                    case "Replacement":
                        allocation.NumberOfDays = 0;
                        break;
                    case "Sick":
                        allocation.NumberOfDays = 20;
                        break;
                    default:
                        allocation.NumberOfDays = 10;
                        break;
                }

                _unitOfWork.Repository<LeaveAllocation>().Create(allocation);
            }

            _unitOfWork.Complete(); // Save changes to the database
        }
    }
}

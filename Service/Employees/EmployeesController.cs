using EcommerceApplication.Employees;
using LMS.Application.Interfaces;
using LMS.Domain.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Service.Employees
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("allocs")]
        public async Task<IActionResult> getAllAllocations(int id)
        {
            var allocs = await _employeeService.getAllAllocationsByUserId(id);
            return Ok(allocs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
            await _employeeService.CreateEmployee(employeeCreateDto);
            return Ok();
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        //{
        //    await _employeeService.UpdateEmployee(id, employeeUpdateDto);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmployee(int id)
        //{
        //    await _employeeService.DeleteEmployee(id);
        //    return NoContent();
        //}
    }
}

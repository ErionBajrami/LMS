using Castle.Core.Smtp;
using EcommerceApplication.CreateLeaveRequest;
using EcommerceDomain.Holiday;
using EcommerceDomain.LeaveAllovations;
using EcommerceDomain.LeaveRequests;
using EcommerceDomain.LeaveTypes;
using LMS.Application.Interfaces;
using LMS.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.LeaveRequestService;

public class LeaveRequestServiceService : ILeaveRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IEmailSender _emailSender;

    public LeaveRequestServiceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void CreateLeaveRequest(LeaveRequestDTO leaveRequestDTO)
    {
        try
        {
            var employee = _unitOfWork.Repository<Employee>()
                .GetById(x => x.Id == leaveRequestDTO.RequestingEmployeeId)
                .FirstOrDefault();

            if (employee == null)
                throw new Exception("Employee doesn't exist");

            var leaveType = _unitOfWork.Repository<LeaveType>()
                .GetById(x => x.Id == leaveRequestDTO.leaveTypeId);

            if (leaveType == null)
                throw new Exception("LeaveType doesn't exist");

            var totalDays = (leaveRequestDTO.endDate - leaveRequestDTO.startDate).Days + 1;

            var holidays = _unitOfWork.Repository<Holiday>()
                .GetByCondition(x => x.Date >= leaveRequestDTO.startDate && x.Date <= leaveRequestDTO.endDate)
                .Select(x => x.Date)
                .ToList();

            var actualLeaveDays = HelperMethods.CalculateLeaveDaysExcludingHolidays(leaveRequestDTO.startDate, leaveRequestDTO.endDate, holidays);

            var allocation = _unitOfWork.Repository<LeaveAllocation>()
                .GetByCondition(x => x.EmployeeId == leaveRequestDTO.RequestingEmployeeId && x.LeaveTypeId == leaveRequestDTO.leaveTypeId)
                .FirstOrDefault();

            if (allocation == null || allocation.NumberOfDays < actualLeaveDays)
                throw new Exception("Insufficient Leave Days");

            allocation.NumberOfDays -= actualLeaveDays;



            var leaveRequest = new LeaveRequest
            {
                RequestingEmployeeId = leaveRequestDTO.RequestingEmployeeId,
                LeaveTypeId = leaveRequestDTO.leaveTypeId,
                StartDate = leaveRequestDTO.startDate,
                EndDate = leaveRequestDTO.endDate,
                DateRequested = DateTime.UtcNow,
                DateActioned = DateTime.UtcNow,
                RequestComments = leaveRequestDTO.requestComments,
                ApprovedById = employee.ReportsTo,
                Approved = null,
                Cancelled = false
            };

            _unitOfWork.Repository<LeaveAllocation>().Update(allocation);

            _unitOfWork.Complete();


            _unitOfWork.Repository<LeaveRequest>().Create(leaveRequest);
            _unitOfWork.Complete();

            // Send email notification
            // var lead = _unitOfWork.Repository<Employee>().GetById(employee.ReportsTo);
            // if (lead != null)
            // {
            //     var emailBody = $"{employee.Firstname} {employee.Lastname} has requested {actualLeaveDays} days off from {leaveRequestDTO.startDate.ToShortDateString()} to {leaveRequestDTO.endDate.ToShortDateString()}.";
            //     _emailSender.Send(lead.Email, "Leave Request", emailBody);
            // }
        }
        catch (Exception ex)
        {
            // Log exception details
            Console.WriteLine($"Error: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            throw; // Rethrow the exception to be caught by the calling method
        }
    }


    public IEnumerable<LeaveRequest> GetLeaveRequests()
    {
        return _unitOfWork.Repository<LeaveRequest>().GetAll();
    }

    public IEnumerable<LeaveRequest> GetUserLeaveRequests(int employeeId)
    {
        var LeaveRequests = _unitOfWork.Repository<LeaveRequest>()
            .GetById(x => x.RequestingEmployeeId == employeeId)
            .ToList();

        return LeaveRequests;
    }
}
using Castle.Core.Smtp;
using EcommerceApplication.CreateLeaveRequest;
using EcommerceDomain.LeaveAllovations;
using EcommerceDomain.LeaveRequests;
using EcommerceDomain.LeaveTypes;
using LMS.Application.Interfaces;
using LMS.Domain.Employees;

namespace EcommerceApplication.LeaveRequestService;

public class LeaveRequestServiceService : ILeaveRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    
    public LeaveRequestServiceService(IUnitOfWork unitOfWork, IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
    }

    public void CreateLeaveRequest(string employeeId, int leaveTypeId, DateTime startDate, DateTime endDate, string requestComments)
    {
        var employee = _unitOfWork.Repository<Employee>()
            .GetById(x => x.Id == employeeId)
            .FirstOrDefault();

        var leaveType = _unitOfWork.Repository<LeaveType>()
            .GetById(x => x.Id == leaveTypeId);

        var allocation = _unitOfWork.Repository<LeaveAllocation>()
            .GetByCondition(x => x.EmployeeId == employeeId && x.LeaveTypeId == leaveTypeId)
            .FirstOrDefault();

        if (allocation == null || allocation.NumberOfDays < (endDate - startDate).Days)
            throw new Exception("Insufficient Leave Days");

        allocation.NumberOfDays -= (endDate - startDate).Days;
        
        var leaveRequest = new EcommerceDomain.LeaveRequests.LeaveRequest
        {
            RequestingEmployeeId = employeeId,
            LeaveTypeId = leaveTypeId,
            StartDate = startDate,
            EndDate = endDate,
            DateRequested = DateTime.Now,
            RequestComments = requestComments,
            Cancelled = false // maybe i should change
        };
        
        //add background job
        
        _unitOfWork.Repository<LeaveRequest>().Create(leaveRequest);
        _unitOfWork.Complete();


        var lead = _unitOfWork.Repository<Employee>().Equals(employee.ReportsTo);

        if (lead != null)
        {
            var emailBody = $"{employee.Firstname} {employee.Lastname} " +
                            $"has requested {(endDate - startDate).Days} days off from " +
                            $"{startDate.ToShortDateString()} to " +
                            $"{endDate.ToShortDateString()}.";
            //await _emailService.SendEmailAsync(lead.Email, "Leave Request", emailBody);
            
           // _emailSender.Send(lead.Email, "Leave Request", emailBody);
        }
    }
}
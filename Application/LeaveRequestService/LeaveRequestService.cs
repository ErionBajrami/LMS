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

    public void CreateLeaveRequest(LeaveRequestDTO leaveRequestDTO)
    {
        var employee = _unitOfWork.Repository<Employee>()
            .GetById(x => x.Id == leaveRequestDTO.employeeId)
            .FirstOrDefault();

        var leaveType = _unitOfWork.Repository<LeaveType>()
            .GetById(x => x.Id == leaveRequestDTO.leaveTypeId);

        var allocation = _unitOfWork.Repository<LeaveAllocation>()
            .GetByCondition(x => x.EmployeeId == leaveRequestDTO.employeeId && x.LeaveTypeId == leaveRequestDTO.leaveTypeId)
            .FirstOrDefault();

        if (allocation == null || allocation.NumberOfDays < (leaveRequestDTO.endDate - leaveRequestDTO.startDate).Days)
            throw new Exception("Insufficient Leave Days");

        var daysOff = (leaveRequestDTO.endDate - leaveRequestDTO.startDate).Days;

        allocation.NumberOfDays -= daysOff;
        
        var leaveRequest = new EcommerceDomain.LeaveRequests.LeaveRequest
        {
            RequestingEmployeeId = leaveRequestDTO.employeeId,
            LeaveTypeId = leaveRequestDTO.leaveTypeId,
            StartDate = leaveRequestDTO.startDate,
            EndDate = leaveRequestDTO.endDate,
            DateRequested = DateTime.Now,
            RequestComments = leaveRequestDTO.requestComments,
            Cancelled = false // maybe i should change
        };
        
        //add background job
        
        _unitOfWork.Repository<LeaveRequest>().Create(leaveRequest);
        _unitOfWork.Complete();


        var lead = _unitOfWork.Repository<Employee>().Equals(employee.ReportsTo);

        if (lead != null)
        {
            var emailBody = $"{employee.Firstname} {employee.Lastname} " +
                            $"has requested {daysOff} days off from " +
                            $"{leaveRequestDTO.startDate.ToShortDateString()} to " +
                            $"{leaveRequestDTO.endDate.ToShortDateString()}.";
            //await _emailService.SendEmailAsync(lead.Email, "Leave Request", emailBody);
            
           // _emailSender.Send(lead.Email, "Leave Request", emailBody);
        }
    }

    public IEnumerable<LeaveRequest> GetLeaveRequests()
    {
        return _unitOfWork.Repository<LeaveRequest>().GetAll();
    }
}
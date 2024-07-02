using Castle.Core.Smtp;
using EcommerceApplication.CreateLeaveRequest;
using EcommerceDomain.Holiday;
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

        if (employee == null)
            throw new Exception("Employee doesnt exist");



        var leaveType = _unitOfWork.Repository<LeaveType>()
            .GetById(x => x.Id == leaveRequestDTO.leaveTypeId);

        if (leaveType == null)
            throw new Exception("LeaveType doesnt exist");



        var totalDays = (leaveRequestDTO.endDate - leaveRequestDTO.startDate).Days + 1;

        var holidays = _unitOfWork.Repository<Holiday>()
            .GetByCondition(x => x.Date >= leaveRequestDTO.startDate && x.Date <= leaveRequestDTO.endDate)
            .Select(x => x.Date)
            .ToList();

        var actualLeaveDays = HelperMethods.CalculateLeaveDaysExcludingHolidays(leaveRequestDTO.startDate, leaveRequestDTO.endDate, holidays);


        var allocation = _unitOfWork.Repository<LeaveAllocation>()
            .GetByCondition(x => x.EmployeeId == leaveRequestDTO.employeeId && x.LeaveTypeId == leaveRequestDTO.leaveTypeId)
            .FirstOrDefault();

        if (allocation == null || allocation.NumberOfDays < actualLeaveDays)
            throw new Exception("Insufficient Leave Days");



        allocation.NumberOfDays -= actualLeaveDays;

        var leaveRequest = new EcommerceDomain.LeaveRequests.LeaveRequest
        {
            RequestingEmployeeId = leaveRequestDTO.employeeId,
            LeaveTypeId = leaveRequestDTO.leaveTypeId,
            StartDate = leaveRequestDTO.startDate,
            EndDate = leaveRequestDTO.endDate,
            DateRequested = DateTime.Now,
            RequestComments = leaveRequestDTO.requestComments,
            Cancelled = false
        };


        _unitOfWork.Repository<LeaveRequest>().Create(leaveRequest);
        _unitOfWork.Complete();

        var lead = _unitOfWork.Repository<Employee>().Equals(employee.ReportsTo);

        if (lead != null)
        {
            var emailBody = $"{employee.Firstname} {employee.Lastname} " +
                            $"has requested {actualLeaveDays} days off from " +
                            $"{leaveRequestDTO.startDate.ToShortDateString()} to " +
                            $"{leaveRequestDTO.endDate.ToShortDateString()}.";

            _emailSender.Send(lead.Email, "Leave Request", emailBody);
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
using EcommerceApplication.LeaveRequestService;
using EcommerceDomain.LeaveRequests;

namespace EcommerceApplication.CreateLeaveRequest;

public interface ILeaveRequestService
{
    IEnumerable<LeaveRequest> GetLeaveRequests();
    IEnumerable<LeaveRequest> GetUserLeaveRequests(int employeeId);
    void CreateLeaveRequest(LeaveRequestDTO leaveRequestDTO);

}
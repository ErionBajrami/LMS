using EcommerceApplication.LeaveRequestService;
using EcommerceDomain.LeaveRequests;

namespace EcommerceApplication.CreateLeaveRequest;

public interface ILeaveRequestService
{
    IEnumerable<LeaveRequest> GetLeaveRequests();
    void CreateLeaveRequest(LeaveRequestDTO leaveRequestDTO);
}
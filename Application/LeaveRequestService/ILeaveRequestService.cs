namespace EcommerceApplication.CreateLeaveRequest;

public interface ILeaveRequestService
{
    void CreateLeaveRequest(string employeeId, int leaveTypeId, DateTime startDate, DateTime endDate, string requestComments);
}
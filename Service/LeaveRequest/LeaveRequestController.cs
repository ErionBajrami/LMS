using EcommerceApplication.CreateLeaveRequest;
using EcommerceApplication.LeaveRequestService;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceService.LeaveRequest
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveRequestApprovalService _leaveRequestApprovalService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, ILeaveRequestApprovalService leaveRequestApprovalService)
        {
            _leaveRequestService = leaveRequestService;
            _leaveRequestApprovalService = leaveRequestApprovalService;
        }

        //[Authorize(Roles = "Lead, HR")]
        [HttpGet("GetAllLeaveRequests")]
        public IActionResult GetAll()
        { 
            var requests = _leaveRequestService.GetLeaveRequests().ToList();

            return Ok(requests);
        }

        //[Authorize(Roles = "Employee")]
        [HttpGet("GetYourRequests")]
        public IActionResult GetUserRequests(int id)
        {
            var requests = _leaveRequestService.GetUserLeaveRequests(id);

            return Ok(requests);
        }

        [HttpPost("CreateLeaveRequest")]
        public IActionResult CreateRequest(LeaveRequestDTO leaveRequestDTO) 
       {

            _leaveRequestService.CreateLeaveRequest(leaveRequestDTO);
return Ok("Leave Request Created");
        }

        [HttpPost("approve")]
       // [Authorize(Roles = "Lead, HR")]
        public async Task<IActionResult> ApproveLeaveRequest([FromBody] ApproveLeaveRequestDto dto)
        {
            _leaveRequestApprovalService.ApproveRequest(dto.RequestId, dto.ApproverId);
            return Ok("Leave request Approved successfully.");
        }


        [HttpPost("cancel")]
        //[Authorize(Roles = "Lead, HR")]
        public async Task<IActionResult> CancelLeaveRequest([FromBody] DeclineRequestDTO dto)
        {
            _leaveRequestApprovalService.DeclineRequest(dto.EmployeeId);
            return Ok("Leave request cancelled successfully.");
        }
    }
}

using EcommerceApplication.CreateLeaveRequest;
using EcommerceApplication.LeaveRequestService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceService.LeaveRequest
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveRequestApprovalService _leaveRequestApprovalService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, ILeaveRequestApprovalService leaveRequestApprovalService)
        {
            _leaveRequestService = leaveRequestService;
            _leaveRequestApprovalService = leaveRequestApprovalService;
        }

        [HttpGet("GetAllLeaveRequests")]
        public IActionResult GetAll()
        { 
            var requests = _leaveRequestService.GetLeaveRequests().ToList();

            return Ok(requests);
        }

        [HttpPost("CreateLeaveRequest")]
        public IActionResult CreateRequest(LeaveRequestDTO leaveRequestDTO) 
        {
            _leaveRequestService.CreateLeaveRequest(leaveRequestDTO);

            return Ok("Leave Request Created");
        }
    }
}

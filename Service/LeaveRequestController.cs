using EcommerceApplication.CreateLeaveRequest;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceService;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequest;

    public LeaveRequestController(ILeaveRequestService leaveRequest)
    {
        _leaveRequest = leaveRequest;
    }

    [HttpPost("MakeRequest")]
    public async Task<IActionResult> CreateRequest()
    {
        
    }
}
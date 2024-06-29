using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.LeaveRequestService
{
    public interface ILeaveRequestApprovalService
    {
        void ApproveRequest(int requestId);
    }
}

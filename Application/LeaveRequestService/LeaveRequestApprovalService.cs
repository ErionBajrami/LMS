using EcommerceDomain.LeaveRequests;
using LMS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.LeaveRequestService
{
    public class LeaveRequestApprovalService : ILeaveRequestApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveRequestApprovalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ApproveRequest(int requestId, int approverId)
        {
            var leaveRequest = _unitOfWork.Repository<LeaveRequest>()
                .GetById(x => x.Id == requestId).FirstOrDefault();

            if (leaveRequest == null)
                return;

            leaveRequest.Approved = true;
            leaveRequest.ApprovedById = approverId;
            leaveRequest.DateActioned = DateTime.UtcNow;

            _unitOfWork.Repository<LeaveRequest>().Update(leaveRequest);
            _unitOfWork.Complete();
        }

        public void DeclineRequest(int requestId)
        {
            var leaveRequest = _unitOfWork.Repository<LeaveRequest>()
                .GetById(x => x.Id == requestId).FirstOrDefault();

            if (leaveRequest == null)
                return;

            leaveRequest.Cancelled = true;
            leaveRequest.DateActioned = DateTime.UtcNow;

            _unitOfWork.Repository<LeaveRequest>().Update(leaveRequest);
            _unitOfWork.Complete();
        }
    }
}

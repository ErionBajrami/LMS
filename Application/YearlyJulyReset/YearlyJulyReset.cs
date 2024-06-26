using EcommerceDomain.LeaveAllovations;
using EcommerceDomain.LeaveTypes;
using LMS.Application.Interfaces;

namespace EcommerceApplication;

public class YearlyJulyReset : IYearlyJulyReset
{
    private readonly IUnitOfWork _unitOfWork;

    public YearlyJulyReset(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public void ResetAllLeaveDays()
    {
        var leaveDays = _unitOfWork.Repository<LeaveAllocation>()
            .GetAll()
            .ToList();


        foreach (var LeaveDays in leaveDays)
        {
            LeaveDays.NumberOfDays = 0;
            _unitOfWork.Repository<LeaveAllocation>().Update(LeaveDays);
        }

        _unitOfWork.Complete();
    }

    public void ResetAnnualLeaveDays()
    {
        var leaveType = _unitOfWork.Repository<LeaveType>()
            .GetByCondition(x => x.Id == 1);

        if (leaveType == null)
            throw new Exception("Leave type doesnt exist");


        var leaveAllocations = _unitOfWork.Repository<LeaveAllocation>()
            .GetByCondition(x => x.LeaveType == leaveType)
            .ToList();

        foreach (var allocation in leaveAllocations)
        {
            allocation.NumberOfDays = 0;
            _unitOfWork.Repository<LeaveAllocation>().Update(allocation);
        }

        _unitOfWork.Complete();
    }
}
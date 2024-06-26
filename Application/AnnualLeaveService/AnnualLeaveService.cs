using EcommerceDomain.LeaveAllovations;
using EcommerceDomain.LeaveTypes;
using LMS.Application.Interfaces;
using LMS.Domain.Employees;

namespace EcommerceApplication.AnnualLeaveService;

public class AnnualLeaveService : IAnnualLeaveService
{
    private readonly IUnitOfWork _unitOfWork;

    public AnnualLeaveService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public void UpdateAnnualLeaveMonthly()
    {
        var AnnualType = _unitOfWork.Repository<LeaveType>()
            .GetById(x => x.Id == 1)
            .FirstOrDefault();

        if (AnnualType == null)
            throw new Exception("Leave type doesnt exist");

        var leaveAllocations = _unitOfWork.Repository<LeaveAllocation>()
            .GetByCondition(x => x.LeaveType == AnnualType)
            .ToList();

        foreach (var employee in leaveAllocations)
        {
            employee.NumberOfDays += 2;
            _unitOfWork.Repository<LeaveAllocation>().Update(employee);
        }

        _unitOfWork.Complete();
    }
}
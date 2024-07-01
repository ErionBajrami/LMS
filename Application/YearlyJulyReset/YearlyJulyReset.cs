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

    public void ResetAnnualLeaveDays()
    {
        // Fetch all leave types
        var leaveTypes = _unitOfWork.Repository<LeaveType>().GetAll().ToList();

        if (!leaveTypes.Any())
            throw new Exception("No leave types exist");

        // Fetch all leave allocations
        var leaveAllocations = _unitOfWork.Repository<LeaveAllocation>().GetAll().ToList();

        foreach (var allocation in leaveAllocations)
        {
            switch (allocation.LeaveType.Name)
            {
                case "Sick":
                    allocation.NumberOfDays = 20;
                    break;
                case "Unpaid":
                    allocation.NumberOfDays = 10;
                    break;
                case "Replacement":
                    allocation.NumberOfDays = 0;
                    break;
                case "Annual":
                    if (allocation.NumberOfDays > 12)
                    {
                        allocation.NumberOfDays = 12;
                    }
                    // if 12 days or fewer, they are continued without changes
                    break;
            }

            _unitOfWork.Repository<LeaveAllocation>().Update(allocation);
        }

        _unitOfWork.Complete();
    }
}

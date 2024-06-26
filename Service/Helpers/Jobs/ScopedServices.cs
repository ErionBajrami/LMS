using EcommerceApplication;
using EcommerceApplication.AnnualLeaveService;

namespace EcommerceService.Helpers;

public static class ScopedServices
{
    public static IServiceProvider ServiceProvider { get; set; }
    
    public static void UpdateAnnualLeaveDays()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var annualLeaveService = scope.ServiceProvider.GetRequiredService<AnnualLeaveService>();
            annualLeaveService.UpdateAnnualLeaveMonthly();
        }
    }

    public static void ResetYearlyAnnualDays()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var YearlyReset = scope.ServiceProvider.GetRequiredService<YearlyJulyReset>();
            YearlyReset.ResetAnnualLeaveDays();
        }
    }
}
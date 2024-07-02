using EcommerceApplication;
using EcommerceApplication.AnnualLeaveService;
using Hangfire;

namespace EcommerceService.Helpers;

public static class Jobs
{
    public static void RecurringJobs()
    {
        RecurringJob.AddOrUpdate(
            "UpdateAnnualLeave",
            () => ScopedServices.UpdateAnnualLeaveDays(),
            Cron.Monthly(1));

        RecurringJob.AddOrUpdate(
            "ResetYearlyAnnualDays",
            () => ScopedServices.ResetYearlyAnnualDays(),
            Cron.Yearly(7, 1)
        );
    }
}
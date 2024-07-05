//using EcommerceApplication;
//using EcommerceApplication.AnnualLeaveService;
//using Hangfire;

//namespace EcommerceService.Helpers;

//public static class Jobs
//{
//    public static void RecurringJobs()
//    {
//        RecurringJob.AddOrUpdate<IYearlyJulyReset>("ResetYearlyAnnualDays", x => x.ResetAnnualLeaveDays(), Cron.Yearly(7, 1));

//        RecurringJob.AddOrUpdate<IAnnualLeaveService>("AddAnnualLeaveDays", x => x.UpdateAnnualLeaveMonthly(), Cron.Monthly(1));
//    }
//}
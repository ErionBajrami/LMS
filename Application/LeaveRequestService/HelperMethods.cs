using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.LeaveRequestService
{
    public class HelperMethods
    {
        public static int CalculateLeaveDaysExcludingHolidays(DateTime startDate, DateTime endDate, List<DateTime> holidays)
        {
            int totalDays = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !holidays.Contains(date))
                {
                    totalDays++;
                }
            }
            return totalDays;
        }
    }
}

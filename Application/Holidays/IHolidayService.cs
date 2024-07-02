using EcommerceDomain.Holiday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Holidays
{
    public interface IHolidayService
    {
        IEnumerable<Holiday> GetAllHolidays();
        Holiday GetHoliday(int id);
        void AddHoliday(HolidayRequestDTO holidayCreateDTO);
        void RemoveHoliday(int id);
        void UpdateHoliday(int id, HolidayRequestDTO updateHolidayDTO);
    }
}

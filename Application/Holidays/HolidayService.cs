using EcommerceDomain.Holiday;
using LMS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Holidays
{
    public class HolidayService : IHolidayService
    {

        private readonly IUnitOfWork _unitOfWork;

        public HolidayService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddHoliday(HolidayRequestDTO holidayRequestDTO)
        {
            var newHoliday = new Holiday
            {
                Date = holidayRequestDTO.Date,
                Name = holidayRequestDTO.Name
            };

            _unitOfWork.Repository<Holiday>().Create(newHoliday);
            _unitOfWork.Complete();

        }

        public IEnumerable<Holiday> GetAllHolidays()
        {
            return _unitOfWork.Repository<Holiday>().GetAll();
        }

        public Holiday GetHoliday(int id)
        {
            var holiday = _unitOfWork.Repository<Holiday>().GetById(x => x.Id == id).FirstOrDefault();

            if (holiday == null)
                throw new Exception("Holiday not found");

            return holiday;
        }

        public void RemoveHoliday(int id)
        {
            var holidayToRemove = _unitOfWork.Repository<Holiday>().GetById(x => x.Id == id).FirstOrDefault();

            if (holidayToRemove == null)
                throw new Exception("Holiday doesn't exist");

            _unitOfWork.Repository<Holiday>().Delete(holidayToRemove);
            _unitOfWork.Complete();
        }

        public void UpdateHoliday(int id, HolidayRequestDTO updateHoliday)
        {
            var holidayToUpdate = _unitOfWork.Repository<Holiday>().GetById(x => x.Id == id).FirstOrDefault();

            holidayToUpdate.Date = updateHoliday.Date;
            holidayToUpdate.Name = updateHoliday.Name;

            _unitOfWork.Repository<Holiday>().Update(holidayToUpdate);
            _unitOfWork.Complete();

        }
    }
}

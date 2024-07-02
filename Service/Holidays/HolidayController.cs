using EcommerceApplication.Holidays;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HolidaysController : ControllerBase
{
    private readonly IHolidayService _holidayService;

    public HolidaysController(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    [HttpGet]
    public IActionResult GetAllHolidays()
    {
        var holidays = _holidayService.GetAllHolidays();
        return Ok(holidays);
    }

    [HttpGet("{id}")]
    public IActionResult GetHoliday(int id)
    {
        try
        {
            var holiday = _holidayService.GetHoliday(id);
            return Ok(holiday);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "HR")]
    public IActionResult AddHoliday([FromBody] HolidayRequestDTO holidayDto)
    {
        _holidayService.AddHoliday(holidayDto);
       return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "HR")]
    public IActionResult UpdateHoliday(int id, [FromBody] HolidayRequestDTO holidayDto)
    {
        try
        {
            _holidayService.UpdateHoliday(id, holidayDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "HR")]
    public IActionResult DeleteHoliday(int id)
    {
        try
        {
            _holidayService.RemoveHoliday(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}

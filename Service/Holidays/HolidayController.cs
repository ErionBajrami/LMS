using EcommerceApplication.Holidays;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HolidaysController : ControllerBase
{
    private readonly IHolidayService _holidayService;

    public HolidaysController(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    [HttpGet("get")]
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

    [HttpPost("add")]
    public IActionResult AddHoliday([FromBody] HolidayRequestDTO holidayDto)
    {
        _holidayService.AddHoliday(holidayDto);
       return Ok();
    }

    [HttpPut("{id}")]
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

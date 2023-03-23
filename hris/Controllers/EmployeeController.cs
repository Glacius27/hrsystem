using Microsoft.AspNetCore.Mvc;

namespace hris.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    //private static readonly string[] Summaries = new[]
    //{
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(ILogger<EmployeeController> logger)
    {
        _logger = logger;
    }
    /*тут должен быть методы замещения штатной единицы
     * метод приема на работу (создания сотрудника)*/



    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Create()
    {
        //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //{
        //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //    TemperatureC = Random.Shared.Next(-20, 55),
        //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //})
        //.ToArray();
        return Ok();
    }
    
}


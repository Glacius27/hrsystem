using Microsoft.AspNetCore.Mvc;
using hris.Logic;
using hris.Models;

namespace hris.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    
    private readonly ILogger<EmployeeController> _logger;
    private HrIsService _hrIsService;

    public EmployeeController(ILogger<EmployeeController> logger, HrIsService hrIsService)
    {
        _logger = logger;
        _hrIsService = hrIsService;
    }

  
    [HttpPut]
    [Route("/Employee/Vacant")]
    public async Task<IActionResult> SetPositionVacant(string positionID)
    {
        await _hrIsService.SetPositionVacant(positionID);
        return Ok();
    }


    //[HttpPost]
    //[Route("/Employee")]
    //public async Task<IActionResult> CreateEmployee(Employee employee)
    //{
    //    await _hrIsService.CreateEmployee(employee);
    //    return Ok();
    //}
}


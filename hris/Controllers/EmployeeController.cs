using Microsoft.AspNetCore.Mvc;
using hris.Logic;
using hris.Models;
using shraredclasses.Models;

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


    [HttpPost]
    [Route("/Employee/Vacant")]
    public async Task<IActionResult> CreateVacantPositionID()
    {
        var result = await _hrIsService.CreateVacantPositionRequest();
        return Ok(new WsResponse() { Data = result});
    }


    [HttpPut]
    [Route("/Employee/Vacant/{positionID}")]
    public async Task<IActionResult> SetPositionIDVacant(string createVacantPositionRequestId, string positionID)
    {
        await _hrIsService.SetPositionVacant(createVacantPositionRequestId, positionID);
        return Ok();
    }


    [HttpGet]
    [Route("/Employee/Vacancy/{positionID}")]
    public async Task<IActionResult> GetVacancyID(string positionID)
    {
        var response = await _hrIsService.GetVacancyID(positionID);
        return Ok(new WsResponse() { Data = response});
    }


    //[HttpPost]
    //[Route("/Employee")]
    //public async Task<IActionResult> CreateEmployee(Employee employee)
    //{
    //    await _hrIsService.CreateEmployee(employee);
    //    return Ok();
    //}
}


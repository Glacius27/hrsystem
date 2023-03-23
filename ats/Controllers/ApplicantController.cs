using ats.Models;
using Microsoft.AspNetCore.Mvc;

namespace ats.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicantController : ControllerBase
{

    // в контроллере должны быть методы отлика на вакансию (ее идентификатор)
    // метод заполнения анкеты
    // методы принятия оффера и отказа от него
    // 
    private readonly ILogger<ApplicantController> _logger;

    public ApplicantController(ILogger<ApplicantController> logger)
    {
        _logger = logger;
    }

   
    [HttpPut]
    public async IActionResult SetQuestionare()
    {
        return Ok();
    }

    [HttpPut]
    public async IActionResult VacancyResponse(string vacancyId, Applicant applicant)
    {
        return Ok();
    }
    [HttpPut]
    public async IActionResult AcceptOffer(string offerId)
    {
        return Ok();
    }
    [HttpPut]
    public async IActionResult RefuseOffer(string offerId)
    {
        return Ok();
    }


}


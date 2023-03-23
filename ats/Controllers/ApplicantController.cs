using ats.Models;
using ats.Logic;
using Microsoft.AspNetCore.Mvc;
using shraredclasses.DTOs;
using shraredclasses.Models;


namespace ats.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicantController : ControllerBase
{

    // в контроллере должны быть методы отлика на вакансию (ее идентификатор)
    // метод выбора кандидата
    // метод заполнения анкеты
    // методы принятия оффера и отказа от него
    // 
    private readonly ILogger<ApplicantController> _logger;
    private AtsService _atsService;
    public ApplicantController(ILogger<ApplicantController> logger, AtsService atsService)
    {
        _logger = logger;
        _atsService = atsService;
    }

   
    [HttpPut]
    public async Task<IActionResult> SetQuestionare(string vacancyId, SetUpApplicantQuestionareDTO applicantQuestionare)
    {
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> VacancyResponse(string vacancyId, VacancyResponse vacancyResponse)
    {
        var result = _atsService.RegisterVacancyResponse(vacancyId, vacancyResponse);
        if (!result)
            return NotFound();
        return Accepted();
    }
    [HttpPut]
    public async Task<IActionResult> AcceptOffer(string offerId)
    {
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> RefuseOffer(string offerId)
    {
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> ChooseApplicant(string vacancyId, string email)
    {
        return Ok();
    }


}


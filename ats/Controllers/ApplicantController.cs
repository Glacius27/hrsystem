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

   
    //[HttpPut]
    //public async Task<IActionResult> SetQuestionare(string vacancyId, SetUpApplicantQuestionareDTO applicantQuestionare)
    //{
    //    return Ok();
    //}
    [HttpPost]
    [Route("/Vacancy/Response")]
    public async Task<IActionResult> CreateVacancyResponse()
    {
        var response = _atsService.CreateVacancyResponse();
        return Ok(new WsResponse() {Data = response });
    }



    [HttpPut]
    [Route("/Vacancy/Response")]
    public async Task<IActionResult> SetVacancyResponse(string vacancyId, VacancyResponse vacancyResponse)
    {
        var result = _atsService.RegisterVacancyResponse(vacancyId, vacancyResponse);
        if (!result)
            return NotFound();
        return Accepted();
    }
    //[HttpPut]
    //public async Task<IActionResult> AcceptOffer(string offerId)
    //{
    //    return Ok();
    //}
    //[HttpPut]
    //public async Task<IActionResult> RefuseOffer(string offerId)
    //{
    //    return Ok();
    //}
    [HttpPut]
    [Route("/Vacancy/IntreviewInvite")]
    public async Task<IActionResult> InviteToInterview(string vacancyId, string vacancyResponseID)
    {
        var result = _atsService.InviteToInterview(vacancyId, vacancyResponseID);
        return Accepted();
    }
    [HttpPut]
    [Route("/Vacancy/Applicant")]
    public async Task<IActionResult> ChooseApplicant(string vacancyId, string vacancyResponseID)
    {
        var result = _atsService.ChooseApplicant(vacancyId, vacancyResponseID);
        return Accepted();
    }

    [HttpPut]
    [Route("/Vacancy/Applicant/Questionnaire")]
    public async Task<IActionResult> SetupQuestionnaire(string applicantId, SetUpApplicantQuestionnareDTO setUpApplicantQuestionnareDTO)
    {
        var result = _atsService.SetUpQuestionnare(applicantId, setUpApplicantQuestionnareDTO);
        return Accepted();
    }
}


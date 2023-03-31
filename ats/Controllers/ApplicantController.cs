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
    [Route("/Vacancy/Response/{vacancyId}")]
    public async Task<IActionResult> SetVacancyResponse(string vacancyId, VacancyResponse vacancyResponse)
    {
        var result = _atsService.RegisterVacancyResponse(vacancyId, vacancyResponse);
        if (!result)
            return NotFound();
        return Accepted();
    }

    [HttpPut]
    [Route("/Vacancy/Offer/Reject/{applicantId}")]
    public async Task<IActionResult> RejectOffer(string applicantId, string offerId)
    {
        var status = JobOfferStatus.refused;
        await _atsService.ChangeOfferState(applicantId, offerId, status);
        return Accepted();
    }
    [HttpPut]
    [Route("/Vacancy/Offer/Accept/{applicantId}")]
    public async Task<IActionResult> AcceptOffer(string applicantId, string offerId)
    {
        var status = JobOfferStatus.accepted;
        await _atsService.ChangeOfferState(applicantId, offerId, status);
        return Accepted();
    }


    [HttpPost]
    [Route("/Vacancy/Applicant")]
    public async Task<IActionResult> CreateApplicantID()
    {
        var result = await _atsService.CreateApplicantID();
        return Ok(new WsResponse() { Data = result.ID});
    }

    [HttpPut]
    [Route("/Vacancy/Applicant/{applicantId}")]
    public async Task<IActionResult> CreateApplicant(string applicantId, CreateApplicantDTO createApplicantDTO)
    {
        var result = await _atsService.CreateApplicant(applicantId, createApplicantDTO);
        return Ok();
    }


    [HttpPut]
    [Route("/Vacancy/Applicant/Questionnaire/{applicantId}")]
    public async Task<IActionResult> SetupQuestionnaire(string applicantId, SetUpApplicantQuestionnareDTO setUpApplicantQuestionnareDTO)
    {
        var result = _atsService.SetUpQuestionnare(applicantId, setUpApplicantQuestionnareDTO);
        return Accepted();
    }

    [HttpGet]
    [Route("/Vacancy/Applicant/{applicantId}")]
    public async Task<IActionResult> GetApplicant(string applicantId)
    {
        var result = await _atsService.GetApplicant(applicantId);
        return Ok(new WsResponse() { Data = result });
    }

    [HttpPut]
    [Route("/Vacancy/Applicant/Offer/{applicantId}")]
    public async Task<IActionResult> CreateOffer(string applicantId, CreateJobOfferDTO createJobOfferDTO)
    {
        var result = await _atsService.CreateOffer(applicantId, createJobOfferDTO);
        return Ok(new WsResponse() { Data = result.JobOfferID });
    }
}


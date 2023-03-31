using ats.Models;
using ats.Logic;
using Microsoft.AspNetCore.Mvc;
using shraredclasses.DTOs;
using shraredclasses.Models;
using Microsoft.AspNetCore.Authorization;
using MassTransit.JobService;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ats.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicantController : ControllerBase
{

    private readonly ILogger<ApplicantController> _logger;
    private AtsService _atsService;
    public ApplicantController(ILogger<ApplicantController> logger, AtsService atsService)
    {
        _logger = logger;
        _atsService = atsService;
    }


    [HttpPut]
    [Authorize(Roles = "hrbp")]
    [Route("/Vacancy/IntreviewInvite/{vacancyId}")]
    public async Task<IActionResult> InvitetoInterview(string vacancyId, string vacancyResponseID)
    {
        var response = _atsService.InviteToInterview(vacancyId, vacancyResponseID);
        return Ok();
    }
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
    [Authorize(Roles = "Applicant")]
    [Route("/Vacancy/Offer/Reject/{applicantId}")]
    public async Task<IActionResult> RejectOffer(string applicantId, string offerId)
    {
        var status = JobOfferStatus.refused;
        await _atsService.ChangeOfferState(applicantId, offerId, status);
        return Accepted();
    }
    [HttpPut]
    [Authorize(Roles = "Applicant")]
    [Route("/Vacancy/Offer/Accept/{applicantId}")]
    public async Task<IActionResult> AcceptOffer(string applicantId, string offerId)
    {
        var status = JobOfferStatus.accepted;
        await _atsService.ChangeOfferState(applicantId, offerId, status);
        return Accepted();
    }


    [HttpPost]
    [Authorize(Roles = "hrbp")]
    [Route("/Vacancy/Applicant")]
    public async Task<IActionResult> CreateApplicantID()
    {
        var result = await _atsService.CreateApplicantID();
        return Ok(new WsResponse() { Data = result.ID});
    }

    [HttpPut]
    [Authorize(Roles = "hrbp")]
    [Route("/Vacancy/Applicant/{applicantId}")]
    public async Task<IActionResult> CreateApplicant(string applicantId, CreateApplicantDTO createApplicantDTO)
    {
        var result = await _atsService.CreateApplicant(applicantId, createApplicantDTO);
        return Ok();
    }


    [HttpPut]
    [Authorize(Roles = "Applicant")]
    [Route("/Vacancy/Applicant/Questionnaire/{applicantId}")]
    public async Task<IActionResult> SetupQuestionnaire(string applicantId, SetUpApplicantQuestionnareDTO setUpApplicantQuestionnareDTO)
    {
        var _user = HttpContext.User.Identities;
        var result = _atsService.SetUpQuestionnare(applicantId, setUpApplicantQuestionnareDTO);
        return Accepted();
    }

    [HttpGet]
    [Authorize(Roles = "hrbp")]
    [Route("/Vacancy/Applicant/{applicantId}")]
    public async Task<IActionResult> GetApplicant(string applicantId)
    {
        var result = await _atsService.GetApplicant(applicantId);
        return Ok(new WsResponse() { Data = result });
    }

    [HttpPut]
    [Authorize(Roles = "hrbp")]
    [Route("/Vacancy/Applicant/Offer/{applicantId}")]
    public async Task<IActionResult> CreateOffer(string applicantId, CreateJobOfferDTO createJobOfferDTO)
    {
        var result = await _atsService.CreateOffer(applicantId, createJobOfferDTO);
        return Ok(new WsResponse() { Data = result.JobOfferID });
    }
}


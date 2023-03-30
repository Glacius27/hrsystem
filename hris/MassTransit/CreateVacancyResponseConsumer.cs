using System;
using hris.Logic;
using MassTransit;
using shraredclasses.Commands;

namespace hris.MassTransit
{
	public class CreateVacancyResponseConsumer : IConsumer<CreateVacancyResponse>
    {
        private readonly ILogger<HrIsService> _logger;
        private HrIsService _hrIsService;
        public CreateVacancyResponseConsumer(ILogger<HrIsService> logger, HrIsService hrIsService)
        {
            _logger = logger;
            _hrIsService = hrIsService;
        }

        public async Task Consume(ConsumeContext<CreateVacancyResponse> context)
        {
            var createVacancyResponse = context.Message;
            await _hrIsService.SetUpVacancyIDCreateVacantPositionRequest(createVacancyResponse);
        }
    }
}

//public class CreateVacancyConsumer : IConsumer<CreateVacancy>
//{
//    private readonly ILogger<AtsService> _logger;
//    private AtsService _atsService;
//    public CreateVacancyConsumer(ILogger<AtsService> logger, AtsService atsService)
//    {
//        _logger = logger;
//        _atsService = atsService;
//    }
//    public async Task Consume(ConsumeContext<CreateVacancy> context)
//    {
//        var createVacancyRequest = context.Message;
//        var response = _atsService.CreateVacancy(new Vacancy(createVacancyRequest));
//    }
//}
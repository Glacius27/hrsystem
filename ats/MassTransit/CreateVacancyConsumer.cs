using System;
using ats.Logic;
using MassTransit;
using System.Text.Json;
using shraredclasses.Commands;
using ats.Models;

namespace ats.MassTransit
{
	public class CreateVacancyConsumer: IConsumer<CreateVacancy>
    {
        private readonly ILogger<AtsService> _logger;
        private AtsService _atsService;
        public CreateVacancyConsumer(ILogger<AtsService> logger, AtsService atsService)
        {
            _logger = logger;
            _atsService = atsService;
        }
        public async Task Consume(ConsumeContext<CreateVacancy> context)
        {
            var createVacancyRequest = context.Message; 
            var response = _atsService.CreateVacancy(createVacancyRequest);
        }
    }
}


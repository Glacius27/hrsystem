using System;
using ats.Logic;
using MassTransit;
using shraredclasses.Commands;

namespace ats.MassTransit.Consumers
{
	public class CreateEmployeeResponseConsumer: IConsumer<CreateEmployeeResponse>
    {
        private readonly ILogger<AtsService> _logger;
        private AtsService _atsService;

        public CreateEmployeeResponseConsumer(ILogger<AtsService> logger, AtsService atsService)
        {
            _logger = logger;
            _atsService = atsService;
        }

        public async Task Consume(ConsumeContext<CreateEmployeeResponse> context)
        {
            var createEmployeeResponse = context.Message;
            await _atsService.ApplyEmployee(createEmployeeResponse);
        }
    }
    
}

using System;
using ats.Logic;
using ats.Models;
using MassTransit;
using shraredclasses.Commands;

namespace ats.MassTransit
{
	public class CreateUserResponseConsumer: IConsumer<CreateUserResponse>
    {
        private readonly ILogger<AtsService> _logger;
        private AtsService _atsService;

        public CreateUserResponseConsumer(ILogger<AtsService> logger, AtsService atsService)
        {
            _logger = logger;
            _atsService = atsService;
        }

        public async Task Consume(ConsumeContext<CreateUserResponse> context)
        {
            var createUserResponse = context.Message;
            await _atsService.SetUserIdToApplicant(createUserResponse);
        }
    }
}


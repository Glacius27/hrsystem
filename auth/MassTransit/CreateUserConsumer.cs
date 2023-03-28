using System;
using auth.Logic;
using MassTransit;
using shraredclasses.Commands;
using auth.Models;


namespace auth.MassTransit
{
	public class CreateUserConsumer: IConsumer<CreateUser>
	{
        private readonly ILogger<UserService> _logger;
        private UserService _userService;
        private readonly IBus _bus;

        public CreateUserConsumer(ILogger<UserService> logger, UserService userService, IBus bus)
        {
            _logger = logger;
            _userService = userService;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<CreateUser> context)
        {
            var data = context.Message;
            var response = await _userService.CreateUser(new User(data));
            await context.RespondAsync(new CreateUserResponse() { UserID = response, CorrelationID = data.CorrelationID });
        }
    }
}


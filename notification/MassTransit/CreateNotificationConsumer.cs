using System;
using MassTransit;
using notification.Logic;
using shraredclasses.Commands;

namespace notification.MassTransit
{

    public class CreateNotificationConsumer : IConsumer<CreateNotification>
    {
        private readonly ILogger<NotificationService> _logger;
        private NotificationService _notificationService;
        private readonly IBus _bus;

        public CreateNotificationConsumer(ILogger<NotificationService> logger, NotificationService notificationService, IBus bus)
        {
            _logger = logger;
            _notificationService = notificationService;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<CreateNotification> context)
        {
            var data = context.Message;
            await _notificationService.SendMail(data);
        }
    }
}


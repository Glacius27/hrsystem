using System;
using lms.Logic;
using MassTransit;
using shraredclasses.Commands;

namespace lms.MassTransit
{
	public class CreateLearningTrackConsumer:IConsumer<CreateLearningTrackRequest>
	{
        private readonly ILogger<LmsService> _logger;
        private LmsService _lmsService;
        public CreateLearningTrackConsumer(ILogger<LmsService> logger, LmsService lmsService)
        {
            _logger = logger;
            _lmsService = lmsService;
        }

        public async Task Consume(ConsumeContext<CreateLearningTrackRequest> context)
        {
            var data = context.Message;
            await _lmsService.AddWelcomeCourse(data);
        }
    }
}




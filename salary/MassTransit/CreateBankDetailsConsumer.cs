using System;
using MassTransit;
using salary.Logic;
using shraredclasses.Commands;

namespace salary.MassTransit
{
	public class CreateBankDetailsConsumer : IConsumer<CreateBankDetailsRequest>
    {
        private readonly ILogger<SalaryService> _logger;
        private SalaryService _salaryService;

        public CreateBankDetailsConsumer(ILogger<SalaryService> logger, SalaryService salaryService)
        {
            _logger = logger;
            _salaryService = salaryService;
        }
        public async Task Consume(ConsumeContext<CreateBankDetailsRequest> context)
        {
            var data = context.Message;
            await _salaryService.AddBankDetails(data);
        }

    }
}

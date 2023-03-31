using System;
using hris.Logic;
using hris.Models;
using shraredclasses.Commands;
using MassTransit;

namespace hris.MassTransit
{
	public class CreateEmployeeConsumer: IConsumer<CreateEmployeeRequest>
    {
        private readonly ILogger<HrIsService> _logger;
        private HrIsService _hrIsService;

        public CreateEmployeeConsumer(ILogger<HrIsService> logger, HrIsService hrIsService)
        {
            _logger = logger;
            _hrIsService = hrIsService;
        }
        public async Task Consume(ConsumeContext<CreateEmployeeRequest> context)
        {
            var cre = context.Message;
            await _hrIsService.CreateEmployee(cre);
        }
    }
}


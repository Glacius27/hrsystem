using System;
using hris.DB;
using hris.Models;
//using hris.MassTransit;
using hris.Controllers;
using MassTransit;
using shraredclasses.Commands;
using System.Threading;
using MassTransit.Clients;

namespace hris.Logic
{
	public class HrIsService
	{
        private readonly IBus _bus;
        private readonly ILogger<HrIsService> _logger;

        public HrIsService(ILogger<HrIsService> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task SetPositionVacant(string positionID)
		{

            #region
            //var serviceAddress = new Uri("rabbitmq://localhost/check-order-status");
            //var client = _bus.CreateRequestClient<CreateVacancy>(serviceAddress);
            //var response = await _client.GetResponse<CreateVacancy>(new CreateVacancy());


            //using (var request = _client.Create(new CreateVacancy()))
            //{
            //    var response = await request.GetResponse<CreateVacancy>();
            //    //p = response.Message.Product;
            //}



            ////var serviceAddress = new Uri("rabbitmq://localhost/check-order-status");
            ////var client = _bus.CreateRequestClient<CreateVacancy>(serviceAddress);
            ////_bus.CreateRequestClient<CreateVacancy>();
            ////var response = await client.GetResponse<CreateVacancyResult>(new { OrderId = id });

            ////var response = await client.GetResponse<OrderStatusResult>(new { OrderId = id });
            ////var serviceAddress = new Uri("rabbitmq://localhost/check-order-status");
            ////var client = _bus.CreateRequestClient<CreateVacancyCommand>(serviceAddress);
            ////_bus.CreateRequestClient<CreateVacancyCommand>();
            ////var response = await client.GetResponse<OrderStatusResult>(new { OrderId = id });


            ////var response = await _client.GetResponse<CreateVacancyCommandResponse>(new CreateVacancyCommand());


            //#region
            ////Ireq
            #endregion
            Uri uri = new Uri("rabbitmq://localhost/createvacancy");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new CreateVacancy() { PositionID = positionID, City = "Moscow", PositionName = "Lead Developer"});
            
        }

        public async Task CreateEmployee(Employee emp)
		{
			using (EmployeeContext db = new EmployeeContext())
			{
				await db.Employees.AddAsync(emp);
				await db.SaveChangesAsync();
			}
		}
    }
}


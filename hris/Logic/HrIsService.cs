using System;
using hris.DB;
using hris.Models;
//using hris.MassTransit;
using hris.Controllers;
using MassTransit;
using shraredclasses.Commands;
using System.Threading;
using MassTransit.Clients;
using Npgsql.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        public async Task SetPositionVacant(string createVacantPositionRequestId, string positionID)
		{
            var correlationID = Guid.NewGuid().ToString();
            using (CreateVacantPositionRequestContext db = new CreateVacantPositionRequestContext())
            {

                var request = await db.CreateVacantPositionRequests.FirstOrDefaultAsync(x => x.ID == createVacantPositionRequestId);
                //var requset = await db.CreateVacantPositionRequests
                //                .Where(x => x.ID == createVacantPositionRequestId).FirstOrDefaultAsync();
                                
                request.PositionID = positionID;
                request.CorrelationID = correlationID;
                await db.SaveChangesAsync();
            }

            Uri uri = new Uri("rabbitmq://localhost/createvacancy");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new CreateVacancy() { PositionID = positionID, City = "Moscow", PositionName = "Lead Developer", CorrelationID = correlationID});
        }

        public async Task<string> CreateVacantPositionRequest()
        {
            using (CreateVacantPositionRequestContext db = new CreateVacantPositionRequestContext())
            {
                var result = await db.CreateVacantPositionRequests.AddAsync(new CreateVacantPositionRequest() { CorrelationID = "null", PositionID = "null", VacancyID = "null"});
                await db.SaveChangesAsync();
                return result.Entity.ID;
            }
        }

        public async Task SetUpVacancyIDCreateVacantPositionRequest(CreateVacancyResponse response)
        {
            using (CreateVacantPositionRequestContext db = new CreateVacantPositionRequestContext())
            {
                var request = await db.CreateVacantPositionRequests.FirstOrDefaultAsync(x => x.CorrelationID == response.CorrelationID);
                request.VacancyID = response.VacancyID;
                await db.SaveChangesAsync();
            }
        }

        public async Task<string> GetVacancyID(string positionId)
        {
            using (CreateVacantPositionRequestContext db = new CreateVacantPositionRequestContext())
            {
                var request = db.CreateVacantPositionRequests.FirstOrDefault(x => x.PositionID == positionId);
                return request.VacancyID;
            }
        }


        //public async Task SetUPVacantPositionRequest()
        //{
        //    using (CreateVacantPositionRequestContext db = new CreateVacantPositionRequestContext())
        //    {
        //        await db.CreateVacantPositionRequests.AddAsync(new CreateVacantPositionRequest());
        //        await db.SaveChangesAsync();
        //    }
        //}



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


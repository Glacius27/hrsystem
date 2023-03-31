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
            using (DataBaseService db = new DataBaseService())
            {

                var request = await db.CreateVacantPositionRequests.FirstOrDefaultAsync(x => x.ID == createVacantPositionRequestId);                   
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
            using (DataBaseService db = new DataBaseService())
            {
                var result = await db.CreateVacantPositionRequests.AddAsync(new CreateVacantPositionRequest() { CorrelationID = "null", PositionID = "null", VacancyID = "null"});
                await db.SaveChangesAsync();
                return result.Entity.ID;
            }
        }

        public async Task SetUpVacancyIDCreateVacantPositionRequest(CreateVacancyResponse response)
        {
            using (DataBaseService db = new DataBaseService())
            {
                var request = await db.CreateVacantPositionRequests.FirstOrDefaultAsync(x => x.CorrelationID == response.CorrelationID);
                request.VacancyID = response.VacancyID;
                await db.SaveChangesAsync();
            }
        }

        public async Task<string> GetVacancyID(string positionId)
        {
            using (DataBaseService db = new DataBaseService())
            {
                var request = db.CreateVacantPositionRequests.FirstOrDefault(x => x.PositionID == positionId);
                return request.VacancyID;
            }
        }



        public async Task CreateEmployee(CreateEmployeeRequest createEmployeeRequest)
        {
            Employee employee = null;
            CreateVacantPositionRequest request = null;
            using (DataBaseService db = new DataBaseService())
            {
                request = await db.CreateVacantPositionRequests.FirstOrDefaultAsync(x => x.VacancyID == createEmployeeRequest.VacancyID);
            }
            var emp = new Employee()
            {
                FirstName = createEmployeeRequest.FirstName,
                LastName = createEmployeeRequest.LastName,
                PositionID = request.PositionID
              };
            using (DataBaseService db = new DataBaseService())
            {
                try
                {
                    await db.Employees.AddAsync(emp);
                    await db.SaveChangesAsync();
                    employee = await db.Employees.FirstOrDefaultAsync(x => x.PositionID == emp.PositionID);
                }catch(Exception ex)
                {

                }
            }

                Uri uri = new Uri("rabbitmq://localhost/createEmployeeResponse");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(new CreateEmployeeResponse() {CorrelationID = createEmployeeRequest.CorrelationID, EmployeeID = employee.PositionID, ApplicantID = createEmployeeRequest.ApplicantID } );
        }
    }
}


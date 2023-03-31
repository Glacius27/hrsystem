using System;
using MassTransit;
using shraredclasses.Events;
using shraredclasses.Commands;


namespace ats.MassTransit.Saga
{
	public class OnboardingEmployeeSaga : MassTransitStateMachine<OnboardingEmployeeSagaState>
    {
        private readonly ILogger<OnboardingEmployeeSaga> _logger;
        public OnboardingEmployeeSaga(ILogger<OnboardingEmployeeSaga> logger)
        {
            _logger = logger;
            InstanceState(x => x.CurrentState);
            Event<OfferAcceptedEvent>(() => OfferAccepted, x => x.CorrelateById(y => y.Message.ProcessID));

            Request(
                 () => CreateEmployee
                 );
            Request(
                 () => SetUpBankDetails
                 );
            Request(
                 () => CreateCourse
                 );
            Initially(
                    When(OfferAccepted)
                    .Then(x =>
                    {
                        if (!x.TryGetPayload(out SagaConsumeContext<OnboardingEmployeeSagaState, OfferAcceptedEvent> payload))
                            x.Saga.RequestId = payload.RequestId;
                        x.Saga.ResponseAddress = payload.ResponseAddress;
                    })
                    .Request(CreateEmployee, x => x.Init<CreateEmployeeRequest>(new { })).TransitionTo(CreateEmployee.Pending));

            During(CreateEmployee.Pending,
                When(CreateEmployee.Completed)
                    .Request(SetUpBankDetails, x => x.Init<CreateBankDetailsRequest>(new { }))
                        .TransitionTo(SetUpBankDetails.Pending),
                When(CreateEmployee.Faulted)
                    .ThenAsync(async context =>
                        {

                        })
                    .TransitionTo(Failed));


            During(CreateCourse.Pending,
                When(SetUpBankDetails.Completed)
           .Request(CreateCourse, x => x.Init<CreateLearningTrackRequest>(new { }))
               .TransitionTo(CreateCourse.Pending),
               When(CreateEmployee.Faulted)
                   .ThenAsync(async context =>
                   {

                   })
                   .TransitionTo(Failed));

            During(CreateCourse.Pending,
            When(CreateCourse.Completed)
              .ThenAsync(async context =>
              {
                  await RespondFromSaga(context, null);
              })
            .Finalize(),

            When(CreateCourse.Faulted)
              .ThenAsync(async context =>
              {
               
              })
            .TransitionTo(Failed),

            When(CreateCourse.TimeoutExpired)
               .ThenAsync(async context =>
               {
                   await RespondFromSaga(context, "Timeout Expired On Create Courses");
               })
            .TransitionTo(Failed)
            );
        }


        public Request<OnboardingEmployeeSagaState, CreateEmployeeRequest, CreateEmployeeResponse> CreateEmployee { get; set; }
        public Request<OnboardingEmployeeSagaState, CreateBankDetailsRequest, CreateBankDetailsResponse> SetUpBankDetails { get; set; }
        public Request<OnboardingEmployeeSagaState, CreateLearningTrackRequest, CreateLearningTrackResponse> CreateCourse { get; set; }
        

        public Event<OfferAcceptedEvent> OfferAccepted { get; set; }

        public State Failed { get; set; }

        private static async Task RespondFromSaga<T>(BehaviorContext<OnboardingEmployeeSagaState, T> context, string error) where T : class
        {
            var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
        }
    }
}
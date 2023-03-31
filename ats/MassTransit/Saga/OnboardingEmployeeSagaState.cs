using System;
using MassTransit;

namespace ats.MassTransit.Saga
{
	public class OnboardingEmployeeSagaState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string? CurrentState { get; set; }
        public Guid? RequestId { get; set; }
        public Uri? ResponseAddress { get; set; }
        public int Version { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}


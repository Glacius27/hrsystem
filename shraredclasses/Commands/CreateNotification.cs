using System;
namespace shraredclasses.Commands
{
	public class CreateNotification
	{
		public string PositionName { get; set; }
        public string Email { get; set; }
		public NotificationType NotificationType { get; set; }
		public string UserID { get; set; } 
    }

	public enum NotificationType
    {
		Interview,
		Register,
		Greetings
	}
}


﻿using System;
using shraredclasses.Commands;
using notification.Model;
using notification.DB;

namespace notification.Logic
{
	public class NotificationService
	{
		public async Task SendMail(CreateNotification createNotification)
		{
			string text = null;

			if(createNotification.NotificationType == NotificationType.Interview)
				text = "Interview invitation";
            if (createNotification.NotificationType == NotificationType.Register)
				text = "HR platform register invitation";
            if (createNotification.NotificationType == NotificationType.Greetings)
				text = "We proud to see you in our team";


			var letter = new Letter()
			{
				Email = createNotification.Email,
				UserID = createNotification.UserID,
				Text = text
			};

			try
			{
				using (LetterContext db = new LetterContext())
				{
					await db.Letters.AddAsync(letter);
					await db.SaveChangesAsync();
				}
			}catch(Exception ex)
			{

			}
		}
	}
}


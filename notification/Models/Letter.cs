using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using shraredclasses.Commands;
namespace notification.Model
{
	public class Letter
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }



        

        //public Letter(CreateNotification createNotification)
        //{
        //    this.UserID = createNotification.UserID;
        //    this.Email = createNotification.Email;
        //    if (createNotification.NotificationType == NotificationType.Interview)
        //        this.Text = "Interview invitation";
        //    if (createNotification.NotificationType == NotificationType.Register)
        //        this.Text = "HR platform register invitation";
        //    if (createNotification.NotificationType == NotificationType.Greetings)
        //        this.Text = "We proud to see you in our team";
        //}
    }
}


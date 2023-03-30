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
        public string Email { get; set; }
        public string Text { get; set; }


    }
}


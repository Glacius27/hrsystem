using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hris.Models
{
	public class Employee
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		public string PositionID { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}


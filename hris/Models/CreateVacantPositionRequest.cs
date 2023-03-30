using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hris.Models
{
	public class CreateVacantPositionRequest
	{
			[Key]
			[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
			public string ID { get; set; }
			public string ?CorrelationID { get; set; }
			public string ?PositionID { get; set; }
			public string ?VacancyID { get; set; }
    }
}


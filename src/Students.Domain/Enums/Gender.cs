using System.ComponentModel.DataAnnotations;

namespace Students.Domain.Enums;

public enum Gender
{
	[Display(Name = "Мужской")]
	Male = 1,

	[Display(Name = "Женский")]
	Female = 2,
}

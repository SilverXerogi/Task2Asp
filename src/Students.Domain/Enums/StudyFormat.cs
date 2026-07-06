using System.ComponentModel.DataAnnotations;

namespace Students.Domain.Enums;

public enum StudyFormat
{
    [Display(Name = "Очно")]
    FullTime = 1,

    [Display(Name = "Заочно")]
    PartTime = 2,

    [Display(Name = "Вечернее")]
    Evening = 3
}

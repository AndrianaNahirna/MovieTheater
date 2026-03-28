using System;
using System.ComponentModel.DataAnnotations;

namespace ActorService.Models
{
    public class DateInPastAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date > DateTime.Today)
                {
                    return new ValidationResult("Дата народження не може бути у майбутньому.");
                }

                if (date.Year < 1850)
                {
                    return new ValidationResult("Рік народження має бути реальним (не раніше 1850 року).");
                }
                if (date.Date > DateTime.Now.AddYears(-1))
                {
                    return new ValidationResult("Актору має бути щонайменше 1 рік.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
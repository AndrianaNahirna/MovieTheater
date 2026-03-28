using System;
using System.ComponentModel.DataAnnotations;

namespace MovieService.Models
{
    public class ValidReleaseYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                int currentYear = DateTime.Now.Year; 

                if (year < 1895 || year > currentYear)
                {
                    return new ValidationResult($"Рік випуску має бути в межах від 1895 до {currentYear}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
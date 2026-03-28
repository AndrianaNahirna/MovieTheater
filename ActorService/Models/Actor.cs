using ActorService.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActorService.Models
{
    public class Actor : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Вкажіть ім'я актора.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ім'я має містити від 2 до 50 символів.")]
        [RegularExpression(@"^[A-ZА-ЯІЇЄҐa-zа-яіїєґ\s\-']+$", ErrorMessage = "Ім'я може містити лише літери.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Вкажіть прізвище актора.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Прізвище має містити від 2 до 50 символів.")]
        [RegularExpression(@"^[A-ZА-ЯІЇЄҐa-zа-яіїєґ\s\-']+$", ErrorMessage = "Прізвище може містити лише літери.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Вкажіть дату народження.")]
        [DataType(DataType.Date)]
        [DateInPast]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Вкажіть країну.")]
        [StringLength(50, ErrorMessage = "Назва країни занадто довга.")]
        [ValidCountry]
        public string Country { get; set; }

        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Біографія має містити від 10 до 2000 символів.")]
        public string? Biography { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
            {
                if (FirstName.Equals(LastName, StringComparison.OrdinalIgnoreCase))
                {
                    yield return new ValidationResult(
                        "Ім'я та прізвище не можуть бути однаковими.",
                        new[] { nameof(FirstName), nameof(LastName) }
                    );
                }
            }
        }
    }
}
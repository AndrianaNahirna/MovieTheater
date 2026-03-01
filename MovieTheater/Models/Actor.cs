using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ім'я актора є обов'язковим.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ім'я має містити від 2 до 50 символів.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Прізвище актора є обов'язковим.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Прізвище має містити від 2 до 50 символів.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Вкажіть дату народження.")]
        [DataType(DataType.Date)]
        [DateInPast] 
        public DateTime BirthDate { get; set; }

        [StringLength(50, ErrorMessage = "Назва країни занадто довга.")]
        public string Country { get; set; }

        [StringLength(2000, ErrorMessage = "Біографія не може перевищувати 2000 символів.")]
        public string Biography { get; set; }

        public List<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();
    }
}
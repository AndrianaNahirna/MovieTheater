using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Назва' є обов'язковим для заповнення.")]
        [StringLength(100, ErrorMessage = "Назва не може бути довшою за 100 символів.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Вкажіть рік випуску.")]
        [ValidReleaseYear] 
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Поле 'Жанр' є обов'язковим.")]
        [StringLength(50)]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Вкажіть тривалість фільму.")]
        [Range(1, int.MaxValue, ErrorMessage = "Тривалість має бути більшою за 0.")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "Вкажіть рейтинг.")]
        [Range(0.0, 10.0, ErrorMessage = "Рейтинг має бути числом від 0 до 10.")]
        public double Rating { get; set; }

        public List<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();
    }
}
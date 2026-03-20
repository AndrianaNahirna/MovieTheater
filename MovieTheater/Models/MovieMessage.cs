namespace MovieTheater.Models
{
    public class MovieMessage
    {
        public int Id { get; set; }
        public int MovieId { get; set; } 
        public string UserName { get; set; } 
        public string Text { get; set; } 
        public string? ImageUrl { get; set; }
        public string? FileName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
    }
}
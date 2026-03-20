namespace MovieTheater.Models
{
    public class PrivateMessage
    {
        public int Id { get; set; }
        public string SenderName { get; set; } 
        public string ReceiverName { get; set; } 
        public string Text { get; set; }
        public string? ImageUrl { get; set; } 
        public string? FileName { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
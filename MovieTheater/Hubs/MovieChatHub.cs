using Microsoft.AspNetCore.SignalR;
using MovieTheater.Data;
using MovieTheater.Models;

namespace MovieTheater.Hubs
{
    public class MovieChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public MovieChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task JoinMovieGroup(string movieId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, movieId);
        }

        public async Task LeaveMovieGroup(string movieId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, movieId);
        }

        public async Task SendMessageToMovie(string movieId, string user, string message, string? fileData, string? fileName)
        {
            var chatMessage = new MovieMessage
            {
                MovieId = int.Parse(movieId),
                UserName = user,
                Text = message ?? "",
                ImageUrl = fileData,
                FileName = fileName,
                CreatedAt = DateTime.Now
            };

            _context.MovieMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(movieId).SendAsync("ReceiveMessage", user, message ?? "", fileData, fileName);
        }

        public async Task SendDirectMessage(string sender, string receiver, string message, string? fileData, string? fileName)
        {
            var msg = new PrivateMessage
            {
                SenderName = sender,
                ReceiverName = receiver,
                Text = message ?? "",
                ImageUrl = fileData,
                FileName = fileName,
                CreatedAt = DateTime.Now
            };

            _context.PrivateMessages.Add(msg);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveDirectMessage", sender, receiver, message ?? "", fileData, fileName);
        }
    }
}
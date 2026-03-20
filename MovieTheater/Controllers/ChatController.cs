using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Data;
using System.Linq;

namespace MovieTheater.Controllers
{
    [Authorize] 
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string receiver = "")
        {
            var currentUser = User.Identity.Name;

            ViewBag.AllUsers = _context.Users
                .Where(u => u.UserName != currentUser)
                .Select(u => u.UserName)
                .ToList();

            ViewBag.CurrentReceiver = receiver;

            if (!string.IsNullOrEmpty(receiver))
            {
                ViewBag.Messages = _context.PrivateMessages
                    .Where(m => (m.SenderName == currentUser && m.ReceiverName == receiver) ||
                                (m.SenderName == receiver && m.ReceiverName == currentUser))
                    .OrderBy(m => m.CreatedAt)
                    .ToList();
            }

            return View();
        }
    }
}
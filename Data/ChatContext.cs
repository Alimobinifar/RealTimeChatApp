using RealTimeChatApp.Models;
using Microsoft.EntityFrameworkCore;


namespace RealTimeChatApp.Data
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

        public DbSet<ChatMessage> Messages { get; set; }
    }
}

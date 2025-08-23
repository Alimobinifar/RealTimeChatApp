namespace RealTimeChatApp.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string User { get; set; } = "";
        public string Message { get; set; } = "";
        public string? GroupName { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}

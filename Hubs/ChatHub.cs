using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Data;
using RealTimeChatApp.Models;
using System.Collections.Concurrent;

public class ChatHub : Hub
{
    private static ConcurrentDictionary<string, string> _users = new ConcurrentDictionary<string, string>();
    private readonly ChatContext _db;

    public ChatHub(ChatContext db)
    {
        _db = db;
    }
    public override async Task OnConnectedAsync()
    {
        var userName = Context.GetHttpContext()?.Request.Query["user"].ToString();
        if (string.IsNullOrEmpty(userName))
            userName = $"User-{Context.ConnectionId.Substring(0, 5)}";

        _users[Context.ConnectionId] = userName;

        await Clients.All.SendAsync("UpdateUsers", _users.Values);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _users.TryRemove(Context.ConnectionId, out _);
        await Clients.All.SendAsync("UpdateUsers", _users.Values);
        await base.OnDisconnectedAsync(exception);
    }

    // پیام عمومی
    public async Task SendMessage(string user, string message)
    {
        #region Save messageOnDb
        var chatMessage = new ChatMessage { User = user, Message = message };
        _db.Messages.Add(chatMessage);
        await _db.SaveChangesAsync();
        #endregion

        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    // پیام خصوصی
    public async Task SendPrivateMessage(string toUserName, string fromUser, string message)
    {
        var toConnection = _users.FirstOrDefault(u => u.Value == toUserName).Key;

        if (!string.IsNullOrEmpty(toConnection))
        {
            await Clients.Client(toConnection)
                .SendAsync("ReceivePrivateMessage", fromUser, message);
        }
    }

    // گروه‌ها
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        var history = _db.Messages
            .Where(m => m.GroupName == groupName)
            .OrderBy(m => m.SentAt)
            .Take(20) // آخرین 20 پیام
            .ToList();

        await Clients.Caller.SendAsync("LoadHistory", history);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", "System", $"{_users[Context.ConnectionId]} joined {groupName}");
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", "System", $"{_users[Context.ConnectionId]} left {groupName}");
    }

    public async Task SendMessageToGroup(string groupName, string user, string message)
    {
        #region Save MessageOnDb
        var chatMessage = new ChatMessage { User = user, Message = message, GroupName = groupName };
        _db.Messages.Add(chatMessage);
        await _db.SaveChangesAsync();
        #endregion

        await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
    }
}

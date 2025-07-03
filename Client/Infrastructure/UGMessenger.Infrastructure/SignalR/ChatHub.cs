using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Security.Claims;
using UGMessenger.Infrastructure.Responses.Chat;

public class ChatHub : Hub
{
    private readonly IHttpClientFactory _сlientFactory;
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(IHttpClientFactory сlientFactory, ILogger<ChatHub> logger)
    {
        _сlientFactory = сlientFactory;
        _logger = logger;
    }

    private string? GetUserId() =>
        Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    private HttpClient GatewayClient => _сlientFactory.CreateClient("Gateway");

    private async Task SetUserPresenceAsync(bool isOnline)
    {
        string? userId = GetUserId();
        if (userId == null) return;

        var payload = new { userId, isOnline };
        await GatewayClient.PostAsJsonAsync("/presence-api/presence/set", payload);
    }

    public override async Task OnConnectedAsync()
    {
        await SetUserPresenceAsync(true);
        string? userId = GetUserId();

        if (userId != null)
            await Clients.Others.SendAsync("MemberIsOnline", userId);

        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await SetUserPresenceAsync(false);
        string? userId = GetUserId();

        if (userId != null)
            await Clients.Others.SendAsync("MemberIsOffline", userId);

        await base.OnDisconnectedAsync(exception);
    }
    public async Task JoinChat(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);

        var response = await GatewayClient.GetAsync($"/chat-api/Message/GetAllByChatId/{chatId}");

        if (response.IsSuccessStatusCode)
        {
            var messages = await response.Content.ReadFromJsonAsync<List<MessageResponse>>();
            await Clients.Caller.SendAsync("LoadChatHistory", chatId, messages);
        }
    }
    public async Task LeaveChat(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }
    public async Task SendMessage(string chatId, string text, byte[]? content, int? typeContent)
    {
        string? userId = GetUserId();
        if (userId == null) return;

        var messageDto = new
        {
            ChatId = chatId,
            SenderId = userId,
            Text = text,
            Content = content,
            TypeContent = typeContent
        };

        var response = await GatewayClient.PostAsJsonAsync("/chat-api/Message/Create", messageDto);
        if (response.IsSuccessStatusCode)
        {
            var sentMessage = await response.Content.ReadFromJsonAsync<MessageResponse>();
            await Clients.Group(chatId).SendAsync("ReceiveMessage", sentMessage);
        }
    }
    public async Task EditMessage(string messageId, string newText)
    {
        var dto = new
        {
            MessageId = messageId,
            NewText = newText
        };

        var response = await GatewayClient.PatchAsJsonAsync("/chat-api/Message/Patch", dto);
        if (response.IsSuccessStatusCode)
        {
            await Clients.All.SendAsync("MessageEdited", messageId, newText);
        }
    }
    public async Task DeleteMessage(string messageId)
    {
        var response = await GatewayClient.DeleteAsync($"/chat-api/Message/Delete/{messageId}");
        if (response.IsSuccessStatusCode)
        {
            await Clients.All.SendAsync("MessageDeleted", messageId);
        }
    }
}
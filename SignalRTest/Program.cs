using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(); // Dodaj SignalR

var app = builder.Build();

// Rejestracja Huba
app.MapHub<ChatHub>("/chatHub");

// Uruchom serwer na konkretnym porcie, żeby nie było wątpliwości
app.Run("http://localhost:5005");

// -------------------
// Definicja klasy ChatHub:
class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"[HUB] OnConnectedAsync() => ConnectionId: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string user, string message)
    {
        Console.WriteLine($"[HUB] Otrzymano wiadomość od '{user}': {message}");
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}

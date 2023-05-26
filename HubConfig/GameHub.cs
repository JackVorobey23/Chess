using Microsoft.AspNetCore.SignalR;

class GameHub : Hub
{
    public async Task AskServer(string text)
    {
        string temp;
        if (text == "11")
        {
            temp = "12";
        }
        else
        {
            temp = "fuck";

            await Clients.Clients(this.Context.ConnectionId).SendAsync("askResponse", temp);
        }
    }
}
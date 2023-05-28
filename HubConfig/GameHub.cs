using Microsoft.AspNetCore.SignalR;

class GameHub : Hub
{
    public GameHub(IRepository<Player> playerRepositpry, IRepository<Game> gameRepository)
    {
        _playerRepository = playerRepositpry;
        _gameRepository = gameRepository;
    }
    private IRepository<Player> _playerRepository;
    private IRepository<Game> _gameRepository;
    public List<Player> WaitList { get; set; }
    public async Task AskServer(string text)
    {
        await Clients.All.SendAsync("askResponse", "Server response: \n" + text);
    }
    public async Task AddWaiter(string playerName)
    {
        int playerId = int.Parse(playerName.Split("-")[1]);

        if (WaitList.Select(p => p.PlayerId == playerId) is not null)
        {
            return;
        }

        Player currentPLayer = await _playerRepository.FindById(playerId);

        if (currentPLayer is null)
        {
            currentPLayer = new Player(playerId, "Ghost");
            await _playerRepository.Add(currentPLayer);
        }
        WaitList.Add(currentPLayer);
        
        await Clients.All.SendAsync("askResponse", 
            "Current Waiters List: ",
            string.Join("; ", _playerRepository.GetAll().Result
            .Select(p => $"{p.PlayerId}: {p.FullName}, r - {p.Rating}")) + " response)");
        
        if(WaitList.Count == 2)
        {
            await AskServer("congratulations!");
        }
    }
}
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
class GameHub : Hub
{
    public GameHub(IRepository<Player> playerRepositpry, IRepository<Game> gameRepository)
    {
        _playerRepository = playerRepositpry;
        _gameRepository = gameRepository;
    }
    private IRepository<Player> _playerRepository;
    private IRepository<Game> _gameRepository;
    public async Task AskServer(string text)
    {
        await Clients.All.SendAsync("askResponse", "Server response: \n" + text);
    }
    public async Task AddWaiter(string playerName, int playerId)
    {
        Player currentPLayer = await _playerRepository.FindById(playerId);

        if (currentPLayer is null)
        {
            currentPLayer = new Player(playerId, playerName);
            currentPLayer.IsWaiting = true;
            await _playerRepository.Add(currentPLayer);
        }
        var waiters = _playerRepository.GetAll().Result.Where(p => p.IsWaiting).ToList();

        await Clients.All.SendAsync("askResponse", 
            "Current Waiters List: " +
            string.Join("\n", waiters.Select(p => $"{p.PlayerId}: {p.FullName}")));

        if(waiters.Count() == 2)
        {
            GameDto gameDto = new GameDto(waiters[0].PlayerId, waiters[1].PlayerId, new Random().Next(1000000));
            
            Game newGame = new Game(gameDto.GameId, gameDto.User1Id, gameDto.User2Id);
            
            await _gameRepository.Add(newGame);

            await AskServer("congratulations!");
            
            await Clients.All.SendAsync("gameStartListener", 
            JsonSerializer.Serialize(gameDto));

            _playerRepository.GetAll().Result.Where(p => p.IsWaiting)
            .ToList().ForEach(async p => {
                Player playerIsntWaiting = await _playerRepository.FindById(p.PlayerId);
                playerIsntWaiting.IsWaiting = false;
                await _playerRepository.Update(playerIsntWaiting);
            });
        }
    }
}
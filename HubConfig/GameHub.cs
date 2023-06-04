using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
class GameHub : Hub
{
    public GameHub
        (IRepository<Player> playerRepositpry,
        IRepository<Game> gameRepository,
        BlockingStrategyFactory factory,
        ChecksCollection checksCollection)
    {
        _playerRepository = playerRepositpry;
        _gameRepository = gameRepository;
        _factory = factory;
        _checksCollection = checksCollection;
    }
    private IRepository<Player> _playerRepository;
    private IRepository<Game> _gameRepository;
    private BlockingStrategyFactory _factory;
    private ChecksCollection _checksCollection;
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
        currentPLayer.IsWaiting = true;
        await _playerRepository.Update(currentPLayer);

        var waiters = _playerRepository.GetAll().Result.Where(p => p.IsWaiting).ToList();

        await Clients.All.SendAsync("askResponse",
            "Current Waiters List: " +
            string.Join("\n", waiters.Select(p => $"{p.PlayerId}: {p.FullName}")));

        if (waiters.Count() == 2)
        {
            List<Piece> newBoard = new BoardDirector(new CommonBoardBuilder()).GetPieces();

            GameDto gameDto = new GameDto(waiters[0].PlayerId, waiters[1].PlayerId,
                new Random().Next(1000000), newBoard);

            Game newGame = new Game(gameDto.GameId, gameDto.BPieceUserId, gameDto.WPieceUserId);
            newGame.Board = newBoard;

            await _gameRepository.Add(newGame);

            await AskServer("congratulations!");

            await Clients.All.SendAsync("gameStart",
            JsonSerializer.Serialize(gameDto));

            _playerRepository.GetAll().Result.Where(p => p.IsWaiting)
            .ToList()
            .ForEach(async p =>
            {
                Player playerIsntWaiting = await _playerRepository.FindById(p.PlayerId);
                playerIsntWaiting.IsWaiting = false;
                await _playerRepository.Update(playerIsntWaiting);
            });
        }
    }
    public async Task MakeMove(int gameId, string Move)
    {

        List<Piece> board = (await _gameRepository.FindById(gameId)).Board;

        Piece movingPiece = board.Find(p => p.PiecePosition == Move.Split('-')[0]);
        string moveTo = Move.Split('-')[1];

        bool moveAllowed = new MoveAllowed(board, _factory, 
            new ChecksCollection(board, movingPiece.PieceColor == PieceColor.White ? PieceColor.Black : PieceColor.White))
            .MoveAllowedRequest(movingPiece, moveTo);

        if (moveAllowed)
        {
            await Clients.All.SendAsync("newMove",
                JsonSerializer.Serialize(new MoveDto(gameId, Move)));
        }
        else
        {
            await Clients.All.SendAsync("moveWrong", gameId);
        }
    }
    public async Task EndGame(int gameId, int LoserId)
    {
        Game endedGame = await _gameRepository.FindById(gameId);

        if (endedGame == null)
        {
            System.Console.WriteLine("Request with incorrect game id.");
            return;
        }
        if (endedGame.WhitePlayerId == LoserId)
        {
            endedGame.WinnerColor = PieceColor.Black;
        }
        else
        {
            endedGame.WinnerColor = PieceColor.White;
        }

        endedGame.WinnerColor = LoserId == -1 ? null : endedGame.WinnerColor;

        await Clients.All.SendAsync("gameEnd", gameId);

        await _gameRepository.Update(endedGame);
    }
}
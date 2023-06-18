using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
class GameHub : Hub
{
    public GameHub
        (IRepository<Player> playerRepositpry,
        IRepository<Game> gameRepository,
        BlockingStrategyFactory factory)
    {
        _playerRepository = playerRepositpry;
        _gameRepository = gameRepository;
        _factory = factory;
    }
    private IRepository<Player> _playerRepository;
    private IRepository<Game> _gameRepository;
    private BlockingStrategyFactory _factory;
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
            List<Piece> newBoard = new BoardDirector(new CommonBoardBuilder()).GetBoard();

            GameDto gameDto = new GameDto(waiters[0].PlayerId, waiters[1].PlayerId,
                new Random().Next(1000000), newBoard);

            Game newGame = new Game(gameDto.GameId, gameDto.BPieceUserId, gameDto.WPieceUserId, JsonSerializer.Serialize(newBoard));

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
        bool moveAllowed = false;
        var game = await _gameRepository.FindById(gameId);
        Piece movingPiece = new Piece(PieceName.Pawn, "e2", PieceColor.Black);
        List<Piece> board = JsonSerializer.Deserialize<List<Piece>>(game.Board);
        if(Move.Contains('O'))
        {
            var castlingWorker = new CastlingWorker(board, Move, game.Moves);
            moveAllowed = castlingWorker.CastleIsPossible();
            if(moveAllowed)
            {
                board = castlingWorker.MakeCastle();
            }
        }
        else
        {
            movingPiece = board.Find(p => p.PiecePosition == Move.Split('-')[0]);
            if(movingPiece == null)
            {
                await Clients.All.SendAsync("moveWrong", gameId);
                return;
            }
            string moveTo = Move.Split('-')[1];

            moveAllowed = new MoveAllowed(board, _factory)
                .MoveAllowedRequest(movingPiece, moveTo);
            if(moveAllowed)
            {
                foreach (var a in board)
                {
                    System.Console.WriteLine(a.PieceName + " - " + a.PiecePosition);
                }
            }
        }


        if (moveAllowed)
        {   
            var samePosPieces = board.GroupBy(p => p.PiecePosition).Where(g => g.Count() >= 2);

            foreach (var samePieceGroup in samePosPieces)
            {
                foreach (var samePiece in samePieceGroup)
                {
                    if(samePiece.PieceColor != movingPiece.PieceColor)
                    {
                        board.Remove(samePiece);
                    }
                }
            }

            game.Board = JsonSerializer.Serialize(board);
            game.Moves += $"{Move}; ";
            
            await _gameRepository.Update(game);

            System.Console.WriteLine((await _gameRepository.FindById(gameId)).Moves);
            
            await Clients.All.SendAsync("newMove",
                JsonSerializer.Serialize(new MoveDto(gameId, board)));
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
    }
}
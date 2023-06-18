// public class BlockingStrategyFactory
// {
//     private readonly Dictionary<PieceName, Func<IPieceBlockingStrategy>> strategies;

//     public BlockingStrategyFactory()
//     {
//         strategies = new();
//     }
//     public IPieceBlockingStrategy CreateStrategy(PieceName pieceName) => strategies[pieceName]();

//     public IPieceBlockingStrategy this[PieceName pieceName]
//     {
//         get
//         {
//             return CreateStrategy(pieceName);
//         }
//     }

//     public PieceName[] RegisteredStrategies => strategies.Keys.ToArray();

//     public void RegisterStrategy(PieceName pieceName, Func<IPieceBlockingStrategy> createMethod)
//     {
//         strategies[pieceName] = createMethod;
//     }

// }class BoardDirector
// {
//     BoardBuilder builder;
//     public BoardDirector(BoardBuilder builder)
//     {
//         this.builder = builder;
//     }
//     public void Construct()
//     {
//         builder.BuildPawns();
//         builder.BuildKnights();
//         builder.BuildBishops();
//         builder.BuildRooks();
//         builder.BuildQueens();
//         builder.BuildKings();
//     }
//     public List<Piece> GetBoard()
//     {
//         Construct();
//         return builder.GetResult();
//     }
// }
// abstract class BoardBuilder
// {
//     public abstract void BuildPawns();
//     public abstract void BuildKnights();
//     public abstract void BuildBishops();
//     public abstract void BuildRooks();
//     public abstract void BuildQueens();
//     public abstract void BuildKings();
//     public abstract List<Piece> GetResult();
// }

// class CommonBoardBuilder : BoardBuilder
// {
//     List<Piece> board = new List<Piece>();
//     public override void BuildPawns()
//     {
//         for (char c = 'a'; c <= 'h'; c++)
//         {
//             board.Add(new Piece(PieceName.Pawn, $"{c.ToString()}2", PieceColor.White));
//         }
//         for (char c = 'a'; c <= 'h'; c++)
//         {
//             board.Add(new Piece(PieceName.Pawn, $"{c.ToString()}7", PieceColor.Black));
//         }
//     }
//     public override void BuildKnights()
//     {
//         board.Add(new Piece(PieceName.Knight, "b1", PieceColor.White));
//         board.Add(new Piece(PieceName.Knight, "g1", PieceColor.White));
//         board.Add(new Piece(PieceName.Knight, "b8", PieceColor.Black));
//         board.Add(new Piece(PieceName.Knight, "g8", PieceColor.Black));
//     }
//     public override void BuildBishops()
//     {
//         board.Add(new Piece(PieceName.Bishop, "c1", PieceColor.White));
//         board.Add(new Piece(PieceName.Bishop, "f1", PieceColor.White));
//         board.Add(new Piece(PieceName.Bishop, "c8", PieceColor.Black));
//         board.Add(new Piece(PieceName.Bishop, "f8", PieceColor.Black));
//     }
//     public override void BuildRooks()
//     {
//         board.Add(new Piece(PieceName.Rook, "a1", PieceColor.White));
//         board.Add(new Piece(PieceName.Rook, "h1", PieceColor.White));
//         board.Add(new Piece(PieceName.Rook, "a8", PieceColor.Black));
//         board.Add(new Piece(PieceName.Rook, "h8", PieceColor.Black));
//     }
//     public override void BuildQueens()
//     {
//         board.Add(new Piece(PieceName.Queen, "d1", PieceColor.White));
//         board.Add(new Piece(PieceName.Queen, "d8", PieceColor.Black));
//     }
//     public override void BuildKings()
//     {
//         board.Add(new Piece(PieceName.King, "e1", PieceColor.White));
//         board.Add(new Piece(PieceName.King, "e8", PieceColor.Black));
//     }
//     public override List<Piece> GetResult()
//     {
//         return board;
//     }
// }class CastlingWorker
// {
//     private List<Piece> _board;
//     private string _move;
//     private string _moves;
//     private PieceColor _castleColor;
//     private string _castleType;
//     public CastlingWorker(List<Piece> board, string move, string moves)
//     {
//         _board = board;
//         _move = move;
//         _moves = moves;
//         _castleColor = _move[0] == 'W' ? PieceColor.White : PieceColor.Black;
//         _castleType = move.Contains("O-O-O") ? "Long" : "Short";
//     }
//     public bool CastleIsPossible()
//     {
//         List<string> listMoves = _moves.Split("; ").ToList().Where(m => m.Length >= 2).ToList();

//         string[] wLongCPieces = { "b1", "c1", "d1" };
//         string[] wShortCPieces = { "f1", "g1" };

//         string[] bLongCPieces = { "b8", "c8", "d8" };
//         string[] bShortCPieces = { "f8", "g8" };

//         if (_castleColor == PieceColor.White)
//         {
//             if (listMoves.Find(m => m.Contains("e1")) is not null)
//             {
//                 return false;
//             }
//             if (_castleType == "Long")
//             {
//                 if (listMoves.Find(m => m.Contains("a1")) is not null)
//                 {
//                     return false;
//                 }
//                 if (_board.Find(p => wLongCPieces.Contains(p.PiecePosition)) is not null)
//                 {
//                     return false;
//                 }
//             }
//             else
//             {
//                 if (listMoves.Find(m => m.Contains("h1")) is not null)
//                 {
//                     return false;
//                 }
//                 if (_board.Find(p => wShortCPieces.Contains(p.PiecePosition)) is not null)
//                 {
//                     return false;
//                 }
//             }
//         }
//         else
//         {
//             if (listMoves.Find(m => m.Contains("e8")) is not null)
//             {
//                 return false;
//             }
//             if (_castleType == "Long")
//             {
//                 if (listMoves.Find(m => m.Contains("a8")) is not null)
//                 {
//                     return false;
//                 }
//                 if (_board.Find(p => bLongCPieces.Contains(p.PiecePosition)) is not null)
//                 {
//                     return false;
//                 }
//             }
//             else
//             {
//                 if (listMoves.Find(m => m.Contains("h8")) is not null)
//                 {
//                     return false;
//                 }
//                 if (_board.Find(p => bShortCPieces.Contains(p.PiecePosition)) is not null)
//                 {
//                     return false;
//                 }
//             }
//         }
//         System.Console.WriteLine("Castle is possible");
//         return true;
//     }
//     public List<Piece> MakeCastle()
//     {
//         string rank = _castleColor == PieceColor.White ? "1" : "8";

//         Piece King = _board.Find(p => p.PiecePosition == $"e{rank}");

//         Piece Rook = _castleType == "Long" ? _board.Find(p => p.PiecePosition == $"a{rank}")
//             : _board.Find(p => p.PiecePosition == $"h{rank}");

//         _board.Remove(King);
//         _board.Remove(Rook);

//         if (_castleType == "Long")
//         {
//             King.PiecePosition = $"c{rank}";
//             Rook.PiecePosition = $"d{rank}";
//         }
//         else
//         {
//             King.PiecePosition = $"g{rank}";
//             Rook.PiecePosition = $"f{rank}";
//         }
//         _board.Add(King);
//         _board.Add(Rook);
//         System.Console.WriteLine(String.Join(", ", _board.Select(p => $"{p.PieceName}-{p.PiecePosition}")));
//         return _board;
//     }
// }using System;
// using System.Collections;
// using System.Collections.Generic;

// public class CheckIterator : IEnumerator<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>>
// {
//     private int currentIndex;
//     private readonly List<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>> checkFunctions;

//     public CheckIterator()
//     {
//         currentIndex = -1;
//         checkFunctions = new List<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>>();
//     }

//     public Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece> Current => checkFunctions[currentIndex];

//     object IEnumerator.Current => Current;

//     public bool MoveNext()
//     {
//         currentIndex++;
//         return currentIndex < checkFunctions.Count;
//     }

//     public void Reset()
//     {
//         currentIndex = -1;
//     }
//     public void AddCheckFunction(Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece> checkFunction)
//     {
//         checkFunctions.Add(checkFunction);
//     }

//     public void Dispose()
//     {

//     }
// }

// public class ChecksCollection : IEnumerable<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>>
// {
//     public ChecksCollection() {}
    
//     public IEnumerator<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>> GetEnumerator()
//     {
//         CheckIterator iterator = new CheckIterator();

//         iterator.AddCheckFunction((board, kingColor, factory) => new GetDiagonalCheck().IsKingUnderCheck(board, kingColor, factory));
//         iterator.AddCheckFunction((board, kingColor, factory) => new GetLineCheck().IsKingUnderCheck(board, kingColor, factory));
//         iterator.AddCheckFunction((board, kingColor, factory) => new GetKnightCheck().IsKingUnderCheck(board, kingColor, factory));

//         return iterator;
//     }

//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return GetEnumerator();
//     }
// }using Microsoft.EntityFrameworkCore;

// public class ChessDataBase : DbContext
// {
//     public ChessDataBase(DbContextOptions<ChessDataBase> options) : base(options)
//     {
//         Database.EnsureCreated();
//     }
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Player>().HasData
//             (
//                 new Player(1, "Zhenyk", 1847),
//                 new Player(2, "Stepan", 3178),
//                 new Player(3, "Hikaru", 3023)
//             );
//     }
//     public DbSet<Player> Players { get; set; }
//     public DbSet<Game> Games { get; set; }
// }using System.ComponentModel.DataAnnotations.Schema;

// public class Game
// {
//     public int GameId { get; set; }
//     public int WhitePlayerId { get; set; }
//     public int BlackPlayerId { get; set; }
//     public string Moves { get; set; }
//     public string Board { get; set; }
//     public PieceColor? WinnerColor { get; set; }
//     public Game(int gameId, int whitePlayerId, int blackPlayerId, string board)
//     {
//         GameId = gameId;
//         WhitePlayerId = whitePlayerId;
//         BlackPlayerId = blackPlayerId;
//         Board = board;
//     }
// }class GameDto
// {
//     public int BPieceUserId { get; set; }
//     public int WPieceUserId { get; set; }
//     public int GameId { get; set; }
//     public List<Piece> Pieces { get; set; }

//     public GameDto(int wPieceUserId, int bPieceUserId, int gameId, List<Piece> pieces)
//     {
//         WPieceUserId = wPieceUserId;
//         BPieceUserId = bPieceUserId;
//         GameId = gameId;
//         Pieces = pieces;
//     }
// }using Microsoft.AspNetCore.SignalR;
// using Microsoft.EntityFrameworkCore;
// using System.Text.Json;
// class GameHub : Hub
// {
//     public GameHub
//         (IRepository<Player> playerRepositpry,
//         IRepository<Game> gameRepository,
//         BlockingStrategyFactory factory)
//     {
//         _playerRepository = playerRepositpry;
//         _gameRepository = gameRepository;
//         _factory = factory;
//     }
//     private IRepository<Player> _playerRepository;
//     private IRepository<Game> _gameRepository;
//     private BlockingStrategyFactory _factory;
//     public async Task AskServer(string text)
//     {
//         await Clients.All.SendAsync("askResponse", "Server response: \n" + text);
//     }
//     public async Task AddWaiter(string playerName, int playerId)
//     {
//         Player currentPLayer = await _playerRepository.FindById(playerId);

//         if (currentPLayer is null)
//         {
//             currentPLayer = new Player(playerId, playerName);
//             currentPLayer.IsWaiting = true;
//             await _playerRepository.Add(currentPLayer);
//         }
//         currentPLayer.IsWaiting = true;
//         await _playerRepository.Update(currentPLayer);

//         var waiters = _playerRepository.GetAll().Result.Where(p => p.IsWaiting).ToList();

//         await Clients.All.SendAsync("askResponse",
//             "Current Waiters List: " +
//             string.Join("\n", waiters.Select(p => $"{p.PlayerId}: {p.FullName}")));

//         if (waiters.Count() == 2)
//         {
//             List<Piece> newBoard = new BoardDirector(new CommonBoardBuilder()).GetBoard();

//             GameDto gameDto = new GameDto(waiters[0].PlayerId, waiters[1].PlayerId,
//                 new Random().Next(1000000), newBoard);

//             Game newGame = new Game(gameDto.GameId, gameDto.BPieceUserId, gameDto.WPieceUserId, JsonSerializer.Serialize(newBoard));

//             await _gameRepository.Add(newGame);
            
//             await AskServer("congratulations!");

//             await Clients.All.SendAsync("gameStart",
//             JsonSerializer.Serialize(gameDto));

//             _playerRepository.GetAll().Result.Where(p => p.IsWaiting)
//             .ToList()
//             .ForEach(async p =>
//             {
//                 Player playerIsntWaiting = await _playerRepository.FindById(p.PlayerId);
//                 playerIsntWaiting.IsWaiting = false;
//                 await _playerRepository.Update(playerIsntWaiting);
//             });
//         }
//     }
//     public async Task MakeMove(int gameId, string Move)
//     {
//         bool moveAllowed = false;
//         var game = await _gameRepository.FindById(gameId);
//         Piece movingPiece = new Piece(PieceName.Pawn, "e2", PieceColor.Black);
//         List<Piece> board = JsonSerializer.Deserialize<List<Piece>>(game.Board);
//         if(Move.Contains('O')) 
//         {
//             var castlingWorker = new CastlingWorker(board, Move, game.Moves);
//             moveAllowed = castlingWorker.CastleIsPossible();
//             if(moveAllowed)
//             {
//                 board = castlingWorker.MakeCastle();
//             }
//         }
//         else
//         {
//             movingPiece = board.Find(p => p.PiecePosition == Move.Split('-')[0]);
//             if(movingPiece == null)
//             {
//                 await Clients.All.SendAsync("moveWrong", gameId);
//                 return;
//             }
//             string moveTo = Move.Split('-')[1];

//             moveAllowed = new MoveAllowed(board, _factory)
//                 .MoveAllowedRequest(movingPiece, moveTo);
//             if(moveAllowed)
//             {
//                 foreach (var a in board)
//                 {
//                     System.Console.WriteLine(a.PieceName + " - " + a.PiecePosition);
//                 }
//             }
//         }


//         if (moveAllowed)
//         {   
//             var samePosPieces = board.GroupBy(p => p.PiecePosition).Where(g => g.Count() >= 2);

//             foreach (var samePieceGroup in samePosPieces)
//             {
//                 foreach (var samePiece in samePieceGroup)
//                 {
//                     if(samePiece.PieceColor != movingPiece.PieceColor)
//                     {
//                         board.Remove(samePiece);
//                     }
//                 }
//             }

//             game.Board = JsonSerializer.Serialize(board);
//             game.Moves += $"{Move}; ";
            
//             await _gameRepository.Update(game);

//             System.Console.WriteLine((await _gameRepository.FindById(gameId)).Moves);
            
//             await Clients.All.SendAsync("newMove",
//                 JsonSerializer.Serialize(new MoveDto(gameId, board)));
//         }
//         else
//         {
//             await Clients.All.SendAsync("moveWrong", gameId);
//         }
//     }
//     public async Task EndGame(int gameId, int LoserId)
//     {
//         Game endedGame = await _gameRepository.FindById(gameId);

//         if (endedGame == null)
//         {
//             System.Console.WriteLine("Request with incorrect game id.");
//             return;
//         }
//         if (endedGame.WhitePlayerId == LoserId)
//         {
//             endedGame.WinnerColor = PieceColor.Black;
//         }
//         else
//         {
//             endedGame.WinnerColor = PieceColor.White;
//         }

//         endedGame.WinnerColor = LoserId == -1 ? null : endedGame.WinnerColor;

//         await Clients.All.SendAsync("gameEnd", gameId);
//     }
// }public interface IGetChecksStrategy
// {
//     Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor, BlockingStrategyFactory factory);
// }

// public class GetDiagonalCheck : IGetChecksStrategy
// {
//     public Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor, BlockingStrategyFactory factory)
//     {
//         MoveAllowed moveAllowed = new MoveAllowed(board, factory);
//         System.Console.WriteLine("Board for check if king under check: ");
//         System.Console.WriteLine(String.Join(" ", board.Select(p => $"{p.PieceName} - {p.PiecePosition}")));
//         string kingPosition = board.Find(p => p.PieceName == PieceName.King && p.PieceColor == kingColor).PiecePosition;

//         System.Console.WriteLine("King position: " + kingPosition);

//         List<Piece> piecesToCheck = board
//             .Where(p => p.PieceColor != kingColor && p.PieceName != PieceName.King)
//             .Where(p => p.PieceName == PieceName.Bishop || p.PieceName == PieceName.Queen || p.PieceName == PieceName.Pawn)
//             .ToList();
//         System.Console.WriteLine($"Pieces to check: {String.Join(';', piecesToCheck.Select(p => p.PieceName + p.PiecePosition))}");
//         foreach (Piece piece in piecesToCheck)
//         {
//             if(moveAllowed.CheckAllowedRequest(piece, kingPosition))
//             {
//                 System.Console.WriteLine($"move allowed: {piece.PieceName}: {piece.PiecePosition}-{kingPosition}");
                
//                 System.Console.WriteLine($"Piece is not blocking? ({piece.PieceName}, {piece.PiecePosition} - {kingPosition})");
//                 if (factory[piece.PieceName].PieceIsNotBlocking(piece, kingPosition, board))
//                 {
//                     System.Console.WriteLine($"piece not blocking: {piece}; {kingPosition}");
//                     return piece;
//                 }
//             }
//         }
//         return null;
//     }

// }
// public class GetLineCheck : IGetChecksStrategy
// {
//     public Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor, BlockingStrategyFactory factory)
//     {
//         MoveAllowed moveAllowed = new MoveAllowed(board, factory);
//         string kingPosition = board.Find(p => p.PieceName == PieceName.King).PiecePosition;

//         List<Piece> piecesToCheck = board
//             .Where(p => p.PieceColor != kingColor && p.PieceName != PieceName.King)
//             .Where(p => p.PieceName == PieceName.Rook || p.PieceName == PieceName.Queen)
//             .ToList();
        
//         foreach (Piece piece in piecesToCheck)
//         {
//             if(moveAllowed.CheckAllowedRequest(piece, kingPosition))
//             {
//                 System.Console.WriteLine($"move allowed: {piece.PieceName}: {piece.PiecePosition}-{kingPosition}");
                
//                 if (factory[piece.PieceName].PieceIsNotBlocking(piece, kingPosition, board))
//                 {
//                     System.Console.WriteLine($"piece noot blocking: {piece}; {kingPosition}");
//                     return piece;
//                 }
//             }
//         }
//         return null;
//     }
// }
// public class GetKnightCheck : IGetChecksStrategy
// {
//     public Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor, BlockingStrategyFactory factory)
//     {
//         MoveAllowed moveAllowed = new MoveAllowed(board, factory);
//         string kingPosition = board.Find(p => p.PieceName == PieceName.King).PiecePosition;

//         List<Piece> piecesToCheck = board
//             .Where(p => p.PieceColor != kingColor && p.PieceName != PieceName.King)
//             .Where(p => p.PieceName == PieceName.Knight)
//             .ToList();
        
//         foreach (Piece piece in piecesToCheck)
//         {
//             if(moveAllowed.CheckAllowedRequest(piece, kingPosition))
//             {
//                 System.Console.WriteLine($"move allowed: {piece.PieceName}: {piece.PiecePosition}-{kingPosition}");
                
//                 if (factory[piece.PieceName].PieceIsNotBlocking(piece, kingPosition, board))
//                 {
//                     System.Console.WriteLine($"piece not blocking: {piece}; {kingPosition}");
//                     return piece;
//                 }
//             }
//         }
//         return null;
//     }
// }public interface IPieceBlockingStrategy
// {
//     bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board);
// }
// public class PawnIsNotBlockingStrategy : IPieceBlockingStrategy
// {
//     public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
//     {
//         if (piece.PiecePosition[0] != move[0])
//         {
//             if (board.Find(p => p.PiecePosition == move && p.PieceColor == piece.PieceColor) is not null)
//             {
//                 return false;
//             }
//             return true;
//         }
//         else
//         {
//             if (piece.PieceColor == PieceColor.White)
//             {
//                 for (int i = piece.PiecePosition[1] + 1; i <= move[1]; i++)
//                 {
//                     if (board.Find(p => p.PiecePosition[1] == i && p.PiecePosition[0] == piece.PiecePosition[0]) is not null)
//                     {
//                         return false;
//                     }
//                 }
//             }
//             else
//             {
//                 for (int i = piece.PiecePosition[1] - 1; i >= move[1]; i--)
//                 {
//                     if (board.Find(p => p.PiecePosition[1] == i && p.PiecePosition[0] == piece.PiecePosition[0]) is not null)
//                     {
//                         return false;
//                     }
//                 }
//             }
//         }
//         return true;
//     }
// }
// public class KnightIsNotBlockingStrategy : IPieceBlockingStrategy
// {
//     public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
//     {
//         if (board.Find(p => p.PieceColor == piece.PieceColor && p.PiecePosition == move) is not null)
//         {
//             return false;
//         }
//         return true;
//     }
// }
// public class RookIsNotBlockingStrategy : IPieceBlockingStrategy
// {
//     public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
//     {
//         int startPoint = piece.PiecePosition[0];
//         var sycleCond = (int i) => (i < piece.PiecePosition[0], i);

//         if (move[0] == piece.PiecePosition[0])
//         {
//             if (move[1] > piece.PiecePosition[1])
//             {
//                 sycleCond = (i) => (i <= move[1], 1);
//                 startPoint = piece.PiecePosition[1] + 1;
//             }
//             else
//             {
//                 sycleCond = (i) => (i >= move[1], -1);
//                 startPoint = piece.PiecePosition[1] - 1;
//             }
//         }
//         else
//         {
//             if (move[0] > piece.PiecePosition[0])
//             {
//                 sycleCond = (i) => (i <= move[0], 1);
//                 startPoint = piece.PiecePosition[0] + 1;
//             }
//             else
//             {
//                 sycleCond = (i) => (i >= move[0], -1);
//                 startPoint = piece.PiecePosition[0] - 1;
//             }
//         }
//         for (int i = startPoint; sycleCond.Invoke(i).Item1; i += sycleCond(i).Item2)
//         {
//             Piece blockingPiece = new Piece(PieceName.Pawn, "", PieceColor.White);
//             if (move[0] == piece.PiecePosition[0])
//             {
//                 blockingPiece = board.Find(p => p.PiecePosition == $"{(char)piece.PiecePosition[0]}{(char)(i)}");
//             }
//             else
//             {
//                 blockingPiece = board.Find(p => p.PiecePosition == $"{(char)(i)}{(char)piece.PiecePosition[1]}");
//             }
//             if (blockingPiece is not null)
//             {
//                 if (blockingPiece.PiecePosition == move && piece.PieceColor != blockingPiece.PieceColor)
//                 {
//                     continue;
//                 }
//                 return false;
//             }
//         }
//         return true;

//     }
// }
// public class BishopIsNotBlockingStrategy : IPieceBlockingStrategy
// {
//     public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
//     {
//         for (int i = 1; i <= Math.Abs(move[0] - piece.PiecePosition[0]); i++)
//         {
//             int k = move[0] < piece.PiecePosition[0] ? -1 : 1;
//             int n = move[1] < piece.PiecePosition[1] ? -1 : 1;

//             int hc = piece.PiecePosition[0] + k * i;
//             int vc = piece.PiecePosition[1] + n * i;
//             Piece blockingPiece = board.Find(p => p.PiecePosition[0] == (char)hc && p.PiecePosition[1] == (char)vc);

//             if (blockingPiece is not null)
//             {
//                 if (blockingPiece.PiecePosition == move && piece.PieceColor != blockingPiece.PieceColor)
//                 {
//                     continue;
//                 }
//                 return false;
//             }
//         }
//         return true;
//     }
// }
// public class QueenIsNotBlockingStrategy : IPieceBlockingStrategy
// {
//     public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
//     {
//         if (piece.PiecePosition[0] != move[0] && piece.PiecePosition[1] != move[1])
//         {
//             return new BishopIsNotBlockingStrategy().PieceIsNotBlocking(piece, move, board);
//         }
//         else
//         {
//             return new RookIsNotBlockingStrategy().PieceIsNotBlocking(piece, move, board);
//         }
//     }
// }
// public class KingIsNotBlockingStrategy : IPieceBlockingStrategy
// {
//     public bool PieceIsNotBlocking(Piece piece, string move, List<Piece> board)
//     {
//         return board.Find(p => p.PieceColor == piece.PieceColor && p.PiecePosition == move) is null;
//     }
// }
// public interface IRepository<TEntity> where TEntity : class
// {
//     Task<TEntity> FindById(int id);

//     Task<IEnumerable<TEntity>> GetAll();

//     Task Remove(TEntity entity);

//     Task<TEntity> Add(TEntity entity);

//     Task<TEntity> Update(TEntity entity);
// }class KingUnderCheck
// {
//     private List<Piece> _checks;
//     private List<Piece> _board;
//     public KingUnderCheck(List<Piece> checks, List<Piece> board)
//     {
//         _checks = checks;
//         _board = board;
//     }
//     public bool MateOnBoard()
//     {
//         Piece king = _board.Find(p => p.PieceName == PieceName.King && p.PieceColor != _checks[0].PieceColor);

//         bool onlyKingMoves = false;

//         if (_checks.Count > 1 || _checks.Exists(p => p.PieceName == PieceName.Knight))
//         {
//             onlyKingMoves = true;
//         }

//         return false;
//     }

//     private void CalculateKingsMove()
//     {

//     }
//     private void CalculateAllMoves(Piece checker, Piece king)
//     {
//         if (checker.PieceName == PieceName.Bishop)
//         {

//         }
//     }
// }public interface ISubject
// {
//     bool MoveAllowedRequest(Piece piece, string move);
// }
// class MoveAllowed : ISubject
// {
//     public MoveAllowed(List<Piece> board, BlockingStrategyFactory factory)
//     {
//         _factory = factory;
//         _board = board;
//     }
//     private MoveHandler _moveHandler;
//     protected BlockingStrategyFactory _factory;
//     protected List<Piece> _board;

//     public bool MoveAllowedRequest(Piece piece, string move)
//     {
//         if (CheckAccess(piece, move))
//         {

//             System.Console.WriteLine($"Piece can move to {move}");
//             _moveHandler = new BlockingPieceHandler(_board, _factory);

//             _moveHandler.SetNextHandler(new KingSafetyHandler(_board, _factory));

//             return _moveHandler.HandleMove(piece, move);
//         }
//         else
//         {
//             System.Console.WriteLine($"{piece.PieceName.ToString()} cannot move this way");
//             return false;
//         }
//     }
//     public bool CheckAllowedRequest(Piece piece, string move)
//     {
//         return CheckAccess(piece, move);
//     }
//     private bool CheckAccess(Piece piece, string move)
//     {
//         List<string> possibleMoves = new List<string>();

//         Func<char, char, string> getMoveStr = (char c1, char c2) => Char.ConvertFromUtf32(c1) + Char.ConvertFromUtf32(c2);

//         if (piece.PieceName == PieceName.Pawn)
//         {
//             int nextRank = piece.PieceColor == PieceColor.White ? 1 : -1;

//             possibleMoves.Add(getMoveStr(piece.PiecePosition[0], (char)(piece.PiecePosition[1] + nextRank)));
//             possibleMoves.Add(getMoveStr((char)(piece.PiecePosition[0] + 1), (char)(piece.PiecePosition[1] + nextRank)));
//             possibleMoves.Add(getMoveStr((char)(piece.PiecePosition[0] - 1), (char)(piece.PiecePosition[1] + nextRank)));
//             if (piece.PieceColor == PieceColor.White && piece.PiecePosition[1] == '2')
//             {
//                 possibleMoves.Add(getMoveStr(piece.PiecePosition[0], '4'));
//             }
//             else if (piece.PieceColor == PieceColor.Black && piece.PiecePosition[1] == '7')
//             {
//                 possibleMoves.Add(getMoveStr(piece.PiecePosition[0], '5'));
//             }
//         }
//         else if (piece.PieceName == PieceName.Knight)
//         {
//             int file = piece.PiecePosition[0] - 'a';
//             int rank = piece.PiecePosition[1] - '1';

//             int[] dx = { -2, -2, -1, -1, 1, 1, 2, 2 };
//             int[] dy = { -1, 1, -2, 2, -2, 2, -1, 1 };

//             for (int i = 0; i < 8; i++)
//             {
//                 int nx = file + dx[i];
//                 int ny = rank + dy[i];

//                 possibleMoves.Add($"{(char)('a' + nx)}{(char)('1' + ny)}");
//             }
//         }
//         else if (piece.PieceName == PieceName.Bishop)
//         {
//             GenerateDiagonalMoves(piece.PiecePosition).ForEach(m => possibleMoves.Add(m));
//         }
//         else if (piece.PieceName == PieceName.Rook)
//         {
//             GenerateLineMoves(piece.PiecePosition).ForEach(m => possibleMoves.Add(m));
//         }
//         else if (piece.PieceName == PieceName.King)
//         {
//             int file = piece.PiecePosition[0] - 'a';
//             int rank = piece.PiecePosition[1] - '1';

//             int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
//             int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

//             for (int i = 0; i < 8; i++)
//             {
//                 int nx = file + dx[i];
//                 int ny = rank + dy[i];
//                 char newFile = (char)('a' + nx);
//                 char newRank = (char)('1' + ny);

//                 string newPosition = $"{newFile}{newRank}";
//                 possibleMoves.Add(newPosition);
//             }
//         }
//         else if (piece.PieceName == PieceName.Queen)
//         {
//             GenerateDiagonalMoves(piece.PiecePosition).ForEach(m => possibleMoves.Add(m));
//             GenerateLineMoves(piece.PiecePosition).ForEach(m => possibleMoves.Add(m));
//         }

//         possibleMoves = possibleMoves.FindAll(m =>
//         {
//             return m[0] <= 'h' && m[0] >= 'a'
//                 && m[1] >= '1' && m[1] <= '8'
//                 && m.Length == 2;
//         });
//         System.Console.WriteLine($"Possible move for {piece.PieceName}, {piece.PiecePosition}" + String.Join("; ", possibleMoves));
//         return possibleMoves.Contains(move);
//     }
//     private List<string> GenerateDiagonalMoves(string position)
//     {
//         List<string> possibleMoves = new List<string>();
//         int file = position[0] - 'a';
//         int rank = position[1] - '1';

//         int[] dx = { -1, -1, 1, 1 };
//         int[] dy = { -1, 1, -1, 1 };

//         for (int i = 0; i < 4; i++)
//         {
//             for (int j = 1; j < 8; j++)
//             {
//                 int nx = file + j * dx[i];
//                 int ny = rank + j * dy[i];

//                 possibleMoves.Add($"{(char)('a' + nx)}{(char)('1' + ny)}");
//             }
//         }

//         return possibleMoves;
//     }
//     private List<string> GenerateLineMoves(string position)
//     {
//         List<string> possibleMoves = new List<string>();
//         int file = position[0] - 'a';
//         int rank = position[1] - '1';

//         int[] dx = { -1, 1, 0, 0 };
//         int[] dy = { 0, 0, -1, 1 };

//         for (int i = 0; i < 4; i++)
//         {
//             for (int j = 1; j < 8; j++)
//             {
//                 int nx = file + j * dx[i];
//                 int ny = rank + j * dy[i];

//                 possibleMoves.Add($"{(char)('a' + nx)}{(char)('1' + ny)}");
//             }
//         }

//         return possibleMoves;
//     }
// }class MoveDto
// {
//     public int GameId { get; set; }
//     public List<Piece> Pieces { get; set; }

//     public MoveDto(int gameId,  List<Piece> pieces)
//     {
//         GameId = gameId;
//         Pieces = pieces;
//     }
// }public abstract class MoveHandler : ISubject
// {
//     public MoveHandler(List<Piece> board)
//     {
//         _board = board;
//     }
//     protected MoveHandler _nextHandler;
//     protected List<Piece> _board;

//     public void SetNextHandler(MoveHandler handler)
//     {
//         _nextHandler = handler;
//     }

//     public virtual bool HandleMove(Piece piece, string move)
//     {
//         if (_nextHandler != null)
//         {
//             return _nextHandler.HandleMove(piece, move);
//         }
//         else
//         {
//             return true;
//         }
//     }

//     public virtual bool MoveAllowedRequest(Piece piece, string move)
//     {
//         return HandleMove(piece, move);
//     }
// }
// public class BlockingPieceHandler : MoveHandler
// {
//     private BlockingStrategyFactory _factory;
//     public BlockingPieceHandler(List<Piece> board, BlockingStrategyFactory factory) : base(board)
//     {
//         _factory = factory;
//     }
//     public override bool HandleMove(Piece piece, string move)
//     {
//         if (_factory[piece.PieceName].PieceIsNotBlocking(piece, move, _board))
//         {
//             if (_nextHandler != null)
//             {
//                 return _nextHandler.HandleMove(piece, move);
//             }
//             else
//             {
//                 System.Console.WriteLine("Piece is not blocking.");
//                 return true;
//             }
//         }
//         else
//         {
//             System.Console.WriteLine("Piece is blocking.");
//             return false;
//         }
//     }

//     public override bool MoveAllowedRequest(Piece piece, string move)
//     {
//         return base.MoveAllowedRequest(piece, move);
//     }
// }
// public class KingSafetyHandler : MoveHandler
// {
//     private BlockingStrategyFactory _factory;
//     private ChecksCollection _collection;
//     public KingSafetyHandler(List<Piece> board, BlockingStrategyFactory factory) : base(board)
//     {
//         _factory = factory;
//     }
//     public override bool HandleMove(Piece piece, string move)
//     {
//         List<Piece> tempBoard = _board;

//         tempBoard.Remove(piece);
//         piece.PiecePosition = move;
//         tempBoard.Add(piece);

//         _collection = new ChecksCollection();

//         using (var a = _collection.GetEnumerator())
//         {
//             while (a.MoveNext())
//             {
//                 var checker = a.Current.Invoke(tempBoard, piece.PieceColor, _factory);
//                 if (checker is not null)
//                 {
//                     System.Console.WriteLine($"King is under a check. {checker.PieceName} {checker.PiecePosition}");
//                     return false;
//                 }
//             }
//         }
//         if (_nextHandler is not null)
//         {
//             return _nextHandler.HandleMove(piece, move);
//         }
//         else
//         {
//             System.Console.WriteLine("King is safe.");
//             return true;
//         }
//     }

//     public override bool MoveAllowedRequest(Piece piece, string move)
//     {
//         return base.MoveAllowedRequest(piece, move);
//     }
// }using System.Text.Json.Serialization;

// public class Piece
// {
//     [JsonConverter(typeof(JsonStringEnumConverter))]
//     public PieceName PieceName { get; set; }
    
//     public string PiecePosition { get; set; }
    
//     [JsonConverter(typeof(JsonStringEnumConverter))]
//     public PieceColor PieceColor { get; set; }

//     public Piece(PieceName pieceName, string piecePosition, PieceColor pieceColor)
//     {
//         PieceName = pieceName;
//         PiecePosition = piecePosition;
//         PieceColor = pieceColor;
//     }
// }

// public enum PieceName {
//     Pawn,
//     Knight, 
//     Bishop,
//     Rook,
//     Queen,
//     King
// }
// public enum PieceColor{
//     White,
//     Black
// }public class Player
// {
//     public int PlayerId { get; set; }
//     public string FullName { get; set; }
//     public List<Game> Games { get; set; }
//     public int Rating { get; set; }
//     public bool IsWaiting { get; set; }

//     public Player(int playerId, string fullName, int rating = 1500)
//     {
//         PlayerId = playerId;
//         FullName = fullName;
//         Rating = rating;
//         IsWaiting = false;
//         Games = new List<Game>();
//     }
// }using Microsoft.EntityFrameworkCore;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllersWithViews();

// builder.Services.AddDbContext<ChessDataBase>((db) => db.UseInMemoryDatabase("Chess"));

// builder.Services.AddScoped<IRepository<Player>, Repository<Player>>();
// builder.Services.AddScoped<IRepository<Game>, Repository<Game>>();
// builder.Services.AddSingleton<BlockingStrategyFactory>(_ => {

//     BlockingStrategyFactory factory = new BlockingStrategyFactory();

//     factory.RegisterStrategy(PieceName.Pawn, () => new PawnIsNotBlockingStrategy());
//     factory.RegisterStrategy(PieceName.Knight, () => new KnightIsNotBlockingStrategy());
//     factory.RegisterStrategy(PieceName.Bishop, () => new BishopIsNotBlockingStrategy());
//     factory.RegisterStrategy(PieceName.Rook, () => new RookIsNotBlockingStrategy());
//     factory.RegisterStrategy(PieceName.Queen, () => new QueenIsNotBlockingStrategy());
//     factory.RegisterStrategy(PieceName.King, () => new KingIsNotBlockingStrategy());

//     return factory;
// });

// builder.Services.AddCors(options => {
//     options.AddPolicy("AllowAllHeaders", 
//     builder => 
//     {
//         builder
//             .AllowAnyOrigin()
//             .AllowAnyHeader()
//             .AllowAnyMethod();
//     });
// });

// builder.Services.AddSignalR(options => {
//     options.EnableDetailedErrors = true;
// });

// var app = builder.Build();

// if (!app.Environment.IsDevelopment())
// {
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();

// app.UseEndpoints(endpoints => 
// {
//     endpoints.MapControllers();
//     endpoints.MapHub<GameHub>("/chess");
// });

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller}/{action=Index}/{id?}");

// app.MapFallbackToFile("index.html");

// app.Run();
 
// using Microsoft.EntityFrameworkCore;

// public class Repository<T> : IRepository<T> where T : class
//     {
//         private ChessDataBase _dbContext;
//         private DbSet<T> _table;
//         public Repository(ChessDataBase context)
//         {   
//             _dbContext = context;
//             _table = _dbContext.Set<T>();
//         }

//         public async Task<T> Add(T entity)
//         {
//             await _table.AddAsync(entity);
//             await _dbContext.SaveChangesAsync();
//             return entity;
//         }

//         public async Task<T> FindById(int id)
//         {
//             return await _table.FindAsync(id);
//         }

//         public async Task<IEnumerable<T>> GetAll()
//         {
//             return await _table.ToListAsync();
//         }

//         public async Task Remove(T entity)
//         {
//             _table.Remove(entity);
//             await _dbContext.SaveChangesAsync();
//         }

//         public async Task<T> Update(T entity)
//         {
//             _table.Update(entity);
//             await _dbContext.SaveChangesAsync();
//             return entity;
//         }
//     }
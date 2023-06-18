public interface ISubject
{
    bool MoveAllowedRequest(Piece piece, string move);
}
class MoveAllowed : ISubject
{
    public MoveAllowed(List<Piece> board, BlockingStrategyFactory factory)
    {
        _factory = factory;
        _board = board;
    }
    private MoveHandler _moveHandler;
    protected BlockingStrategyFactory _factory;
    protected List<Piece> _board;

    public bool MoveAllowedRequest(Piece piece, string move)
    {
        if (CheckAccess(piece, move))
        {

            System.Console.WriteLine($"Piece can move to {move}");
            
            _moveHandler = new BlockingPieceHandler(_board, _factory);

            _moveHandler.SetNextHandler(new KingSafetyHandler(_board, _factory));

            return _moveHandler.HandleMove(piece, move);
        }
        else
        {
            System.Console.WriteLine($"{piece.PieceName.ToString()} cannot move this way");
            return false;
        }
    }
    public bool CheckAllowedRequest(Piece piece, string move)
    {
        return CheckAccess(piece, move);
    }
    private bool CheckAccess(Piece piece, string move)
    {
        List<string> possibleMoves = new List<string>();

        Func<char, char, string> getMoveStr = (char c1, char c2) => Char.ConvertFromUtf32(c1) + Char.ConvertFromUtf32(c2);

        if (piece.PieceName == PieceName.Pawn)
        {
            int nextRank = piece.PieceColor == PieceColor.White ? 1 : -1;

            possibleMoves.Add(getMoveStr(piece.PiecePosition[0], (char)(piece.PiecePosition[1] + nextRank)));
            possibleMoves.Add(getMoveStr((char)(piece.PiecePosition[0] + 1), (char)(piece.PiecePosition[1] + nextRank)));
            possibleMoves.Add(getMoveStr((char)(piece.PiecePosition[0] - 1), (char)(piece.PiecePosition[1] + nextRank)));
            if (piece.PieceColor == PieceColor.White && piece.PiecePosition[1] == '2')
            {
                possibleMoves.Add(getMoveStr(piece.PiecePosition[0], '4'));
            }
            else if (piece.PieceColor == PieceColor.Black && piece.PiecePosition[1] == '7')
            {
                possibleMoves.Add(getMoveStr(piece.PiecePosition[0], '5'));
            }
        }
        else if (piece.PieceName == PieceName.Knight)
        {
            int file = piece.PiecePosition[0] - 'a';
            int rank = piece.PiecePosition[1] - '1';

            int[] dx = { -2, -2, -1, -1, 1, 1, 2, 2 };
            int[] dy = { -1, 1, -2, 2, -2, 2, -1, 1 };

            for (int i = 0; i < 8; i++)
            {
                int nx = file + dx[i];
                int ny = rank + dy[i];

                possibleMoves.Add($"{(char)('a' + nx)}{(char)('1' + ny)}");
            }
        }
        else if (piece.PieceName == PieceName.Bishop)
        {
            GenerateDiagonalMoves(piece.PiecePosition).ForEach(m => possibleMoves.Add(m));
        }
        else if (piece.PieceName == PieceName.Rook)
        {
            GenerateLineMoves(piece.PiecePosition).ForEach(m => possibleMoves.Add(m));
        }
        else if (piece.PieceName == PieceName.King)
        {
            int file = piece.PiecePosition[0] - 'a';
            int rank = piece.PiecePosition[1] - '1';

            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int nx = file + dx[i];
                int ny = rank + dy[i];
                char newFile = (char)('a' + nx);
                char newRank = (char)('1' + ny);

                string newPosition = $"{newFile}{newRank}";
                possibleMoves.Add(newPosition);
            }
        }
        else if (piece.PieceName == PieceName.Queen)
        {
            GenerateDiagonalMoves(piece.PiecePosition).ForEach(m => possibleMoves.Add(m));
            GenerateLineMoves(piece.PiecePosition).ForEach(m => possibleMoves.Add(m));
        }

        possibleMoves = possibleMoves.FindAll(m =>
        {
            return m[0] <= 'h' && m[0] >= 'a'
                && m[1] >= '1' && m[1] <= '8'
                && m.Length == 2;
        });
        System.Console.WriteLine($"Possible move for {piece.PieceName}, {piece.PiecePosition}" + String.Join("; ", possibleMoves));
        return possibleMoves.Contains(move);
    }
    private List<string> GenerateDiagonalMoves(string position)
    {
        List<string> possibleMoves = new List<string>();
        int file = position[0] - 'a';
        int rank = position[1] - '1';

        int[] dx = { -1, -1, 1, 1 };
        int[] dy = { -1, 1, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j < 8; j++)
            {
                int nx = file + j * dx[i];
                int ny = rank + j * dy[i];

                possibleMoves.Add($"{(char)('a' + nx)}{(char)('1' + ny)}");
            }
        }

        return possibleMoves;
    }
    private List<string> GenerateLineMoves(string position)
    {
        List<string> possibleMoves = new List<string>();
        int file = position[0] - 'a';
        int rank = position[1] - '1';

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j < 8; j++)
            {
                int nx = file + j * dx[i];
                int ny = rank + j * dy[i];

                possibleMoves.Add($"{(char)('a' + nx)}{(char)('1' + ny)}");
            }
        }

        return possibleMoves;
    }
}
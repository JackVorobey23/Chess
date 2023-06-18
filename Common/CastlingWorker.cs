class CastlingWorker
{
    private List<Piece> _board;
    private string _move;
    private string _moves;
    private PieceColor _castleColor;
    private string _castleType;
    public CastlingWorker(List<Piece> board, string move, string moves)
    {
        _board = board;
        _move = move;
        _moves = moves;
        _castleColor = _move[0] == 'W' ? PieceColor.White : PieceColor.Black;
        _castleType = move.Contains("O-O-O") ? "Long" : "Short";
    }
    public bool CastleIsPossible()
    {
        List<string> listMoves = _moves.Split("; ").ToList().Where(m => m.Length >= 2).ToList();

        string[] wLongCPieces = { "b1", "c1", "d1" };
        string[] wShortCPieces = { "f1", "g1" };

        string[] bLongCPieces = { "b8", "c8", "d8" };
        string[] bShortCPieces = { "f8", "g8" };

        if (_castleColor == PieceColor.White)
        {
            if (listMoves.Find(m => m.Contains("e1")) is not null)
            {
                return false;
            }
            if (_castleType == "Long")
            {
                if (listMoves.Find(m => m.Contains("a1")) is not null)
                {
                    return false;
                }
                if (_board.Find(p => wLongCPieces.Contains(p.PiecePosition)) is not null)
                {
                    return false;
                }
            }
            else
            {
                if (listMoves.Find(m => m.Contains("h1")) is not null)
                {
                    return false;
                }
                if (_board.Find(p => wShortCPieces.Contains(p.PiecePosition)) is not null)
                {
                    return false;
                }
            }
        }
        else
        {
            if (listMoves.Find(m => m.Contains("e8")) is not null)
            {
                return false;
            }
            if (_castleType == "Long")
            {
                if (listMoves.Find(m => m.Contains("a8")) is not null)
                {
                    return false;
                }
                if (_board.Find(p => bLongCPieces.Contains(p.PiecePosition)) is not null)
                {
                    return false;
                }
            }
            else
            {
                if (listMoves.Find(m => m.Contains("h8")) is not null)
                {
                    return false;
                }
                if (_board.Find(p => bShortCPieces.Contains(p.PiecePosition)) is not null)
                {
                    return false;
                }
            }
        }
        System.Console.WriteLine("Castle is possible");
        return true;
    }
    public List<Piece> MakeCastle()
    {
        string rank = _castleColor == PieceColor.White ? "1" : "8";

        Piece King = _board.Find(p => p.PiecePosition == $"e{rank}");

        Piece Rook = _castleType == "Long" ? _board.Find(p => p.PiecePosition == $"a{rank}")
            : _board.Find(p => p.PiecePosition == $"h{rank}");

        _board.Remove(King);
        _board.Remove(Rook);

        if (_castleType == "Long")
        {
            King.PiecePosition = $"c{rank}";
            Rook.PiecePosition = $"d{rank}";
        }
        else
        {
            King.PiecePosition = $"g{rank}";
            Rook.PiecePosition = $"f{rank}";
        }
        _board.Add(King);
        _board.Add(Rook);
        System.Console.WriteLine(String.Join(", ", _board.Select(p => $"{p.PieceName}-{p.PiecePosition}")));
        return _board;
    }
}
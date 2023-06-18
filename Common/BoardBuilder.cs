class BoardDirector
{
    BoardBuilder builder;
    public BoardDirector(BoardBuilder builder)
    {
        this.builder = builder;
    }
    public void Construct()
    {
        builder.BuildPawns();
        builder.BuildKnights();
        builder.BuildBishops();
        builder.BuildRooks();
        builder.BuildQueens();
        builder.BuildKings();
    }
    public List<Piece> GetBoard()
    {
        Construct();
        return builder.GetResult();
    }
}
abstract class BoardBuilder
{
    public abstract void BuildPawns();
    public abstract void BuildKnights();
    public abstract void BuildBishops();
    public abstract void BuildRooks();
    public abstract void BuildQueens();
    public abstract void BuildKings();
    public abstract List<Piece> GetResult();
}

class CommonBoardBuilder : BoardBuilder
{
    List<Piece> board = new List<Piece>();
    public override void BuildPawns()
    {
        for (char c = 'a'; c <= 'h'; c++)
        {
            board.Add(new Piece(PieceName.Pawn, $"{c.ToString()}2", PieceColor.White));
        }
        for (char c = 'a'; c <= 'h'; c++)
        {
            board.Add(new Piece(PieceName.Pawn, $"{c.ToString()}7", PieceColor.Black));
        }
    }
    public override void BuildKnights()
    {
        board.Add(new Piece(PieceName.Knight, "b1", PieceColor.White));
        board.Add(new Piece(PieceName.Knight, "g1", PieceColor.White));
        board.Add(new Piece(PieceName.Knight, "b8", PieceColor.Black));
        board.Add(new Piece(PieceName.Knight, "g8", PieceColor.Black));
    }
    public override void BuildBishops()
    {
        board.Add(new Piece(PieceName.Bishop, "c1", PieceColor.White));
        board.Add(new Piece(PieceName.Bishop, "f1", PieceColor.White));
        board.Add(new Piece(PieceName.Bishop, "c8", PieceColor.Black));
        board.Add(new Piece(PieceName.Bishop, "f8", PieceColor.Black));
    }
    public override void BuildRooks()
    {
        board.Add(new Piece(PieceName.Rook, "a1", PieceColor.White));
        board.Add(new Piece(PieceName.Rook, "h1", PieceColor.White));
        board.Add(new Piece(PieceName.Rook, "a8", PieceColor.Black));
        board.Add(new Piece(PieceName.Rook, "h8", PieceColor.Black));
    }
    public override void BuildQueens()
    {
        board.Add(new Piece(PieceName.Queen, "d1", PieceColor.White));
        board.Add(new Piece(PieceName.Queen, "d8", PieceColor.Black));
    }
    public override void BuildKings()
    {
        board.Add(new Piece(PieceName.King, "e1", PieceColor.White));
        board.Add(new Piece(PieceName.King, "e8", PieceColor.Black));
    }
    public override List<Piece> GetResult()
    {
        return board;
    }
}
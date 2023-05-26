class Client
{
    void Main()
    {
        BoardBuilder builder = new CommonBoardBuilder();
        Director director = new Director(builder);
        director.Construct();
        Board product = builder.GetResult();
    }
}
class Director
{
    BoardBuilder builder;
    public Director(BoardBuilder builder)
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
}

abstract class BoardBuilder
{
    public abstract void BuildPawns();
    public abstract void BuildKnights();
    public abstract void BuildBishops();
    public abstract void BuildRooks();
    public abstract void BuildQueens();
    public abstract void BuildKings();
    public abstract Board GetResult();
}

class Board
{
    List<Piece> pieces = new List<Piece>();
    public void AddPiece(Piece piece)
    {
        pieces.Add(piece);
    }
    public void MovePiece()
    {

    }
    public Piece FindPiece(string position)
    {
        return pieces.Find(p => p.PiecePosition == position);
    }
}

class CommonBoardBuilder : BoardBuilder
{
    Board board = new Board();
    public override void BuildPawns()
    {
        for (char c = 'a'; c <= 'h'; c++)
        {
            board.AddPiece(new Piece(PieceName.Pawn, $"{c.ToString()}2", PieceColor.White));
        }
        for (char c = 'a'; c <= 'h'; c++)
        {
            board.AddPiece(new Piece(PieceName.Pawn, $"{c.ToString()}7", PieceColor.White));
        }
    }
    public override void BuildKnights()
    {
        board.AddPiece(new Piece(PieceName.King, "b1", PieceColor.White));
        board.AddPiece(new Piece(PieceName.King, "g1", PieceColor.White));
        board.AddPiece(new Piece(PieceName.King, "b8", PieceColor.Black));
        board.AddPiece(new Piece(PieceName.King, "g8", PieceColor.Black));
    }
    public override void BuildBishops()
    {
        board.AddPiece(new Piece(PieceName.Bishop, "c1", PieceColor.White));
        board.AddPiece(new Piece(PieceName.Bishop, "f1", PieceColor.White));
        board.AddPiece(new Piece(PieceName.Bishop, "c8", PieceColor.Black));
        board.AddPiece(new Piece(PieceName.Bishop, "f8", PieceColor.Black));
    }
    public override void BuildRooks()
    {
        board.AddPiece(new Piece(PieceName.Rook, "a1", PieceColor.White));
        board.AddPiece(new Piece(PieceName.Rook, "h1", PieceColor.White));
        board.AddPiece(new Piece(PieceName.Rook, "a8", PieceColor.Black));
        board.AddPiece(new Piece(PieceName.Rook, "h8", PieceColor.Black));
    }
    public override void BuildQueens()
    {
        board.AddPiece(new Piece(PieceName.Queen, "d1", PieceColor.White));
        board.AddPiece(new Piece(PieceName.Queen, "d8", PieceColor.Black));
    }
    public override void BuildKings()
    {
        board.AddPiece(new Piece(PieceName.King, "e1", PieceColor.White));
        board.AddPiece(new Piece(PieceName.King, "e8", PieceColor.Black));
    }
    public override Board GetResult()
    {
        return board;
    }
}
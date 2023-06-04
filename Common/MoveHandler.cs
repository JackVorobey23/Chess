public abstract class MoveHandler : ISubject
{
    public MoveHandler(BlockingStrategyFactory factory, List<Piece> board)
    {
        _factory = factory;
        _board = board;
    }
    protected BlockingStrategyFactory _factory;
    protected MoveHandler _nextHandler;
    protected List<Piece> _board;

    public void SetNextHandler(MoveHandler handler)
    {
        _nextHandler = handler;
    }

    public virtual bool HandleMove(Piece piece, string move)
    {
        if (_nextHandler != null)
        {
            return _nextHandler.HandleMove(piece, move);
        }
        else
        {
            return true;
        }
    }

    public virtual void Request(Piece piece, string move)
    {
        HandleMove(piece, move);
    }
}
public class BlockingPieceHandler : MoveHandler
{
    public BlockingPieceHandler(BlockingStrategyFactory factory, List<Piece> board) : base(factory, board) { }
    public override bool HandleMove(Piece piece, string move)
    {
        if (_factory[piece.PieceName].PieceIsNotBlocking(piece, move, _board))
        {
            return base.HandleMove(piece, move);
        }
        else
        {
            return false;
        }
    }

    public override void Request(Piece piece, string move)
    {
        base.Request(piece, move);
    }
}
public class KingSafetyHandler : MoveHandler
{
    public KingSafetyHandler(BlockingStrategyFactory factory, List<Piece> board) : base(factory, board) { }
    public override bool HandleMove(Piece piece, string move)
    {
        if (new Random().Next(0,5) > 2)
        {
            return base.HandleMove(piece, move);
        }
        else
        {
            // Move hangs the king, return false
            return false;
        }
    }

    public override void Request(Piece piece, string move)
    {
        base.Request(piece, move);
    }
}
public abstract class MoveHandler
{
    protected MoveHandler _nextHandler;

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
}
public class MovePossibilityHandler : MoveHandler
{
    public override bool HandleMove(Piece piece, string move)
    {
        if (true)
        {
            return base.HandleMove(piece, move);
        }
        else
        {
            return false;
        }
    }
}
public class BlockingPieceHandler : MoveHandler
{
    public override bool HandleMove(Piece piece, string move)
    {
        if (true)
        {
            return base.HandleMove(piece, move);
        }
        else
        {
            return false;
        }
    }
}
public class KingSafetyHandler : MoveHandler
{
    public override bool HandleMove(Piece piece, string move)
    {
        if (true)
        {
            return base.HandleMove(piece, move);
        }
        else
        {
            // Move hangs the king, return false
            return false;
        }
    }
}
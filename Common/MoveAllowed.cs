public interface ISubject
{
    void Request(Piece piece, string move);
}
class MoveAllowed : ISubject
{
    private MoveHandler _moveHandler;
    public void Request(Piece piece, string move)
    {
        if (CheckAccess())
        {
            _moveHandler = new BlockingPieceHandler();
            _moveHandler.SetNextHandler(new KingSafetyHandler());
            _moveHandler.Request(piece, move);
        }
    }
    public bool CheckAccess()
    {
        Console.WriteLine("Proxy: Checking access prior to firing a real request.");

        return true;
    }
}
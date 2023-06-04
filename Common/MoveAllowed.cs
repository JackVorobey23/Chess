public interface ISubject
{
    void Request(Piece piece, string move);
}
class MoveAllowed : ISubject
{
    public MoveAllowed(BlockingStrategyFactory factory, List<Piece> board)
    {
        _factory = factory;

    }
    private MoveHandler _moveHandler;
    protected BlockingStrategyFactory _factory;
    protected List<Piece> _board;

    public void Request(Piece piece, string move)
    {
        if (CheckAccess())
        {
            _moveHandler = new BlockingPieceHandler(_factory, _board);
            _moveHandler.SetNextHandler(new KingSafetyHandler(_factory, _board));
            _moveHandler.Request(piece, move);
        }
    }
    public bool CheckAccess()
    {
        #warning Proxy: Checking access prior to firing a real request.

        return true;
    }
}
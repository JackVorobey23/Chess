public interface ISubject
{
    bool MoveAllowedRequest(Piece piece, string move);
}
class MoveAllowed : ISubject
{
    public MoveAllowed(List<Piece> board, BlockingStrategyFactory factory, ChecksCollection checksCollection)
    {
        _factory = factory;
        _board = board;
        _checksCollection = checksCollection;
    }
    private MoveHandler _moveHandler;
    protected BlockingStrategyFactory _factory;
    protected ChecksCollection _checksCollection;
    protected List<Piece> _board;

    public bool MoveAllowedRequest(Piece piece, string move)
    {
        if (CheckAccess(piece, move))
        {
            _moveHandler = new BlockingPieceHandler(_board, _factory);
            
            _moveHandler.SetNextHandler(new KingSafetyHandler(_board, _checksCollection));
           
            
            return _moveHandler.HandleMove(piece, move);
        }
        else 
        {
            System.Console.WriteLine($"{piece.PieceName.ToString()} cannot move this way");
            return false;
        }
    }
    private bool CheckAccess(Piece piece, string move)
    {
        #warning Proxy: Checking access prior to firing a real request.

        return true;
    }
}
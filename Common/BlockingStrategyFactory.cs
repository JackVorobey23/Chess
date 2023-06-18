public class BlockingStrategyFactory
{
    private readonly Dictionary<PieceName, Func<IPieceBlockingStrategy>> strategies;

    public BlockingStrategyFactory()
    {
        strategies = new();
    }
    public IPieceBlockingStrategy CreateStrategy(PieceName pieceName) => strategies[pieceName]();

    public IPieceBlockingStrategy this[PieceName pieceName]
    {
        get
        {
            return CreateStrategy(pieceName);
        }
    }

    public PieceName[] RegisteredStrategies => strategies.Keys.ToArray();

    public void RegisterStrategy(PieceName pieceName, Func<IPieceBlockingStrategy> createMethod)
    {
        strategies[pieceName] = createMethod;
    }

}
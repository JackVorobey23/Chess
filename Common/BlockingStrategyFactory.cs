abstract class BlockingStrategyFactory
{
    public abstract IPieceBlockingStrategy CreateStrategy();
}
public class BlockingStrategyFactoryDirector
{
    public IPieceBlockingStrategy GetBlockingStrategy(Piece piece)
    {
        switch (piece.PieceName)
        {
            case PieceName.Pawn:
                return new PawnStrategyCreator().CreateStrategy();

            case PieceName.King:
                return new PawnStrategyCreator().CreateStrategy();

            case PieceName.Bishop:
                return new PawnStrategyCreator().CreateStrategy();

            case PieceName.Knight:
                return new PawnStrategyCreator().CreateStrategy();

            case PieceName.Rook:
                return new PawnStrategyCreator().CreateStrategy();
                
            case PieceName.Queen:
                return new PawnStrategyCreator().CreateStrategy();
        }
        return null;
    }
}
class PawnStrategyCreator : BlockingStrategyFactory
{
    public override PawnIsNotBlockingStrategy CreateStrategy() { return new PawnIsNotBlockingStrategy(); }
}

class KnightStrategyCreator : BlockingStrategyFactory
{

    public override KnightIsNotBlockingStrategy CreateStrategy() { return new KnightIsNotBlockingStrategy(); }
}
class RookStrategyCreator : BlockingStrategyFactory
{

    public override RookIsNotBlockingStrategy CreateStrategy() { return new RookIsNotBlockingStrategy(); }
}
class BishopStrategyCreator : BlockingStrategyFactory
{

    public override BishopIsNotBlockingStrategy CreateStrategy() { return new BishopIsNotBlockingStrategy(); }
}
class QueenStrategyCreator : BlockingStrategyFactory
{

    public override QueenIsNotBlockingStrategy CreateStrategy() { return new QueenIsNotBlockingStrategy(); }
}
class KingStrategyCreator : BlockingStrategyFactory
{

    public override KingIsNotBlockingStrategy CreateStrategy() { return new KingIsNotBlockingStrategy(); }
}
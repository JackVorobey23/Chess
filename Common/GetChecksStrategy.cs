public interface IGetChecksStrategy
{
    Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor);
}

public class GetDiagonalCheck : IGetChecksStrategy
{
    public Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor)
    {
        throw new NotImplementedException();
    }
}
public class GetLineCheck : IGetChecksStrategy
{
    public Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor)
    {
        throw new NotImplementedException();
    }
}
public class GetKnightCheck : IGetChecksStrategy
{
    public Piece IsKingUnderCheck(List<Piece> board, PieceColor kingColor)
    {
        throw new NotImplementedException();
    }
}
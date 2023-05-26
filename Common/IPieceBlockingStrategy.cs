interface IPieceBlockingStrategy
{
    bool PieceIsNotBlocking(Piece piece, string move);
}
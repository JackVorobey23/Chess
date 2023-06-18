class MoveDto
{
    public int GameId { get; set; }
    public List<Piece> Pieces { get; set; }

    public MoveDto(int gameId,  List<Piece> pieces)
    {
        GameId = gameId;
        Pieces = pieces;
    }
}
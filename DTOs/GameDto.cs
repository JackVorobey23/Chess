class GameDto
{
    public int BPieceUserId { get; set; }
    public int WPieceUserId { get; set; }
    public int GameId { get; set; }
    public List<Piece> Pieces { get; set; }

    public GameDto(int wPieceUserId, int bPieceUserId, int gameId, List<Piece> pieces)
    {
        WPieceUserId = wPieceUserId;
        BPieceUserId = bPieceUserId;
        GameId = gameId;
        Pieces = pieces;
    }
}
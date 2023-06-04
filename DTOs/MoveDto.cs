class MoveDto
{
    public int GameId { get; set; }
    public string Move { get; set; }

    public MoveDto(int gameId, string move)
    {
        GameId = gameId;
        Move = move;
    }
}
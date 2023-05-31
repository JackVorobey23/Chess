class GameDto
{
    public int User1Id { get; set; }
    public int User2Id { get; set; }
    public int GameId { get; set; }

    public GameDto(int user1Id, int user2Id, int gameId)
    {
        User1Id = user1Id;
        User2Id = user2Id;
        GameId = gameId;
    }
}
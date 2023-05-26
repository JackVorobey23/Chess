public class Player
{
    public int PlayerId { get; set; }
    public string FullName { get; set; }
    public List<Game> games { get; set; }
    public int Rating { get; set; }

    public Player(int playerId, string fullName, int rating = 1500)
    {
        PlayerId = playerId;
        FullName = fullName;
        this.games = new List<Game>();
        Rating = rating;
    }
}
public class Player
{
    public int PlayerId { get; set; }
    public string FullName { get; set; }
    public List<Game> Games { get; set; }
    public int Rating { get; set; }
    public bool IsWaiting { get; set; }

    public Player(int playerId, string fullName, int rating = 1500)
    {
        PlayerId = playerId;
        FullName = fullName;
        Rating = rating;
        IsWaiting = false;
        Games = new List<Game>();
    }
}
namespace AutoDice.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsGameMaster { get; set; }

    public ICollection<Character> Characters { get; set; } = new List<Character>();
    public ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
}

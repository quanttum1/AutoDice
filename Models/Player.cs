namespace AutoDice.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsGameMaster { get; set; }

    public ICollection<CharacterEntity> Characters { get; set; } = new List<CharacterEntity>();
    public ICollection<WebUserSession> WebUserSessions { get; set; } = new List<WebUserSession>();
    public ICollection<TgUser> TgUsers { get; set; } = new List<TgUser>();
}

namespace AutoDice.Models;

public class TgUser
{
    public long TgUserId { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
}

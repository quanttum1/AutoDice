namespace AutoDice.Models;

public class WebUserSession
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
}

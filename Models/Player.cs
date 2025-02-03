namespace AutoDice.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Character> Characters { get; set; } = new List<Character>();
}

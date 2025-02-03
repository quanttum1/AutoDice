namespace AutoDice.Models;

public class HealthType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double CanBeLost { get; set; }
    public double Weight { get; set; }
    public double PowerWeight { get; set; }
    public double PassiveIncrease { get; set; }
    public double PassiveDecrease { get; set; }

    public ICollection<CharacterHealth> CharacterHealths { get; set; } = new List<CharacterHealth>();
}


namespace AutoDice.Models;

public class CharacterHealth
{
    public int CharacterId { get; set; }
    public CharacterEntity Character { get; set; } = null!;

    public int HealthTypeId { get; set; }
    public HealthType HealthType { get; set; } = null!;

    public double Value { get; set; }
    public double MaxValue { get; set; }
}


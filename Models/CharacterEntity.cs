namespace AutoDice.Models;

public class CharacterEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public string? ImagePath { get; set; }

    public double Experience { get; set; } = 0.0;
    public double ExperienceCanBeSpent { get; set; } = 0.0;

    public ICollection<CharacterSkill> CharacterSkills { get; set; } = new List<CharacterSkill>();
    public ICollection<CharacterHealth> CharacterHealths { get; set; } = new List<CharacterHealth>();
    public ICollection<CharacterInventory> CharacterInventories { get; set; } = new List<CharacterInventory>();
}


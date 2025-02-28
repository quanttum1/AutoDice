namespace AutoDice.Models;

public class CharacterSkill
{
    public int CharacterId { get; set; }
    public CharacterEntity Character { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;

    public double Value { get; set; }
}


namespace AutoDice.Models;

public class SkillTree
{
    public int ParentSkillId { get; set; }
    public Skill ParentSkill { get; set; } = null!;

    public int SubskillId { get; set; }
    public Skill Subskill { get; set; } = null!;

    public double Weight { get; set; }
}

namespace AutoDice.Models;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<SkillTree> ParentSkillLinks { get; set; } = new List<SkillTree>();
    public ICollection<SkillTree> SubskillLinks { get; set; } = new List<SkillTree>();
    public ICollection<CharacterSkill> CharacterSkills { get; set; } = new List<CharacterSkill>();
}

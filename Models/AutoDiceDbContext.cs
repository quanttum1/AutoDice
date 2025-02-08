using Microsoft.EntityFrameworkCore;

namespace AutoDice.Models;

public class AutoDiceDbContext : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<SkillTree> SkillTrees { get; set; }
    public DbSet<CharacterSkill> CharacterSkills { get; set; }
    public DbSet<HealthType> HealthTypes { get; set; }
    public DbSet<CharacterHealth> CharacterHealths { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<CharacterInventory> CharacterInventories { get; set; }

    public AutoDiceDbContext(DbContextOptions<AutoDiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WebUserSession>()
            .HasOne(wus => wus.Player)
            .WithMany(p => p.WebUserSessions)
            .HasForeignKey(wus => wus.PlayerId);

        modelBuilder.Entity<TgUser>()
            .HasOne(tu => tu.Player)
            .WithMany(p => p.TgUsers)
            .HasForeignKey(tu => tu.PlayerId);

        modelBuilder.Entity<Character>()
            .HasOne(c => c.Player)
            .WithMany(p => p.Characters)
            .HasForeignKey(c => c.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Composite Key for SkillTree
        modelBuilder.Entity<SkillTree>()
            .HasKey(st => new { st.ParentSkillId, st.SubskillId });

        modelBuilder.Entity<SkillTree>()
            .HasOne(st => st.ParentSkill)
            .WithMany(s => s.ParentSkillLinks)
            .HasForeignKey(st => st.ParentSkillId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SkillTree>()
            .HasOne(st => st.Subskill)
            .WithMany(s => s.SubskillLinks)
            .HasForeignKey(st => st.SubskillId)
            .OnDelete(DeleteBehavior.Restrict);

        // Composite Key for CharacterSkills
        modelBuilder.Entity<CharacterSkill>()
            .HasKey(cs => new { cs.CharacterId, cs.SkillId });

        modelBuilder.Entity<CharacterSkill>()
            .HasOne(cs => cs.Character)
            .WithMany(c => c.CharacterSkills)
            .HasForeignKey(cs => cs.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CharacterSkill>()
            .HasOne(cs => cs.Skill)
            .WithMany(s => s.CharacterSkills)
            .HasForeignKey(cs => cs.SkillId)
            .OnDelete(DeleteBehavior.Cascade);

        // Composite Key for CharacterHealth
        modelBuilder.Entity<CharacterHealth>()
            .HasKey(ch => new { ch.CharacterId, ch.HealthTypeId });

        modelBuilder.Entity<CharacterHealth>()
            .HasOne(ch => ch.Character)
            .WithMany(c => c.CharacterHealths)
            .HasForeignKey(ch => ch.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CharacterHealth>()
            .HasOne(ch => ch.HealthType)
            .WithMany(ht => ht.CharacterHealths)
            .HasForeignKey(ch => ch.HealthTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Composite Key for CharacterInventory
        modelBuilder.Entity<CharacterInventory>()
            .HasKey(ci => new { ci.CharacterId, ci.ItemId });

        modelBuilder.Entity<CharacterInventory>()
            .HasOne(ci => ci.Character)
            .WithMany(c => c.CharacterInventories)
            .HasForeignKey(ci => ci.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CharacterInventory>()
            .HasOne(ci => ci.Item)
            .WithMany(i => i.CharacterInventories)
            .HasForeignKey(ci => ci.ItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


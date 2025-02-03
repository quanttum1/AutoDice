namespace AutoDice.Models;

public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImagePath { get; set; }

    public ICollection<CharacterInventory> CharacterInventories { get; set; } = new List<CharacterInventory>();
}


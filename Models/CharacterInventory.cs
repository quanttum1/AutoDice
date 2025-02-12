namespace AutoDice.Models;

public class CharacterInventory
{
    public int CharacterId { get; set; }
    public CharacterEntity Character { get; set; } = null!;

    public int ItemId { get; set; }
    public InventoryItem Item { get; set; } = null!;

    public int Count { get; set; }
}


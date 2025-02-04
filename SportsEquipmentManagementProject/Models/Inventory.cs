namespace SportsEquipmentManagementProject.Models;

public class Inventory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Condition { get; set; } // New, Used, Broken
    public ICollection<UserInventory> UserInventories { get; set; }
}

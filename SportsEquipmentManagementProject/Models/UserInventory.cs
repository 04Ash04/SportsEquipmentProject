namespace SportsEquipmentManagementProject.Models;

public class UserInventory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int InventoryId { get; set; }
    
    public int Quantity { get; set; }
    
    public User User { get; set; }
    public Inventory Inventory { get; set; }
}
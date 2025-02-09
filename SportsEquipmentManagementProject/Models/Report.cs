namespace SportsEquipmentManagementProject.Models;

public class Report
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int InventoryId { get; set; }
    public string Status { get; set; } 
    public string UsageInfo { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual User User { get; set; }
    public virtual Inventory Inventory { get; set; }
}

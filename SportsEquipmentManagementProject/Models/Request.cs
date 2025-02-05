namespace SportsEquipmentManagementProject.Models;

public class Request
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int InventoryId { get; set; }
    public string Type { get; set; } // "Получение", "Ремонт", "Замена"
    public string Status { get; set; } // "Ожидание", "Отклонено", "Получено"
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual User User { get; set; }
    public virtual Inventory Inventory { get; set; }
}
namespace SportsEquipmentManagementProject.Models;

public class Request
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int ClientId { get; set; }
    public string Status { get; set; }
    public Inventory Inventory { get; set; }
}
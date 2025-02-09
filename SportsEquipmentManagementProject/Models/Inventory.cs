using System.ComponentModel.DataAnnotations;

namespace SportsEquipmentManagementProject.Models;

public class Inventory
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Название инвентаря")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Количество")]
    public int Quantity { get; set; }
    [Required]
    [Display(Name = "Состояние")]
    public string Condition { get; set; } // New, Used, Broken
    
}

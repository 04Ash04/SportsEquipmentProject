using System.ComponentModel.DataAnnotations;

namespace SportsEquipmentManagementProject.Models;

public class PurchasePlan
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Название инвентаря")]
    public string ItemName { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Цена должна быть положительным числом")]
    [Display(Name = "Цена")]
    public decimal Price { get; set; }
    
    [Required]
    [Display(Name = "Количество")]
    [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
    public int Quantity { get; set; }

    [Required]
    [Display(Name = "Поставщик")]
    public string SupplierName { get; set; }
}
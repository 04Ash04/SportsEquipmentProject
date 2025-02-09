using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsEquipmentManagementProject.Context;
using SportsEquipmentManagementProject.Models;

namespace SportsEquipmentManagementProject.Controllers;

public class UserInventoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserInventoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    // Страница для администратора: Закрепление инвентаря за пользователем
    public ActionResult Assign()
    {
        ViewBag.Users = _context.Users.Where(u => u.Role != "Admin").ToList();;
        ViewBag.Inventory = _context.Inventories.ToList();
        return View();
    }
    
    [HttpPost]
    public ActionResult Assign(int userId, int inventoryId, int quantity)
    {
        var existingAssignment = _context.UserInventories.FirstOrDefault(ui => ui.UserId == userId && ui.InventoryId == inventoryId);
        
        if (existingAssignment != null)
        {
            existingAssignment.Quantity += quantity;
        }
        else
        {
            var userInventory = new UserInventory
            {
                UserId = userId,
                Quantity = quantity,
                InventoryId = inventoryId,
            };
            _context.UserInventories.Add(userInventory);
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    // Просмотр всех закрепленных инвентарей (для администратора)
    public ActionResult Index()
    {
        var assignedInventory = _context.UserInventories.Include("User").Include("Inventory").ToList();
        return View(assignedInventory);
    }
    
    public ActionResult UserInventory()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");
        
        var userInventory = _context.UserInventories.Where(ui => ui.UserId == userId).Include("Inventory").ToList();
        return View(userInventory);
    }

}

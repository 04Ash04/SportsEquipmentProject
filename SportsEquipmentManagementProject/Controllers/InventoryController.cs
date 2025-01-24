using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using SportsEquipmentManagementProject.Context;
using SportsEquipmentManagementProject.Models;

public class InventoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public InventoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var inventories = _context.Inventories.ToList();
        var role = HttpContext.Session.GetString("Role");

        if (role == null)
            return RedirectToAction("Login", "Account");

        ViewBag.Role = role;
        return View(inventories);
    }

    [HttpGet]
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
  
    public IActionResult Create(Inventory inventory)
    {
        if (ModelState.IsValid)
        {
            _context.Inventories.Add(inventory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(inventory);
    }

    [HttpGet]
  
    public IActionResult Edit(int id)
    {
        var inventory = _context.Inventories.Find(id);
        if (inventory == null)
        {
            return NotFound();
        }
        return View(inventory);
    }

    [HttpPost]
 
    public IActionResult Edit(Inventory inventory)
    {
        if (ModelState.IsValid)
        {
            _context.Inventories.Update(inventory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(inventory);
    }

    [HttpPost]
   
    public IActionResult Delete(int id)
    {
        var inventory = _context.Inventories.Find(id);
        if (inventory != null)
        {
            _context.Inventories.Remove(inventory);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}

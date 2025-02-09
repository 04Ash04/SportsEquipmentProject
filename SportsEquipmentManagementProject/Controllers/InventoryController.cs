using Microsoft.AspNetCore.Mvc;
using SportsEquipmentManagementProject.Context;
using SportsEquipmentManagementProject.Models;

namespace SportsEquipmentManagementProject.Controllers;

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
        var role = HttpContext.Session.GetString("Role");
        if (role == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var inventories = _context.Inventories.ToList();
        ViewBag.Role = role;
        return View(inventories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Inventory inventory)
    {
        if (!ModelState.IsValid)
        {
            return View(inventory);
        }

        _context.Add(inventory);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inventory =  _context.Inventories
            .FirstOrDefault(m => m.Id == id);
        if (inventory == null)
        {
            return NotFound();
        }

        return View(inventory);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var inventory = await _context.Inventories.FindAsync(id);
        _context.Inventories.Remove(inventory);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
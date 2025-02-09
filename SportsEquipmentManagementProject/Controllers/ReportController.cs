using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsEquipmentManagementProject.Context;
using SportsEquipmentManagementProject.Models;

namespace SportsEquipmentManagementProject.Controllers;

public class ReportController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReportController(ApplicationDbContext context)
    {
        _context = context;
    }
    // 1. Создание отчета администратором
    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Users = _context.Users.Where(u => u.Role != "Admin").ToList();;
        ViewBag.Inventory = _context.Inventories.ToList();
        return View();
    }

    [HttpPost]
    public ActionResult Create(int userId, int inventoryId, string status, string usageInfo)
    {
        var report = new Report
        {
            UserId = userId,
            InventoryId = inventoryId,
            Status = status,
            UsageInfo = usageInfo,
            CreatedAt = DateTime.Now
        };

        _context.Reports.Add(report);
        _context.SaveChanges();
        return RedirectToAction("AllReports");
    }

    // 2. Просмотр всех отчетов администратором
    public ActionResult AllReports()
    {
        var reports = _context.Reports.Include("User").Include("Inventory").OrderByDescending(r => r.CreatedAt).ToList();
        return View(reports);
    }

    // 3. Просмотр отчета по конкретному инвентарю
    public ActionResult InventoryReports(int inventoryId)
    {
        var reports = _context.Reports.Where(r => r.InventoryId == inventoryId).OrderByDescending(r => r.CreatedAt).ToList();
        return View(reports);
    }
}
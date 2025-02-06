using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsEquipmentManagementProject.Context;
using SportsEquipmentManagementProject.Models;
namespace SportsEquipmentManagementProject.Controllers;

public class RequestController : Controller
{
    private readonly ApplicationDbContext _context;

    public RequestController(ApplicationDbContext context)
    {
        _context = context;
    }
    // 1. Создание заявки пользователем
    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Inventory = _context.Inventories.ToList();
        return View();
    }

    [HttpPost]
    public ActionResult Create(int inventoryId, string type)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");
        ViewBag.Id = userId;
        var request = new Request
        {
            UserId = userId.Value,
            InventoryId = inventoryId,
            Type = type,
            Status = "Ожидание",
            CreatedAt = DateTime.Now
        };

        _context.Requests.Add(request);
        _context.SaveChanges();
        return RedirectToAction("UserRequests");
    }
    // 2. Просмотр своих заявок с фильтрацией и сортировкой
    public ActionResult UserRequests(string status, string sortOrder)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");
        ViewBag.Id = userId;
        var requests = _context.Requests
            .Include(r => r.Inventory) // Это ключевое изменение
            .Where(r => r.UserId == userId);
        // Фильтрация по статусу
        if (!string.IsNullOrEmpty(status))
        {
            requests = requests.Where(r => r.Status == status);
        }

        // Сортировка по дате
        requests = sortOrder == "desc" 
            ? requests.OrderByDescending(r => r.CreatedAt) 
            : requests.OrderBy(r => r.CreatedAt);

        return View(requests.ToList());
    }

    // 3. Просмотр всех заявок для администратора
    public ActionResult AdminRequests(string status, string sortOrder)
    {
        var requests = _context.Requests.Include("User").Include("Inventory");

        // Фильтрация по статусу
        if (!string.IsNullOrEmpty(status))
        {
            requests = requests.Where(r => r.Status == status);
        }

        // Сортировка по дате
        requests = sortOrder == "desc" 
            ? requests.OrderByDescending(r => r.CreatedAt) 
            : requests.OrderBy(r => r.CreatedAt);

        return View(requests.ToList());
    }

    // 4. Изменение статуса заявки администратором
    [HttpPost]
    public ActionResult UpdateStatus(int requestId, string status)
    {
        var request = _context.Requests.Find(requestId);
        if (request != null)
        {
            request.Status = status;
            _context.SaveChanges();
        }
        return RedirectToAction("AdminRequests");
    }
}
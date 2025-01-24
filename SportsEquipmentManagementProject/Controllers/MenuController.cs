using Microsoft.AspNetCore.Mvc;

namespace SportsEquipmentManagementProject.Controllers;

public class MenuController : Controller
{
    public IActionResult Index()
    {
        var role = HttpContext.Session.GetString("Role");

        if (role == null)
            return RedirectToAction("Login", "Account");

        ViewBag.Role = role;
        return View();
    }
    
}
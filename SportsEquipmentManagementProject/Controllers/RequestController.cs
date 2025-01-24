using Microsoft.AspNetCore.Mvc;

namespace SportsEquipmentManagementProject.Controllers;

public class RequestController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
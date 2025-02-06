using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using SportsEquipmentManagementProject.Context;
using SportsEquipmentManagementProject.Models;
using BCrypt.Net;

namespace SportsEquipmentManagementProject.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string userName, string password,string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ViewBag.Error = "Пароли не совпадают.";
            return View();
        }
        if (_context.Users.Any(u => u.UserName == userName))
        {
            ViewBag.Error = "Пользователь с таким ником уже существует.";
            return View();
        }

        var user = new User
        {
            UserName = userName,
            Password = BCrypt.Net.BCrypt.HashPassword(password),
            Role = "User",
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return RedirectToAction("Login");
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string userName, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            ViewBag.Error = "Неверные учетные данные.";
            return View();
        }

        // Установим сессию
        HttpContext.Session.SetString("UserName", user.UserName);
        HttpContext.Session.SetString("Role", user.Role);
        HttpContext.Session.SetInt32("UserId", user.Id);
        return RedirectToAction("Index", "Menu");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

}

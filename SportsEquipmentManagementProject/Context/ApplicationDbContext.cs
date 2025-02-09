using Microsoft.EntityFrameworkCore;
using SportsEquipmentManagementProject.Models;

namespace SportsEquipmentManagementProject.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<UserInventory> UserInventories { get; set; }
    public DbSet<PurchasePlan> PurchasePlans { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Report> Reports { get; set; }
}
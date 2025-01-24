using System.Data.Entity.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SportsEquipmentManagementProject.Context;
using SportsEquipmentManagementProject.Models;

namespace SportsEquipmentManagementProject.Controllers;

public class PurchasePlanController : Controller
{
    private readonly ApplicationDbContext _context;

        public PurchasePlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View( _context.PurchasePlans.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchasePlan purchasePlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchasePlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchasePlan);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchasePlan = await _context.PurchasePlans.FindAsync(id);
            if (purchasePlan == null)
            {
                return NotFound();
            }
            return View(purchasePlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchasePlan purchasePlan)
        {
            if (id != purchasePlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchasePlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchasePlanExists(purchasePlan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(purchasePlan);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchasePlan =  _context.PurchasePlans
                .FirstOrDefault(m => m.Id == id);
            if (purchasePlan == null)
            {
                return NotFound();
            }

            return View(purchasePlan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchasePlan = await _context.PurchasePlans.FindAsync(id);
            _context.PurchasePlans.Remove(purchasePlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchasePlanExists(int id)
        {
            return _context.PurchasePlans.Any(e => e.Id == id);
        }
}
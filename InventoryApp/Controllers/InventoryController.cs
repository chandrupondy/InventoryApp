using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class InventoryController : Controller
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context)
        {
            _context = context;
        }

       
        private bool IsUserLoggedIn()
        {
            //return HttpContext.Session.GetString("Username") != null;
            return User?.Identity != null && User.Identity.IsAuthenticated;

        }

        // GET: /Inventory/
        public IActionResult Index()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var items = _context.InventoryItems.ToList();
            return View(items);
        }

        // GET: /Inventory/Create
        public IActionResult Create()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: /Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InventoryItem item)
        {
            if (ModelState.IsValid)
            {
                _context.InventoryItems.Add(item);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: /Inventory/Edit/5
        public IActionResult Edit(int id)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var item = _context.InventoryItems.Find(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: /Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, InventoryItem item)
        {
            if (id != item.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.InventoryItems.Update(item);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: /Inventory/Delete/5
        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.InventoryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Inventory/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item != null)
            {
                _context.InventoryItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

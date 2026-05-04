using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingManagementSystem.Data;
using BankingManagementSystem.Models;

namespace BankingManagementSystem.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private SelectList GetCustomerSelectList(int? selectedCustomerId = null)
        {
            var customers = _context.Customers
                .Select(c => new
                {
                    c.CustomerId,
                    Display = "ID: " + c.CustomerId + " - " + c.FirstName + " " + c.LastName
                })
                .ToList();

            return new SelectList(customers, "CustomerId", "Display", selectedCustomerId);
        }

        public async Task<IActionResult> Index()
        {
            var accounts = _context.Accounts
                .Include(a => a.Branch)
                .Include(a => a.Customer);

            return View(await accounts.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var account = await _context.Accounts
                .Include(a => a.Branch)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.AccountId == id);

            if (account == null) return NotFound();

            return View(account);
        }

        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchName");
            ViewData["CustomerId"] = GetCustomerSelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,AccountNumber,CustomerId,BranchId,AccountType,Balance,OpenDate,AccountStatus,CreatedAt,UpdatedAt")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchName", account.BranchId);
            ViewData["CustomerId"] = GetCustomerSelectList(account.CustomerId);

            return View(account);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var account = await _context.Accounts.FindAsync(id);

            if (account == null) return NotFound();

            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchName", account.BranchId);
            ViewData["CustomerId"] = GetCustomerSelectList(account.CustomerId);

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,AccountNumber,CustomerId,BranchId,AccountType,Balance,OpenDate,AccountStatus,CreatedAt,UpdatedAt")] Account account)
        {
            if (id != account.AccountId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchName", account.BranchId);
            ViewData["CustomerId"] = GetCustomerSelectList(account.CustomerId);

            return View(account);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var account = await _context.Accounts
                .Include(a => a.Branch)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.AccountId == id);

            if (account == null) return NotFound();

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
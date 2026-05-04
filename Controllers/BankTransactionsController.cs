using BankingManagementSystem.Data;
using BankingManagementSystem.Models;
using BankingManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Controllers
{
    public class BankTransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankTransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _context.BankTransactions
                .Include(t => t.Account)
                .ThenInclude(a => a.Customer)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return View(transactions);
        }

        public IActionResult Create()
        {
            LoadAccounts();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadAccounts(model.AccountId);
                return View(model);
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == model.AccountId);

            if (account == null)
            {
                ModelState.AddModelError("", "Selected account was not found.");
                LoadAccounts(model.AccountId);
                return View(model);
            }

            if (model.TransactionType == "Withdrawal" && account.Balance < model.Amount)
            {
                ModelState.AddModelError("Amount", "Insufficient balance for withdrawal.");
                LoadAccounts(model.AccountId);
                return View(model);
            }

            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (model.TransactionType == "Deposit")
                {
                    account.Balance += model.Amount;
                }
                else if (model.TransactionType == "Withdrawal")
                {
                    account.Balance -= model.Amount;
                }
                else
                {
                    ModelState.AddModelError("TransactionType", "Invalid transaction type.");
                    LoadAccounts(model.AccountId);
                    return View(model);
                }

                account.UpdatedAt = DateTime.Now;

                var bankTransaction = new BankTransaction
                {
                    AccountId = model.AccountId,
                    TransactionType = model.TransactionType,
                    Amount = model.Amount,
                    TransactionDate = DateTime.Now,
                    Description = model.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Accounts.Update(account);
                _context.BankTransactions.Add(bankTransaction);

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                ModelState.AddModelError("", "Transaction failed. Please try again.");
                LoadAccounts(model.AccountId);
                return View(model);
            }
        }

        private void LoadAccounts(int? selectedAccountId = null)
        {
            var accounts = _context.Accounts
                .Include(a => a.Customer)
                .Select(a => new
                {
                    a.AccountId,
                    DisplayName = a.AccountNumber + " - " + a.Customer!.FirstName + " " + a.Customer.LastName
                })
                .ToList();

            ViewData["AccountId"] = new SelectList(accounts, "AccountId", "DisplayName", selectedAccountId);
        }
    }
}
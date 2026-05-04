using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankingManagementSystem.Models;
using BankingManagementSystem.Data;
using BankingManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            TotalCustomers = await _context.Customers.CountAsync(),
            TotalBranches = await _context.Branches.CountAsync(),
            TotalAccounts = await _context.Accounts.CountAsync(),

            TotalBalance = await _context.Accounts.AnyAsync()
                ? await _context.Accounts.SumAsync(a => a.Balance)
                : 0,

            AverageBalance = await _context.Accounts.AnyAsync()
                ? await _context.Accounts.AverageAsync(a => a.Balance)
                : 0,

            TotalTransactions = await _context.BankTransactions.CountAsync(),

            TotalTransactionAmount = await _context.BankTransactions.AnyAsync()
                ? await _context.BankTransactions.SumAsync(t => t.Amount)
                : 0,
            
            TotalLoans = await _context.Loans.CountAsync(),

            TotalLoanAmount = await _context.Loans.AnyAsync()
                ? await _context.Loans.SumAsync(l => l.LoanAmount)
                : 0,
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel 
        { 
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
        });
    }
}
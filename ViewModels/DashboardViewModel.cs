namespace BankingManagementSystem.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalBranches { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal AverageBalance { get; set; }
        public int TotalTransactions { get; set; }
        public decimal TotalTransactionAmount { get; set; }
        public int TotalLoans { get; set; }
        public decimal TotalLoanAmount { get; set; }
    }
}
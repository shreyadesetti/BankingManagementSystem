using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.ViewModels
{
    public class TransactionViewModel
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public string TransactionType { get; set; } = string.Empty;

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [StringLength(150)]
        public string? Description { get; set; }
    }
}
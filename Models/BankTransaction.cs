using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Models
{
    public class BankTransaction
    {
        public int BankTransactionId { get; set; }

        [Required]
        public int AccountId { get; set; }

        public Account? Account { get; set; }

        [Required]
        [StringLength(30)]
        public string TransactionType { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [StringLength(150)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
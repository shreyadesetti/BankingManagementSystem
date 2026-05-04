using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        public int BranchId { get; set; }

        public Branch? Branch { get; set; }

        [Required]
        [StringLength(30)]
        public string AccountType { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Balance { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OpenDate { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountStatus { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<BankTransaction> BankTransactions { get; set; } = new List<BankTransaction>();
    }
}
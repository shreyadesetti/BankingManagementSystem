using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Models
{
    public class Loan
    {
        public int LoanId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        public int BranchId { get; set; }

        public Branch? Branch { get; set; }

        [Required]
        [StringLength(50)]
        public string LoanType { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "Loan amount must be greater than 0.")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal LoanAmount { get; set; }

        [Range(0, 100, ErrorMessage = "Interest rate must be between 0 and 100.")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(20)]
        public string LoanStatus { get; set; } = string.Empty;

        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalRepayment => LoanAmount + (LoanAmount * InterestRate / 100);

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
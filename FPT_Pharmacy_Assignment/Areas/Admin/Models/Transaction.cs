using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Models
{
    public enum PaymentMethod
    {
        Cash,
        CreditCard,
        DebitCard,
        OnlinePayment
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Transaction amount must be greater than or equal to zero.")]
        public decimal TransactionAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public TransactionStatus Status { get; set; }

        public virtual Order Order { get; set; }
    }
}

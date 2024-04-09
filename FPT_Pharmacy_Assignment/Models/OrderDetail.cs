using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPT_Pharmacy_Assignment.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        public int OrderDetailId { get; set; }

        [ForeignKey("Order")]
        [Column(Order = 1)]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        [Column(Order = 2)]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal PricePerUnit => Product?.Price ?? 0;

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

    }
}

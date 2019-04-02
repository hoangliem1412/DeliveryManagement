using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementDelivery.App.Model
{
    [Table("GoodsReceipt")]
    public partial class GoodsReceipt : EntityBase
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int? SupplierId { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public DateTime DateReceipt { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}

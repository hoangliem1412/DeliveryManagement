using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementDelivery.App.Model
{
    [Table("DeliveryDetail")]
    public partial class DeliveryDetail : EntityBase
    {
        public long Id { get; set; }

        public long DeliveryId { get; set; }

        public int ProductId { get; set; }

        // public int DriverId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int Status { get; set; }

        // public virtual Driver Driver { get; set; }

        public virtual Delivery Delivery { get; set; }

        public virtual Product Product { get; set; }
    }
}

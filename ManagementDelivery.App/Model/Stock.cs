using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementDelivery.App.Model
{
    [Table("Stock")]
    public partial class Stock : EntityBase
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}

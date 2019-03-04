namespace ManagementDelivery.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoodsReceipt")]
    public partial class GoodsReceipt
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? SupplierId { get; set; }

        public int? Quantity { get; set; }

        public DateTime? DateReceipt { get; set; }

        public DateTime? InsertDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}

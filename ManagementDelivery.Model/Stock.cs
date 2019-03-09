namespace ManagementDelivery.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stock")]
    public partial class Stock : EntityBase
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public bool IsDelete { get; set; }

        public virtual Product Product { get; set; }
    }
}

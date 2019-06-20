using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementDelivery.App.Model
{
    [Table("Delivery")]
    public partial class Delivery : EntityBase
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Delivery()
        //{
        //    DeliveryDetails = new HashSet<DeliveryDetail>();
        //}

        public long Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalPrice { get; set; }

        public virtual Customer Customer { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; }
    }
}

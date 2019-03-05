namespace ManagementDelivery.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Delivery")]
    public partial class Delivery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Delivery()
        {
            DeliveryDetails = new HashSet<DeliveryDetail>();
        }

        public long Id { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [StringLength(10)]
        public string TotalPrice { get; set; }

        public DateTime? DateDelivery { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? InsertDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; }
    }
}

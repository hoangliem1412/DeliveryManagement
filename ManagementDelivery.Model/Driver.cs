namespace ManagementDelivery.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Driver")]
    public partial class Driver
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public string MoreInfo { get; set; }

        public string Note { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? InsertDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}

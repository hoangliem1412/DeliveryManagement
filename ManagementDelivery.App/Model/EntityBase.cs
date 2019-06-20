using System;

namespace ManagementDelivery.App.Model
{
    public class EntityBase
    {
        public DateTime InsertAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public bool IsDelete { get; set; }
    }
}

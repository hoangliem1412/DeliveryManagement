using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementDelivery.App.Common
{
    public class EnumList
    {
    }

    public enum StatusDelivery : int
    {
        Dispatched = 1, // Đã tiếp nhận đơn hàng
        InTransit = 2, // đã giao tài xế
        OutForDelivery = 3, // tài xế đang đi giao
        Delivered = 4,  // đã giao
        Done = 5
    }
}

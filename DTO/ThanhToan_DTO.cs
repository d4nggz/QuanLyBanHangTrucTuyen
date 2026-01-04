using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ThanhToan_DTO
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public int MethodId { get; set; }
        public string MethodName { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SecurityMS.Core.Models
{
    public class ExchangeModel
    {
        public DateTime ExchangeDate { get; set; }
        public int ExchangeType { get; set; }
        public long? ExchangeTo { get; set; }
        public string ExchangeName { get; set; }
        public List<ExchangeItemModel> ExchangeItems { get; set; }
    }

    public class ExchangeItemModel
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
    }
}

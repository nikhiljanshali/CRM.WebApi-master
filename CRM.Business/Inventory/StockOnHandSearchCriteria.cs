using System;
using System.Collections.Generic;

namespace CRM.Business.Inventory {
    public class StockOnHandSearchCriteria {
        public string StoneId { get; set; }
        public List<string> Kapan { get; set; }
        public decimal? MinSize { get; set; }
        public decimal? MaxSize { get; set; }
        public List<string> Shape { get; set; }
        public List<string> Color { get; set; }
        public List<string> Clarity { get; set; }
        public List<string> Cut { get; set; }
        public List<string> Polish { get; set; }
        public List<string> Symmetry { get; set; }
        public List<string> Fluorescence { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool? IsHold { get; set; }
        public string EmployeeId { get; set; }

        public StockOnHandSearchCriteria() {
        }

    }
}
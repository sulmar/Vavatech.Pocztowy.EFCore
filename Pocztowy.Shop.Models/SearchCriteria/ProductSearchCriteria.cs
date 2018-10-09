using System;
using System.Collections.Generic;
using System.Text;

namespace Pocztowy.Shop.Models.SearchCriteria
{

    public class Range<T>
    {
        public T From { get; set; }
        public T To { get; set; }
    }

    public abstract class SearchCriteria
    {

    }

    public class OrderSearchCriteria : SearchCriteria
    {
        public Range<DateTime?> Period { get; set; }

        //public DateTime? From { get; set; }
        //public DateTime? To { get; set; }
    }

    public class ProductSearchCriteria : SearchCriteria
    {
        public string Color { get; set; }
        public string Barcode { get; set; }
        public float? Weight { get; set; }

        //public decimal? From { get; set; }
        //public decimal? To { get; set; }

        public Range<decimal?> UnitPrice { get; set; }


        public ProductSearchCriteria()
        {
            UnitPrice = new Range<decimal?>();
        }
    }
}

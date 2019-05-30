using System.Collections.Generic;
using System;
namespace TPCIP.CommonDomain
{
    public class ProductPrice
    {
        //Aviraj Holkar:20-02-2016: Added Properties nodes (stores child products), text (Stores product Name) and badge (stores Product price)
        public string Name { get; set; }
        public string text { get; set; }
        public string ParentName { get; set; }
        public string badge { get; set; }
        public decimal Price { get; set; }
        public int Level { get; set; }
        public List<ProductPrice> nodes { get; set; }
    }

    public class HouseholdProductPrice
    {
        public ProductPrice ProductPrice { get; set; }
        public string SerializedProductTree { get; set; }
        public string MainProductPrice { get; set; }
    }
}

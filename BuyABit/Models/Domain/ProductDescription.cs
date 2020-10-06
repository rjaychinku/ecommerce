using System.Collections.Generic;

namespace BuyABit.Models
{
    public class ProductColour
    { 
        public int ProductColourId { get; set; }
        public string Name { get; set; } 
              public int DropDownOrder { get; set; } 
    }

    public class ProductSize
    {
        public int ProductSizeId { get; set; }
        public string Name { get; set; } 

           public int DropDownOrder { get; set; } 
    }
}
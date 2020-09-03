using System.ComponentModel.DataAnnotations;
using myShop.Models;

public class BaseAddress {
        [Key]
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string StreetName {get ;  set  ;}
        public string ApartmentSuite {get ;  set  ;}
        public string City { get; set; }
        public string PostalCode { get; set; }
        public ApplicationUser buyer {get; set;}    
    }
      public class ShippingAddress : BaseAddress   {
    }
      public class BillingAddress : BaseAddress    {      
      public string TaxNumber { get; set; }
      public string CardNumber { get; set; }
    }


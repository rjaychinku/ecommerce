using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }


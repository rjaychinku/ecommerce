using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyABit.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(100)")]
        public string FullName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

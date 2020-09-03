using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace myShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(100)")]
        public string FullName { get; set; }
    }
}

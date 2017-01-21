using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Surname { get; set; }
        [Required]
        public string Othername { get; set; }
        [Required]
        public string Sex { get; set; }

        [Display(Name= "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Status { get; set; }

        public string Role { get; set; }
        public string Flag { get; set; }
        public DateTime Keydate { get; set; }
    }
}

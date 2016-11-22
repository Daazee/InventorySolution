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
        public string Surname { get; set; }
        public string Othername { get; set; }
        public string Sex { get; set; }

        [Display(Name= "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public int Status { get; set; }

        public string Role { get; set; }
        public string Flag { get; set; }
        public DateTime Keydate { get; set; }
    }
}

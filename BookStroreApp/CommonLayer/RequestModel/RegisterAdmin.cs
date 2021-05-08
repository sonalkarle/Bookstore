using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
     public  class RegisterAdmin
    {

        [Required]
        public string AdminName { get; set; }


        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }

    }
}

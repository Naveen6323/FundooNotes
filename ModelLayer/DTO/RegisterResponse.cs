using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class RegisterResponse
    {
        [Required(ErrorMessage="name is required")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "last is required")]

        public string last { get; set; }
        [Required(ErrorMessage = "eamil is required")]

        public string email { get; set; }
        [Required(ErrorMessage = "pass is required")]

        public string pass { get; set; }

    }
}

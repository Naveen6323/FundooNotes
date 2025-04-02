using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class RegisterModel
    {
        [Required(ErrorMessage="name is required")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "last is required")]

        public string lastname { get; set; }
        [Required(ErrorMessage = "eamil is required")]

        public string email { get; set; }
        [Required(ErrorMessage = "pass is required")]

        public string password { get; set; }

    }
}

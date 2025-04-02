using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class ResetPasswordModel
    {
        public string password { get; set; }
        public string confirmpassword { get; set; }
    }
}

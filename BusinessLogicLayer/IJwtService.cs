using DataAcessLayer.Entity;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IJwtService
    {
        Task<LoginResponseModel> Login(string email, string pass);
        Task Register(RegisterModel data);
        Task ForgotPassword(PasswordResetRequest data);
        Task ResetPassword(string token,string password, string confirmpassword);

    }
}

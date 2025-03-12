using DataAcessLayer.Entity;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IJwtService
    {
        Task<LoginResponseModel> Login(string email, string pass);
        Task Register(RegisterResponse data);
        Task ForgotPassword(PasswordResetRequest data);
        Task ResetPassword(string email, string token, string newPassword);

    }
}

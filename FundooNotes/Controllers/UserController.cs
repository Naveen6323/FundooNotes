using BusinessLogicLayer;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using ModelLayer.DTO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtService authenticate;

        public UserController(IJwtService authenticate)
        {
            this.authenticate = authenticate;
        }
        [HttpPost]
        public async Task<ActionResult> Post(RegisterResponse data)
        {
            try
            {
                await authenticate.Register(data);
                return Ok("user registered succesfully");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(string email, string pass)
        {
            try
            {
                var token=await authenticate.Login(email, pass);
                if (token == null)
                {
                    return Unauthorized("null");
                }
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(PasswordResetRequest data)
        {
            try
            {
                await authenticate.ForgotPassword(data);
                return Ok("Password reset link sent.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string pass, string confirmpass)
        {
            try
            {
                await authenticate.ResetPassword(token, pass, confirmpass);
                return Ok("Password reset successful.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }





    }
}

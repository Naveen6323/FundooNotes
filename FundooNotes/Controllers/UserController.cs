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
        public async Task<ActionResult> Post(RegisterModel data)
        {
            try
            {
                await authenticate.Register(data);
                return Ok(new { Message="user registered succesfully" });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginReq req)
        {
            try
            {
                var token=await authenticate.Login(req.email, req.password);
                if (token == null)
                {
                    return Unauthorized(new { message = "null" });
                }
                return Ok(new {data=token});
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(PasswordResetRequest data)
        {
            try
            {
                await authenticate.ForgotPassword(data);
                return Ok(new { Message = "Password reset link sent." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPost("resetpassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel data)
        {
            try
            {
                if (data.password != data.confirmpassword)
                {
                    throw new ArgumentException("Password and Confirm Password should be the same.");
                }
                var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return BadRequest("Authorization token is missing or invalid.");
                }

                // Extract token (after "Bearer ")
                var token = authHeader.Split(" ")[1];
                await authenticate.ResetPassword(token,data.password, data.confirmpassword);
                return Ok(new { Message = "Password reset successful." });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }





    }
}

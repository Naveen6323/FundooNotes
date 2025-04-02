using DataAcessLayer.Context;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTO;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class JwtService:IJwtService
    {
        private readonly UserDBContext user;
        private readonly IConfiguration configuration;
        private readonly IEmailSender emailSender;

        public JwtService(UserDBContext context,IConfiguration configuration, IEmailSender emailSender) 
        {
            this.user = context;
            this.configuration = configuration;
            this.emailSender = emailSender;
        }
        public async Task<LoginResponseModel> Login(string email, string password)
        {
            var existingUser = await user.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser == null)
            {
                throw new Exception("user not found");
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, existingUser.Password);
            if (isValid == false)
            {
                throw new Exception("invalid password");
            }
            //var userAccount = await user.Users.FirstOrDefaultAsync(user => user.Email == email);

            var issuer = configuration["JwtConfig:Issuer"];
            var audience = configuration["JwtConfig:Audience"];
            var key = configuration["JwtConfig:Key"];
            var tokenvalidity = configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenexpiry = DateTime.UtcNow.AddMinutes(tokenvalidity);
            var tokenDescriptor= new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,existingUser.ID.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,existingUser.Email)
                }),
                Expires = tokenexpiry,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken =  tokenHandler.CreateToken(tokenDescriptor);
             var accessToken =  tokenHandler.WriteToken(securityToken);
             return new LoginResponseModel
            {
                ID = existingUser.ID,
                Token = accessToken
            };

        }
        public async Task Register(RegisterModel data)
        {
            var existingUser = user.Users.Any(u => u.Email == data.email);
            
            if (existingUser == true)
            {
                throw new Exception("email already exists");
            }
            data.password = BCrypt.Net.BCrypt.HashPassword(data.password);
            var reg = new User
            {
                FirstName = data.firstname,
                LastName = data.lastname,
                Email = data.email,
                Password = data.password
            };
            await user.Users.AddAsync(reg);
            await user.SaveChangesAsync();
        }
        public async Task ForgotPassword(PasswordResetRequest data)
        {
            var existingUser = await user.Users.FirstOrDefaultAsync(u => u.Email == data.Email);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenvalidity = configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var issuer = configuration["JwtConfig:Issuer"];
            var audience = configuration["JwtConfig:Audience"];
            var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:ResetKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, existingUser.ID.ToString()) 
                }),
                
                Expires = DateTime.UtcNow.AddMinutes(tokenvalidity),
                Issuer=issuer,
                Audience=audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var resetToken = tokenHandler.WriteToken(token);

            //var resetLink = $"token={resetToken}";
            var resetLink = $"http://localhost:4200/reset?token={resetToken}";


            string emailBody = $"<p>Click <a href='{resetLink}'>here</a> to reset your password. This link is valid for 30 minutes.</p>";

            await emailSender.SendEmailAsync(data.Email, "Password Reset", resetLink);
        }
        public async Task ResetPassword(string token,string pass, string confirmpass)
        { 

            

            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claimsPrincipal;

            try
            {
                // Validate token
                claimsPrincipal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidAudience = configuration["JwtConfig:Audience"],
                    ClockSkew = TimeSpan.Zero,  // No grace period for expiration
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:ResetKey"]))
                }, out _);
            }
            catch (SecurityTokenException)
            {
                throw new UnauthorizedAccessException("Invalid or expired token.");
            }

            // Extract email from claims
            var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                throw new Exception("Email not found in token.");
            }

            // Fetch user by id
            var existingUser = await user.Users.FirstOrDefaultAsync(u => u.ID == int.Parse(id));
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            // Reset password
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(pass);
            await user.SaveChangesAsync();
        }




    }
}

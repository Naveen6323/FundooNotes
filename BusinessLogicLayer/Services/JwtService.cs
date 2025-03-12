using DataAcessLayer.Context;
using DataAcessLayer.Entity;
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
        public async Task<LoginResponseModel> Login(string email, string pass)
        {
            var existingUser = await user.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser == null)
            {
                throw new Exception("user not found");
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(pass, existingUser.Password);
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
        public async Task Register(RegisterResponse data)
        {
            var existingUser = user.Users.Any(u => u.Email == data.email);
            
            if (existingUser == true)
            {
                throw new Exception("email already exists");
            }
            data.pass = BCrypt.Net.BCrypt.HashPassword(data.pass);
            var reg = new User
            {
                FirstName = data.firstname,
                LastName = data.last,
                Email = data.email,
                Password = data.pass
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
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, existingUser.Email) ,
                                                     new Claim(ClaimTypes.NameIdentifier, existingUser.ID.ToString()) 
                }),
                
                Expires = DateTime.UtcNow.AddMinutes(tokenvalidity),
                Issuer=issuer,
                Audience=audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var resetToken = tokenHandler.WriteToken(token);

            var resetLink = $"token={resetToken}";
            //var resetLink = $"http://localhost:5256/api/users/reset-password?email=naveenveerabattini%40gmail.com&token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6Im5hdmVlbn";


            string emailBody = $"<p>Click <a href='{resetLink}'>here</a> to reset your password. This link is valid for 30 minutes.</p>";

            await emailSender.SendEmailAsync(data.Email, "Password Reset", resetLink);
        }

        public async Task ResetPassword(string token, string pass, string confirmpass)
        {
            //    if (pass != confirmpass) throw new Exception("pass and confirmpass shuld be same");
            //    var handler = new JwtSecurityTokenHandler();
            //    var jwtToken = handler.ReadJwtToken(token);
            //    var email = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            //    var existingUser = await user.Users.FirstOrDefaultAsync(u => u.Email == email);
            //    if (existingUser == null)
            //    {
            //        throw new Exception("User not found");
            //    }

            //    var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:ResetKey"]);

            //    try
            //    {
            //        handler.ValidateToken(token, new TokenValidationParameters
            //        {
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateLifetime = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(key),
            //            ValidateIssuerSigningKey = true
            //        }, out SecurityToken validatedToken);
            //    }
            //    catch (Exception)
            //    {
            //        throw new Exception("Invalid or expired token");
            //    }

            //    // Reset password  
            //    existingUser.Password = BCrypt.Net.BCrypt.HashPassword(pass);
            //    await user.SaveChangesAsync();
            if (pass != confirmpass)
            {
                throw new ArgumentException("Password and Confirm Password should be the same.");
            }

            // Fetch Reset Key from configuration
            var resetKey = configuration["JwtConfig:ResetKey"];
            if (string.IsNullOrEmpty(resetKey))
            {
                throw new InvalidOperationException("Reset key is not configured.");
            }

            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claimsPrincipal;

            try
            {
                // Validate token
                var key = Encoding.ASCII.GetBytes(resetKey);
                claimsPrincipal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    //ValidateIssuer = false,
                    //ValidateAudience = false,
                    //ValidateLifetime = true,
                    //IssuerSigningKey = new SymmetricSecurityKey(key),
                    //ValidateIssuerSigningKey = true
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtConfig:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,  // No grace period for expiration
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true
                }, out _);
            }
            catch (SecurityTokenException)
            {
                throw new UnauthorizedAccessException("Invalid or expired token.");
            }

            // Extract email from claims
            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Email not found in token.");
            }

            // Fetch user by email
            var existingUser = await user.Users.FirstOrDefaultAsync(u => u.Email == email);
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

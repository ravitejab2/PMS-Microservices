using IdentityAPI.Data;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static IdentityAPI.Data.IdentityModel;

namespace IdentityAPI.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IConfiguration configuration, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser()
            {
                Title = model.Title,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                DateOfBirth = model.DateOfBirth,
                ContactNumber = model.ContactNumber,
                Role=model.Role,
                IsPwdChanged=false
            };
            
            var roleExist = await _roleManager.RoleExistsAsync(user.Role);
            if (!roleExist)
            {
               await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Role
                });
            }
            if (user.Role == "user")
            {
                user.IsPwdChanged = true;
            }
            var result= await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, model.Role);
            return result;
        }

        public async Task<string> LoginAsync(SignInModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            if (result.Succeeded)
            {
                
                var authClaims = new List<Claim>
                {
                    new Claim("Role",user.Role),
                    new Claim("FirstName",user.FirstName),
                    new Claim("Email",user.Email)
                };
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            if (result.IsNotAllowed)
            {
                return "Not allowed";
            }
            if (result.IsLockedOut)
            {
                return "Locked out";
            }

            return null;        
        }

        
    }
    
    
}

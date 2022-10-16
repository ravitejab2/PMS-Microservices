using IdentityAPI.Models;
using IdentityAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityAPI.Data;
using static IdentityAPI.Data.IdentityModel;
using Microsoft.EntityFrameworkCore;
using PMS.UserAPI.Models;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(IAccountRepository accountRepository, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<object> RegisterUser([FromBody] SignUpModel model)
        {
            //var result = await _accountRepository.SignUpAsync(signUpModel);

            var user = new ApplicationUser()
            {
                Title = model.Title,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                DateOfBirth = model.DateOfBirth,
                ContactNumber = model.ContactNumber,
                Role = model.Role.ToLower(),
                IsPwdChanged = false
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

            if (user.Role != "user")
            {
                model.Password = _configuration["Password"];
                model.ConfirmPassword = _configuration["Password"];
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, model.Role);
          

            if (result.Succeeded)
            {
                await SendWelcomeEmail(model);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"User Registered Successfully",null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, " ", result.Errors.Select(x => x.Description).ToArray()));
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<object> LoginUser([FromBody] SignInModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Email);
            
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            if (result.Succeeded)
            {
                var roles = (await _userManager.GetRolesAsync(user)).ToList();
               
                var appuser = new UserDTO(user.Id, user.FirstName, user.LastName, user.Email,user.IsPwdChanged, roles);
               
                appuser.Token = GenerateToken(user, roles);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", appuser));
            }
            if (result.IsLockedOut)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Account Locked out please contact Admin", null));
            }
            else
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "invalid Email or password", null));
            }

        }
        private string GenerateToken(ApplicationUser user, List<string> roles)
        {

            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>(){
     
               new Claim(JwtRegisteredClaimNames.Email,user.Email),
               new Claim(_options.ClaimsIdentity.RoleClaimType,user.Role),

           };
            

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"]
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

    
        [HttpPost("forgot-password")]
        public async Task<object> ForgotPassword(ForgotPasswordModel model)
        {
            var user= await _userManager.FindByNameAsync(model.Email);
            if (user !=null)
            {
                //string passwordhash= _userManager.PasswordHasher
                await _userManager.RemovePasswordAsync(user);
                var result= await _userManager.AddPasswordAsync(user, _configuration["Password"]);
                user.IsPwdChanged = false;
                await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await SendForgotPasswordEmail(user, _configuration["Password"]);
                   return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Password sent to email and please change the password once you login", null));
                }
                else
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in update password, please try after sometime", null));
                }
            }
                         
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Email id not found", null));
            
            
        }

        [HttpPost("change-password/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Object> ChangePassword(int id,ChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _userManager.ChangePasswordAsync(user,
                        model.CurrentPassword, model.NewPassword);
            //user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,model.NewPassword);
           
           
            if (result.Succeeded)
            {
                user.IsPwdChanged = true;
                await _userManager.UpdateAsync(user);
                await SendChangePasswordEmail(user);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Password updated successfully. please login", null));
            }
            else
            {
                
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Password mismatch.Please try again", null));
            }
        }

        public async Task<object> SendWelcomeEmail(SignUpModel model)
        {
            string Subject = "Welcome to CT Hospital";
            string Body = $"Hello {model.FirstName} {model.LastName}.\nWelome to CT Hospital.\nThanks for registering on the portal. Here are your credential details for loging into the portal.\nUsername/Email : {model.Email} \nPassword : {model.Password}  \nPlease change the password once you login for first time.";

           await _emailService.SendEmail(model.Email, Subject, Body);

            return Ok();
        }

        public async Task<object> SendForgotPasswordEmail(ApplicationUser model, string password)
        {
            string Subject = "Password Changed Successfully";
            string Body = $"Hello {model.FirstName}  {model.LastName} \n . Here is your updated Password : {password}   \n Please change the password once you login for first time. \n Thanks, \n Admin \n CT Hospital";

            await _emailService.SendEmail(model.Email, Subject, Body);

            return Ok();
        }
        public async Task<object> SendChangePasswordEmail(ApplicationUser model)
        {
            string Subject = "Password Changed Successfully";
            string Body = $"Hello {model.FirstName}  {model.LastName} \n . Your  Password is updated successfully  \n Please login again \n Thanks, \n Admin \n CT Hospital";

            await _emailService.SendEmail(model.Email, Subject, Body);

            return Ok();
        }

        [HttpGet("allPatients")]
        public async Task<object> GetAllPatients()
        {
            var result=await _userManager.Users.Where(x => x.Role == "user").ToListAsync();
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "All Patients details:", result));
        }

        [HttpGet("allPhysicians")]
        public async Task<object> GetAllPhysicians()
        {
            var result=await _userManager.Users.Where(x => x.Role == "physician").ToListAsync();
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "All Physician details:", result));
        }

        [HttpGet("allNurses")]
        public async Task<object> GetAllNurses()
        {
            var result = await _userManager.Users.Where(x => x.Role == "nurse").ToListAsync();
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "All Nurses details:", result));
        }

        [HttpGet("allusers")]
        public async Task<object> GetAllUsers()
        {
            var result = await _userManager.Users.ToListAsync();
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "All users details:", result));
        }

        [HttpGet("user/{id}")]
        public async Task<object> GetUserById(int id)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id==id);
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User Deatils", result));
        }

        [HttpGet("profile-user/{id}")]
        public async Task<object> GetUserProfileById(int id)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                UserProfileModel employees = new UserProfileModel();
                employees.Id = result.Id;
                employees.Name = result.FirstName + " " + result.LastName;
                employees.Role = result.Role;
                employees.Title = result.Title;
                employees.Contact = result.ContactNumber;
                employees.DateOfBirth = result.DateOfBirth;
                employees.Email = result.Email;
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User Deatils", employees));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User Not Found", null));
        }

        [HttpGet("employee-profile/{id}")]
        public async Task<object> GetEmployeeProfileById(int id)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                UserProfileModel employees = new UserProfileModel();
                employees.Id = result.Id;
                employees.Name = result.FirstName + " " + result.LastName;
                employees.Role = result.Role;
                employees.Title = result.Title;
                employees.Contact = result.ContactNumber;
                employees.DateOfBirth = result.DateOfBirth;
                employees.Email = result.Email;
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User Deatils", employees));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User Not Found", null));
        }


        [HttpGet("GetEmployees")]
        public async Task<object> GetAllEmployees()
        {
            var result = await _userManager.Users.Where(x => x.Role == "physician" || x.Role == "nurse" || x.Role == "admin").ToListAsync();
            List<EmployeeDetailsModel> employees = result.AsEnumerable()
                                      .Select(o => new EmployeeDetailsModel
                                      {
                                          Id = o.Id,
                                          Name = o.FirstName + " " + o.LastName,
                                          Email = o.Email,
                                          Role = o.Role,
                                          Lockout = o.LockoutEnd,
                                      }).ToList();
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "All Patients details:", employees));
        }

        [HttpPost("sendLocation")]
        [AllowAnonymous]
        public async Task<object> SendLocationEmail([FromBody] LocationModel model)
        {
            string Subject = "Emergency User location";
            string Body = $"Hello below are the users location details\n Latitude: {model.Latitude}  & Longitude: {model.Longitude} \n ";
            await _emailService.SendEmail(model.Email, Subject, Body);
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Location Sent to Hospital", null));
        }


        [HttpGet("users/{id}")]
        public async Task<object> GetUsersById(int id)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                EmployeeDetailsModel employees = new EmployeeDetailsModel
                {
                    Id = result.Id,
                    Name = result.FirstName + " " + result.LastName,
                    Email = result.Email,
                    Role = result.Role,


                };




                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", employees));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error While getting details", null));

        }


        [HttpGet("GetAllPhysicians")]
        public async Task<object> GetAllPhysician()
        {
            var result = await _userManager.Users.Where(x => x.Role == "physician").ToListAsync();
            List<EmployeeDetailsModel> employees = result.AsEnumerable()
                                      .Select(o => new EmployeeDetailsModel
                                      {
                                          Id = o.Id,
                                          Name = o.FirstName + " " + o.LastName,
                                          Email = o.Email,
                                          Role = o.Role,
                                          Lockout = o.LockoutEnd,
                                      }).ToList();
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", employees));
        }

        [HttpGet("GetPatient")]
        public async Task<object> GetAllPatient()
        {
            var result = await _userManager.Users.Where(x => x.Role == "user").ToListAsync();
            List<EmployeeDetailsModel> employees = result.AsEnumerable()
                                      .Select(o => new EmployeeDetailsModel
                                      {
                                          Id = o.Id,
                                          Name = o.FirstName + " " + o.LastName,
                                          Email = o.Email,
                                          Role = o.Role,
                                          Lockout = o.LockoutEnd,
                                      }).ToList();
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "All Patients details:", employees));
        }

        [HttpPut("editEmployee/{id}")]
        public async Task<object> EditEmployee(int id, [FromBody] EditEmployeeModel model)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                result.FirstName = model.FirstName;
                result.LastName = model.LastName;
                result.ContactNumber = model.ContactNumber;
                result.DateOfBirth = model.DateOfBirth;
                var updated = await _userManager.UpdateAsync(result);
                if (updated.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Deatils Updated Successfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Error in updating  ", updated.Errors.Select(x => x.Description).ToArray()));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User Not found", null));
        }


    }
}

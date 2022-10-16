using IdentityAPI.Data;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "employee")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost("unlockuser/{id}")]
        
        public async Task<object> UnLockoutuser(int id)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                if (await _userManager.IsLockedOutAsync(user))
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                    user.LockoutEnd = null;
                    await _userManager.UpdateAsync(user);
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User unlocked successfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User not locked ", null));

            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "", null));

        }

        [HttpPost("activate-user")]
        public async Task<object> ActivateUser([FromBody] int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                if (await _userManager.IsLockedOutAsync(user))
                {
                    await _userManager.SetLockoutEndDateAsync(user, null);
                    user.LockoutEnd = null;
                    await _userManager.UpdateAsync(user);
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User unlocked successfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User not locked ", null));
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.Error, "", null));
        }
        [HttpPost("deactivate-user")]
        public async Task<object> DeactivateUser([FromBody] int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddYears(10));
                user.LockoutEnd = DateTimeOffset.Now.AddYears(10);
                await _userManager.UpdateAsync(user);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User blocked successfully", null));
                // return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User not locked ", null));
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
            return await Task.FromResult(employees);
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
            return await Task.FromResult(employees);
        }


    }
}

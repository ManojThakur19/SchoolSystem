using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebESchool.Authorization;
using WebESchool.ViewModel;
using WebESchoolData.Model;
using static WebESchoolData.ApiResponseCodes;

namespace WebESchool.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [Route("api/token")]
        [HttpGet]
        public async Task<dynamic> Token()
        {
            var token = new JwtTokenBuilder()
                          .AddSecurityKey(JwtSecurityKey.Create("This is my Web_Echool key"))
                          .AddSubject("Web_Echool")
                          .AddIssuer("Web_Echool")
                          .AddAudience("Web_Echool")
                          .AddClaim("Id", "0")
                          .AddClaim("Role", "0")
                          .AddExpiry(1)
                          .Build();

            object response = HttpResponse((int)HttpStatusCode.OK, "Token Create successfully", token);
            return response;
        }
        [AllowAnonymous]
        [Route("api/user/login")]
        [HttpPost]
        public async Task<dynamic> LogIn(LoginViewModel model)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    var roleid = await userManager.GetRolesAsync(user);

                    var data = new JwtTokenBuilder()
                                      .AddSecurityKey(JwtSecurityKey.Create("This is my Web_Echool key"))
                                      .AddSubject("Web_Echool")
                                      .AddIssuer("Web_Echool")
                                      .AddAudience("Web_Echool")
                                      .AddClaim("Id", user.Id.ToString())
                                      // .AddClaim("PhoneNumber", user.PhoneNumber)
                                      .AddClaim("role", "School")
                                      .AddClaim("Email", user.Email)
                                      .AddExpiry(1)
                                      .Build();


                    return new
                    {
                        USER_ID = user.Id,
                        EMAIL = user.Email,
                        FULL_NAME = user.UserName,
                        IS_ACTIVE = true,
                        token = data,
                        is_super_admin = false,
                        ROLE_NAME = "School"
                    };
                }
                return StatusCodes.Status200OK;
            }
            catch (Exception e)
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        [AllowAnonymous]
        [Route("api/user/registration")]
        [HttpPost]
        public async Task<dynamic> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var existingUser = await userManager.FindByEmailAsync(model.Email);
                    if (existingUser != null)
                    {
                        return StatusCodes.Status200OK;
                    }
                    var responce =  await userManager.CreateAsync(user,model.Password);
                    if (responce.Succeeded)
                    {
                        var myuser = await userManager.FindByEmailAsync(model.Email);
                        var roleid = await userManager.GetRolesAsync(myuser);

                        var defaultRole = string.Empty;
                        if(roleid.Count > 0)
                        {
                            defaultRole = roleid.FirstOrDefault();
                        }
                         
                        var data = new JwtTokenBuilder()
                                    .AddSecurityKey(JwtSecurityKey.Create("This is my Web_Echool key"))
                                      .AddSubject("Web_Echool")
                                      .AddIssuer("Web_Echool")
                                      .AddAudience("Web_Echool")
                                      .AddClaim("Id", user.Id.ToString())
                                     // .AddClaim("PhoneNumber", user.PhoneNumber)
                                      .AddClaim("role", defaultRole)
                                      .AddClaim("Email", user.Email)
                                      .AddExpiry(1)
                                      .Build();

                        return StatusCodes.Status200OK;
                    }
                    else
                    {
                        return StatusCodes.Status500InternalServerError;
                    }
                }
                else
                {
                    return StatusCodes.Status500InternalServerError;
                }
            }
            catch (Exception e)
            {
                return StatusCodes.Status500InternalServerError;
            }
        }
    }
}

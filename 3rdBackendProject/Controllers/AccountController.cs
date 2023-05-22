using _3rdBackendProject.Models;
using _3rdBackendProject.Utilities.Extentions;
using _3rdBackendProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace _3rdBackendProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM newUser)
        {

            string radioGender;
            if (newUser.IsMale == true)
            {radioGender = "Male"; } 

            else { radioGender = "Female"; }


               if (!ModelState.IsValid) return View();
            Regex checkEmail = new Regex(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(@"
            + @"(([-a-z0-9]+\.)*[a-z]{2,}|"
            + @"(\d{1,3}\.){3}\d{1,3}))"
            + @"(:\d{4})?"
            + @"(([-a-z0-9]+\.)*[a-z0-9]{2,})?$"); 
            
            if (!checkEmail.IsMatch(newUser.Email))
            { return View(); }

            newUser.Surname.CheckUserName();                                                                                                                                                                                                                          newUser.Name.CheckUserName();
            newUser.Name.Capitalize();
            newUser.Surname.Capitalize();

            AppUser user = new AppUser
                {
                    Name = newUser.Name,
                    Surname = newUser.Surname,
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                    Gender=radioGender
                };
            
            IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();

            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
    }
}

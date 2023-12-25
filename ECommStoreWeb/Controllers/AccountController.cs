using ECommStoreWeb.Data;
using ECommStoreWeb.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommStoreWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _roleManager=roleManager;
            _webHostEnvironment=webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            string[] roles = { "seller", "customer" };
            foreach (string role in roles)
            {
                if(!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                };

                var res = await _userManager.CreateAsync(user, model.Password);
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user,"customer");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["success"] = $"Hello, {user.Name}. Your Registration is Successfull!";
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            var res = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
            if(res.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt!");
            TempData["failed"] = "Login failed due to incorrect information";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (_signInManager.IsSignedIn(User))
            {
				await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
			}

			return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if(!_signInManager.IsSignedIn(User))
                return NotFound();

            var user = await _signInManager.UserManager.GetUserAsync(User);
            Profile model = new Profile()
            {
                Name = user.Name, Email = user.Email, Age = user.Age, PhotoLink = user.PhotoLink, PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(Profile model)
        {
            if(ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.GetUserAsync(User);
                user.Name = model.Name;
                user.Email = model.Email;
                string oldPhotoLink = user.PhotoLink;
                user.PhotoLink = UploaadProfilePhoto(model.Photo, oldPhotoLink);
                user.PhoneNumber = model.PhoneNumber;
                user.Age = model.Age;

                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    TempData["success"] = "Changes are saved successfully!";
                    return RedirectToAction("Profile");
                }
            }
            return View(model);
        }

        public string UploaadProfilePhoto(IFormFile? file, string? oldPhotoLink)
        {
            if (file == null)
                if (oldPhotoLink == null)
                    return "";
                else
                    return oldPhotoLink;

            //upload profile image
            string fileName = Guid.NewGuid().ToString() + ".jpg"; 
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ProfileImages");

            if(!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string fullPath = Path.Combine(filePath, fileName);
            file.CopyTo(new FileStream(fullPath, FileMode.Create));

            //delete old profile image
            if(oldPhotoLink != null)
            {
                string oldPhotoPath = Path.Combine(filePath, oldPhotoLink);
                if (System.IO.File.Exists(oldPhotoPath))
                {
                    Thread.Sleep(1000);
                    System.IO.File.Delete(oldPhotoPath);
                }

            }
            return fileName;
        }
    }
}

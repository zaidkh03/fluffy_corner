using fluffy_corner.Models;
using fluffy_corner.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fluffy_corner.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register (RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedAt = DateTime.Now
                };
                //add user to database
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Assign default role to the user (optional)
                    if (!await _roleManager.RoleExistsAsync("User"))
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    await _userManager.AddToRoleAsync(user, "User");
                    // Sign in the user after successful registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    // Redirect to home page or any other page after successful registration
                    //////////////
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            TempData["Success"] = "Your account has been created successfully!";
            return View(model);
            }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // lockoutOnFailure: true يفعل خاصية قفل الحساب بعد محاولات فاشلة
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    // 2. جلب بيانات المستخدم الذي سجل دخوله للتو
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    // 3. الفحص السحري: هل هذا المستخدم يمتلك دور "Admin"؟
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        // إذا كان أدمن، حوّله فوراً إلى الـ Dashboard داخل منطقة الـ Admin
                        // تأكدي من مطابقة أسماء الـ Area والـ Controller والـ Action لمشروعك
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }

                    return RedirectToAction("Index", "Home");
                }
                

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Account locked due to multiple failed attempts. Try again later.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //////////////////////////
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // user profile
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            //retrieve the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound();

            
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ApplicationUser updatedData)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // Update only allowed fields (excluding email and username for security reasons)
            user.FirstName = updatedData.FirstName;
            user.LastName = updatedData.LastName;
            user.Address = updatedData.Address;
            user.City = updatedData.City;
            user.Country = updatedData.Country;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = "Profile updated successfully!";
                return RedirectToAction(nameof(MyProfile));
            }

            return View("MyProfile", user);
        }
    }
    }


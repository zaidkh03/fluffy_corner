using fluffy_corner.Data;
using fluffy_corner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fluffy_corner.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            if (category.ImageFile != null)
            {
                category.ImageUrl = await SaveImage(category.ImageFile);
            }

            category.CreatedAt = DateTime.Now;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var oldCategory = await _context.Categories.FindAsync(id);

            if (oldCategory == null)
            {
                return NotFound();
            }

            oldCategory.Name = category.Name;
            oldCategory.Description = category.Description;
            oldCategory.IsActive = category.IsActive;

            if (category.ImageFile != null)
            {
                oldCategory.ImageUrl = await SaveImage(category.ImageFile);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            string folder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "categories"
            );

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/categories/" + fileName;
        }
    }
}
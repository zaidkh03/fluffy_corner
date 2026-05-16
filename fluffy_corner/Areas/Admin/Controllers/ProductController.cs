using fluffy_corner.Data;
using fluffy_corner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fluffy_corner.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories
                .Where(c => c.IsActive)
                .ToListAsync();

            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? ImageFile)
        {
            ModelState.Remove("Category");
            ModelState.Remove("Images");
            ModelState.Remove("Reviews");
            ModelState.Remove("WishlistItems");
            ModelState.Remove("OrderItems");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories
                    .Where(c => c.IsActive)
                    .ToListAsync();

                return View(product);
            }

            product.CreatedAt = DateTime.Now;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    "products"
                );

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                _context.ProductImages.Add(new ProductImage
                {
                    ProductId = product.Id,
                    ImageUrl = "/images/products/" + fileName,
                    IsPrimary = true
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _context.Categories
                .Where(c => c.IsActive)
                .ToListAsync();

            ViewBag.PrimaryImage = product.Images
                .FirstOrDefault(i => i.IsPrimary)?.ImageUrl;

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? ImageFile)
        {
            ModelState.Remove("Category");
            ModelState.Remove("Images");
            ModelState.Remove("Reviews");
            ModelState.Remove("WishlistItems");
            ModelState.Remove("OrderItems");

            if (id != product.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories
                    .Where(c => c.IsActive)
                    .ToListAsync();

                return View(product);
            }

            var oldProduct = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (oldProduct == null)
            {
                return NotFound();
            }

            oldProduct.Name = product.Name;
            oldProduct.SKU = product.SKU;
            oldProduct.ShortDescription = product.ShortDescription;
            oldProduct.Description = product.Description;
            oldProduct.Price = product.Price;
            oldProduct.DiscountPercentage = product.DiscountPercentage;
            oldProduct.StockQuantity = product.StockQuantity;
            oldProduct.Weight = product.Weight;
            oldProduct.Brand = product.Brand;
            oldProduct.AnimalType = product.AnimalType;
            oldProduct.IsFeatured = product.IsFeatured;
            oldProduct.IsActive = product.IsActive;
            oldProduct.CategoryId = product.CategoryId;
            oldProduct.UpdatedAt = DateTime.Now;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    "products"
                );

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                var oldImage = oldProduct.Images.FirstOrDefault(i => i.IsPrimary);

                if (oldImage == null)
                {
                    oldProduct.Images.Add(new ProductImage
                    {
                        ImageUrl = "/images/products/" + fileName,
                        IsPrimary = true
                    });
                }
                else
                {
                    oldImage.ImageUrl = "/images/products/" + fileName;
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
    }
}
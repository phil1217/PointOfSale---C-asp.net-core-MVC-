using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Data;
using Point_Of_Sale.Models;

namespace Point_Of_Sale.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ps = await _context.ProductSupplier.ToListAsync();
            var productSuppliler =new List<ProductSupplierAddModel>();

            foreach (var product in ps)
            {
                ProductSupplierAddModel psvm = new ProductSupplierAddModel();
                psvm.Product = await _context.Products.FindAsync(product.ProductID);
                psvm.ProductSupplier = product;
                productSuppliler.Add(psvm);
            }

            return View(productSuppliler);
        }

        public IActionResult AddNewProduct()
        {
            ViewBag.CategoryID = _context.ProductCategories
                                  .Select(pc => new SelectListItem
                                  {
                                      Value = pc.CategoryID,
                                      Text = pc.CategoryName
                                  }).ToList();

            ViewBag.SupplierID = _context.Suppliers
                                  .Select(s => new SelectListItem
                                  {
                                      Value = s.SupplierID,
                                      Text = s.SupplierName
                                  }).ToList();
            return View();
        }

        public async Task<IActionResult> Add(ProductSupplierAddModel? model)
        {
            if (model == null || model.Product == null || model.ProductSupplier == null || model.ProductCategories == null)
            {
                return BadRequest("Invalid product or product-supplier data.");
            }

            var product = model.Product;
            var productSupplier = model.ProductSupplier;
            var productCategories = model.ProductCategories;

            DateTime now = DateTime.Now;
            string dateString = now.ToString("yyyy-MM-dd HH:mm:ss");

            productSupplier.CreatedAt = dateString;
            productSupplier.UpdatedAt = dateString;

            var existingCategory = await _context.ProductCategories.FirstOrDefaultAsync(p => p.CategoryName == productCategories.CategoryName);

            if (existingCategory == null)
            {
                _context.ProductCategories.Add(productCategories);

                await _context.SaveChangesAsync();

                product.CategoryID = productCategories.CategoryID;

            }
            else
            {
                product.CategoryID = existingCategory.CategoryID;
            }

            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == product.ProductName);

            if (existingProduct == null)
            {
                _context.Products.Add(product);

                await _context.SaveChangesAsync();

                productSupplier.ProductID = product.ProductID;
            }
            else
            {
                productSupplier.ProductID = existingProduct.ProductID;
            }

            _context.ProductSupplier.Add(productSupplier);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ViewProduct(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ps = await _context.ProductSupplier.FirstOrDefaultAsync(p => p.ProductSupplierID == id);
            if (ps == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == ps.ProductID);

            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == ps.SupplierID);

            if (supplier == null)
            {
                supplier = new Suppliers();
            }

            var category = new ProductCategories();

            if (product == null)
            {
                product = new Products();
            }
            else
            {
                category = await _context.ProductCategories.FirstOrDefaultAsync(c => c.CategoryID == product.CategoryID);
            }

            var model = new ProductSupplierViewModel();
            model.Product = product;
            model.ProductSupplier = ps;
            model.ProductCategories = category;
            model.Suppliers = supplier;

            return View(model);
        }

        public async Task<IActionResult> Edit(ProductSupplierAddModel model)
        {
            if (model == null || model.Product == null || model.ProductSupplier == null || model.ProductCategories == null)
            {
                return BadRequest("Invalid product or product-supplier data.");
            }

            var existingProduct = await _context.Products.FindAsync(model.Product.ProductID);
            var existingProductSupplier = await _context.ProductSupplier.FindAsync(model.ProductSupplier.ProductSupplierID);
            var existingProductCategories = await _context.ProductCategories.FindAsync(model.ProductCategories.CategoryID);

            if (existingProduct == null || existingProductSupplier == null || existingProductCategories == null)
            {
                return NotFound();
            }

            existingProductSupplier.ProductSupplierID = model.ProductSupplier.ProductSupplierID;
            existingProductSupplier.SupplierID = model.ProductSupplier.SupplierID;
            existingProductSupplier.Price = model.ProductSupplier.Price;
            existingProductSupplier.QuantityInStock = model.ProductSupplier.QuantityInStock;
            existingProductSupplier.ReorderLevel = model.ProductSupplier.ReorderLevel;
            existingProductSupplier.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            existingProduct.ProductName = model.Product.ProductName;
            existingProduct.Description = model.Product.Description;

            existingProductCategories.CategoryName = model.ProductCategories.CategoryName;
            existingProductCategories.Description = model.ProductCategories.Description;

            _context.Products.Update(existingProduct);
            _context.ProductSupplier.Update(existingProductSupplier);
            _context.ProductCategories.Update(existingProductCategories);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditProduct(string? id)
        {

            ViewBag.SupplierID = _context.Suppliers
                                  .Select(s => new SelectListItem
                                  {
                                      Value = s.SupplierID,
                                      Text = s.SupplierName
                                  }).ToList();

            if (id == null)
            {
                return NotFound();
            }

            var ps = await _context.ProductSupplier.FirstOrDefaultAsync(p => p.ProductSupplierID == id);
            if (ps == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == ps.ProductID);

            if(product == null)
            {
                product = new Products();
            }

            var categories = await _context.ProductCategories.FirstOrDefaultAsync(p => p.CategoryID == product.CategoryID);

            if (categories == null)
            {
                categories = new ProductCategories();
            }

            var model = new ProductSupplierAddModel();
            model.Product = product;
            model.ProductSupplier = ps;
            model.ProductCategories = categories;

            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest("Product Supplier ID is required.");
            }

            var ps = await _context.ProductSupplier.FindAsync(id);
            if (ps == null)
            {
                return NotFound();
            }

            var productsCount = await _context.ProductSupplier.CountAsync(p => p.ProductID == ps.ProductID);
            
            if (productsCount <= 1)
            {
                var product = await _context.Products.FindAsync(ps.ProductID);
               
                var categoriesCount = await _context.Products.CountAsync(p => p.CategoryID == product.CategoryID);
                
                if (categoriesCount <= 1)
                {
                    var category = await _context.ProductCategories.FindAsync(product.CategoryID);

                    if(category != null)
                    {
                        _context.ProductCategories.Remove(category);
                    }

                }

                if (product != null)
                {
                    _context.Products.Remove(product);
                }
            }

            if (productsCount <= 1)
            {
                var product = await _context.Products.FindAsync(ps.ProductID);
                if (product != null)
                {
                    _context.Products.Remove(product);
                }
            }

            _context.ProductSupplier.Remove(ps);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}

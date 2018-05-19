using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyKitchenApp.DBContexts;
using MyKitchenApp.Models;

namespace MyKitchenApp.Pages
{
    public class EditDishModel : BasePageModel
    {
        [BindProperty]
        public Dish Dish { get; set; }
        [BindProperty]
        public string ProductsData { get; set; }

        public List<Product> Products { get; set; }

        public EditDishModel(ApplicationContext db) : base(db) { }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Dish = await _context.Dishes.Include(c => c.ProductDish).ThenInclude(sc => sc.Product).FirstAsync(x => x.Id == id);
            Products = await _context.Products.ToListAsync();

            if (Dish == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Products = await _context.Products.ToListAsync();
            await _context.Attach(Dish).Collection(x => x.ProductDish).LoadAsync();

            if (Dish == null || string.IsNullOrWhiteSpace(Dish.Title))
            {
                if (ModelState.TryAddModelError("Title", "Необходимо ввести название"))
                    return Page();
            }
            if (!ModelState.IsValid) return Page();
            
            Dish.ProductDish.Clear();
            _context.Update(Dish);
            await _context.Flush();

            foreach (var item in ProductsData.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = item.Split(new[] { ':' });
                if (int.TryParse(parts[0], out int id_product) && int.TryParse(parts[1], out int count_product))
                    Dish.Bind(_context.Products.Find(id_product), count_product);
            }

            try
            {
                _context.Update(Dish);
                await _context.Flush();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Dishes.Any(e => e.Id == Dish.Id)) return NotFound();
                else throw;
            }
            return RedirectToPage("DishView", Dish.Id);
        }
    }
}
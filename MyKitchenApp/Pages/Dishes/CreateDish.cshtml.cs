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
    public class CreateDishModel : BasePageModel
    {
        [BindProperty]
        public Dish Dish { get; set; }
        [BindProperty]
        public string ProductsData { get; set; }

        public List<Product> Products { get; set; }

        public CreateDishModel(ApplicationContext db) : base(db) { }

        public async void OnGet()
        {
            Products = await _context.Products.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Products = await _context.Products.ToListAsync(); 

            if (Dish == null || string.IsNullOrWhiteSpace(Dish.Title))
            {
                if (ModelState.TryAddModelError("Title", "Необходимо ввести название"))
                    return Page();
            }

            if (ModelState.IsValid)
            {
                foreach (var item in ProductsData.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var parts = item.Split(new[] { ':' });
                    if (int.TryParse(parts[0], out int id_product) && int.TryParse(parts[1], out int count_product))
                        Dish.Bind(_context.Products.Find(id_product), count_product);
                }

                _context.Dishes.Add(Dish);
                await _context.Flush();
                return RedirectToPage("Dishes");
            }
            return Page();
        }

    }
}
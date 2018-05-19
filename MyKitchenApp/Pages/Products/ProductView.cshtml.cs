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
    public class ProductViewModel : BasePageModel
    {
        public Product Product { get; set; }

        public ProductViewModel(ApplicationContext db) : base(db) { }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Product = await _context.Products.Include(c => c.ProductDish).ThenInclude(sc => sc.Dish).FirstAsync(x => x.Id == id);

            if (Product == null) return NotFound();
            return Page();
        }
    }
}
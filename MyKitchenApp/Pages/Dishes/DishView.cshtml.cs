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
    public class DishViewModel : BasePageModel
    {
        public Dish Dish { get; set; }

        public DishViewModel(ApplicationContext db) : base(db) { }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Dish = await _context.Dishes.Include(c => c.ProductDish).ThenInclude(sc => sc.Product).FirstAsync(x => x.Id == id);

            if (Dish == null) return NotFound();
            return Page();
        }
    }
}
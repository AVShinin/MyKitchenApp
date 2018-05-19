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
    public class DishesModel : BasePageModel
    {
        public List<Dish> Dishes { get; set; }

        public DishesModel(ApplicationContext db) : base(db) { }

        public async void OnGet()
        {
            await _context.Dishes.LoadAsync();
            Dishes = await _context.Dishes.Include(c => c.ProductDish).ThenInclude(sc => sc.Product).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
 
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
                await _context.Flush();
            }
 
            return RedirectToPage();
        }
    }
}
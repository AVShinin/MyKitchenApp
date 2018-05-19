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
    public class ProductsModel : BasePageModel
    {
        public List<Product> Products { get; set; }

        public ProductsModel(ApplicationContext db) : base(db) { }

        public async void OnGet()
        {
            await _context.Products.LoadAsync();
            Products = await _context.Products.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
 
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.Flush();
            }
 
            return RedirectToPage();
        }
    }
}
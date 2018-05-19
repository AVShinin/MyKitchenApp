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
    public class EditProductModel : BasePageModel
    {
        [BindProperty]
        public Product Product { get; set; }

        public EditProductModel(ApplicationContext db) : base(db) { }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Product = await _context.Products.FindAsync(id);

            if (Product == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Product == null || string.IsNullOrWhiteSpace(Product.Title))
            {
                if (ModelState.TryAddModelError("Title", "Необходимо ввести название"))
                    return Page();
            }
            if (!ModelState.IsValid) return Page();
            

            _context.Update(Product);
            await _context.Flush();

            try
            {
                await _context.Flush();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == Product.Id)) return NotFound();
                else throw;
            }
            return RedirectToPage("ProductView", Product.Id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyKitchenApp.DBContexts;

namespace MyKitchenApp.Pages
{
    public class BasePageModel : PageModel
    {
        protected readonly ApplicationContext _context;
        public BasePageModel(ApplicationContext db)=>_context = db;
    }
}

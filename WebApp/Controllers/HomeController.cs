using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin_0306181083.Data;
namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DPContext _context;
        
        public HomeController(DPContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }

    }
}

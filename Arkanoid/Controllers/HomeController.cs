using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Arkanoid.Models;

namespace Arkanoid.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? id)
        {
            if (id == 404)
            {
                return View("NotFound");
            }
            else if (id == 500)
            {
                return View("IntServError");
            }
            else if (id == 401)
            {
                return View("Unath");
            }
            else if (id == 403)
            {
                return View("Forbid");
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            
        }
    }
}

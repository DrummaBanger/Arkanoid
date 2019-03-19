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
        /// <summary>
        /// Получает главную страницу
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Получает страницу конфиденциальности
        /// </summary>
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Обработка ошибок http
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error(int? id)
        {
            switch (id)
            {
                case 404:
                    return View("NotFound");
                case 500:
                    return View("IntServError");
                case 401:
                    return View("Unath");
                case 403:
                    return View("Forbid");
                default:
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}

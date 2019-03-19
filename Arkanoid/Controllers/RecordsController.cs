using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Arkanoid.Models;
using Microsoft.AspNetCore.Authorization;
using DAL.Services;
using Arkanoid.Services;

namespace Arkanoid.Controllers
{
    /// <summary>
    /// Класс управления рекордами
    /// </summary>
    public class RecordsController : Controller
    {
        private readonly IRecordsService _records;

        public RecordsController(IRecordsService records)
        {
            _records = records;
        }

        // GET: Records
        /// <summary>
        /// Получает список рекордов
        /// </summary>
        /// <param name="sortOrder">Сортировка</param>
        /// <param name="searchString">Поиск</param>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View("Index", await this._records.GetRecords(HttpContext.User.Identity.Name));
        }

        // GET: Persons/Details/5
        /// <summary>
        /// Получает данные о персонаже
        /// </summary>
        /// <param name="id">Идентификатор персонажа</param>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var records = await _records.GetDetails((int)id);

            if (records == null)
            {
                return NotFound();
            }

            return View(records);
        }

        // GET: Records/Create
        /// <summary>
        /// Получает страницу создания рекорда
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Records records)
        {
            if (ModelState.IsValid)
            {
                await _records.CreateRecord(records, "add");
                return RedirectToAction(nameof(Index));
            }
            return View(records);
        }

        // GET: Records/Edit/5
        /// <summary>
        /// Получает страницу редактирования рекорда
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var records = await _records.GetDetails((int)id);
            if (records == null)
            {
                return NotFound();
            }
            return View(records);
        }

        // POST: Records/Edit/5
        /// <summary>
        /// Редактирует рекорд
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Records records)
        {
            if (id != records.RecordID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _records.CreateRecord(records, "update");
                }
                catch (DbUpdateConcurrencyException)
                {
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(records);
        }

        // GET: Records/Delete/5
        /// <summary>
        /// Получает страницу удаления персонажа
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var records = await _records.GetDetails((int)id);

            if (records == null)
            {
                return NotFound();
            }

            return View(records);
        }

        // POST: Records/Delete/5
        /// <summary>
        /// Удаляет рекорд
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _records.DeleteRecordsAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

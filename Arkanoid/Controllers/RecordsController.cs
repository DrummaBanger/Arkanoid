﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Arkanoid.Data;
using Arkanoid.Models;
using Microsoft.AspNetCore.Authorization;

namespace Arkanoid.Controllers
{
    public class RecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Records
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["UsernameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Username_desc" : "";
            ViewData["ScoreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Score_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            var Records = from s in _context.Records
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Records = Records.Where(s => s.UserName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Username_desc":
                    Records = Records.OrderByDescending(s => s.UserName);
                    break;
                case "Score_desc":
                    Records = Records.OrderByDescending(s => s.UserScore);
                    break;
                default:
                    Records = Records.OrderBy(s => s.UserName);
                    Records = Records.OrderBy(s => s.UserScore);
                    break;
            }
            return View(await Records.AsNoTracking().ToListAsync());
        }

        // GET: Records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var records = await _context.Records
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (records == null)
            {
                return NotFound();
            }

            return View(records);
        }

        // GET: Records/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,RecordID,UserName,UserScore")] Records records)
        {
            if (ModelState.IsValid)
            {
                _context.Add(records);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(records);
        }

        // GET: Records/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var records = await _context.Records.FindAsync(id);
            if (records == null)
            {
                return NotFound();
            }
            return View(records);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,RecordID,UserName,UserScore")] Records records)
        {
            if (id != records.RecordID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(records);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordsExists(records.RecordID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(records);
        }

        // GET: Records/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var records = await _context.Records
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (records == null)
            {
                return NotFound();
            }

            return View(records);
        }

        // POST: Records/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var records = await _context.Records.FindAsync(id);
            _context.Records.Remove(records);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordsExists(int id)
        {
            return _context.Records.Any(e => e.RecordID == id);
        }
    }
}
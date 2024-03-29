﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleStockTracker.Data;
using SimpleStockTracker.Models;

namespace SimpleStockTracker.Controllers
{
    [Authorize]
    public class HoldingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoldingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Holdings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Holding.Include(h => h.Account);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Holdings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Holding == null)
            {
                return View("404");
            }

            var holding = await _context.Holding
                .Include(h => h.Account)
                .FirstOrDefaultAsync(m => m.HoldingId == id);
            if (holding == null)
            {
                return View("404");
            }

            return View("Details", holding);
        }

        // GET: Holdings/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Name");
            return View("Create");
        }

        // POST: Holdings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoldingId,Ticker,TradeDate,TradeType,Quantity,Price,AccountId")] Holding holding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(holding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Name", holding.AccountId);
            return View("Create", holding);
        }

        // GET: Holdings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Holding == null)
            {
                return View("404");
            }

            var holding = await _context.Holding.FindAsync(id);
            if (holding == null)
            {
                return View("404");
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Name", holding.AccountId);
            return View("Edit", holding);
        }

        // POST: Holdings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("HoldingId,Ticker,TradeDate,TradeType,Quantity,Price,AccountId")] Holding holding)
        {
            if (id != holding.HoldingId)
            {
                return View("404");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(holding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoldingExists(holding.HoldingId))
                    {
                        return View("404");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Name", holding.AccountId);
            return View("Edit", holding);
        }

        // GET: Holdings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Holding == null)
            {
                return View("404");
            }

            var holding = await _context.Holding
                .Include(h => h.Account)
                .FirstOrDefaultAsync(m => m.HoldingId == id);
            if (holding == null)
            {
                return View("404");
            }

            return View("Delete", holding);
        }

        // POST: Holdings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Holding == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Holding'  is null.");
            }
            var holding = await _context.Holding.FindAsync(id);
            if (holding != null)
            {
                _context.Holding.Remove(holding);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoldingExists(int id)
        {
          return _context.Holding.Any(e => e.HoldingId == id);
        }
    }
}

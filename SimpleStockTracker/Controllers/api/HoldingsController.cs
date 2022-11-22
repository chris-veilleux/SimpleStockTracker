using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleStockTracker.Data;
using SimpleStockTracker.Models;

namespace SimpleStockTracker.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoldingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HoldingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Holdings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Holding>>> GetHolding()
        {
            return await _context.Holding.ToListAsync();
        }

        // GET: api/Holdings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Holding>> GetHolding(int id)
        {
            var holding = await _context.Holding.FindAsync(id);

            if (holding == null)
            {
                return NotFound();
            }

            return holding;
        }

        // PUT: api/Holdings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHolding(int id, Holding holding)
        {
            if (id != holding.HoldingId)
            {
                return BadRequest();
            }

            _context.Entry(holding).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoldingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Holdings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Holding>> PostHolding(Holding holding)
        {
            _context.Holding.Add(holding);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHolding", new { id = holding.HoldingId }, holding);
        }

        // DELETE: api/Holdings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHolding(int id)
        {
            var holding = await _context.Holding.FindAsync(id);
            if (holding == null)
            {
                return NotFound();
            }

            _context.Holding.Remove(holding);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoldingExists(int id)
        {
            return _context.Holding.Any(e => e.HoldingId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using online_knjizara.EF;
using online_knjizara.EntityModels;

namespace online_knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ZanrAngularController : ControllerBase
    {
        private readonly OnlineKnjizaraDbContext _context;

        public ZanrAngularController(OnlineKnjizaraDbContext context)
        {
            _context = context;
        }

        // GET: api/ZanrAngular
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zanr>>> GetZanr()
        {
            return await _context.Zanr.ToListAsync();
        }

        // GET: api/ZanrAngular/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zanr>> GetZanr(int id)
        {
            var zanr = await _context.Zanr.FindAsync(id);

            if (zanr == null)
            {
                return NotFound();
            }

            return zanr;
        }

        // PUT: api/ZanrAngular/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Route("UpdateZanrDetails")]
        public async Task<IActionResult> PutZanr(int id, Zanr zanr)
        {
            if (id != zanr.ID)
            {
                return BadRequest();
            }

            _context.Entry(zanr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZanrExists(id))
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

        // POST: api/ZanrAngular
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Zanr>> PostZanr(Zanr zanr)
        {
            _context.Zanr.Add(zanr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZanr", new { id = zanr.ID }, zanr);
        }

        // DELETE: api/ZanrAngular/5
        [HttpDelete("{id}")]
        [Route("DeleteZanrDetails")]
        public async Task<ActionResult<Zanr>> DeleteZanr(int id)
        {
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
            {
                return NotFound();
            }

            _context.Zanr.Remove(zanr);
            await _context.SaveChangesAsync();

            return zanr;
        }

        private bool ZanrExists(int id)
        {
            return _context.Zanr.Any(e => e.ID == id);
        }
    }
}

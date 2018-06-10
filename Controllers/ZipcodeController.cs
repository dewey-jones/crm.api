using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crmApi.Models;

namespace crmApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Zipcode")]
    public class ZipcodeController : Controller
    {
        private readonly CrmContext _context;

        public ZipcodeController(CrmContext context)
        {
            _context = context;
        }

        // GET: api/Zipcode
        [HttpGet]
        public IEnumerable<Zipcode> GetZipcodes()
        {
            return _context.Zipcodes;
        }

        // GET: api/Zipcode/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetZipcode([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zipcode = await _context.Zipcodes.SingleOrDefaultAsync(m => m.Id == id);

            if (zipcode == null)
            {
                return NotFound();
            }

            return Ok(zipcode);
        }

        // PUT: api/Zipcode/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZipcode([FromRoute] long id, [FromBody] Zipcode zipcode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zipcode.Id)
            {
                return BadRequest();
            }

            _context.Entry(zipcode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZipcodeExists(id))
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

        // POST: api/Zipcode
        [HttpPost]
        public async Task<IActionResult> PostZipcode([FromBody] Zipcode zipcode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Zipcodes.Add(zipcode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZipcode", new { id = zipcode.Id }, zipcode);
        }

        // DELETE: api/Zipcode/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZipcode([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zipcode = await _context.Zipcodes.SingleOrDefaultAsync(m => m.Id == id);
            if (zipcode == null)
            {
                return NotFound();
            }

            _context.Zipcodes.Remove(zipcode);
            await _context.SaveChangesAsync();

            return Ok(zipcode);
        }

        private bool ZipcodeExists(long id)
        {
            return _context.Zipcodes.Any(e => e.Id == id);
        }
    }
}
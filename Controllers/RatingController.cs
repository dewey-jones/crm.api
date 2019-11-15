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
    [Route("api/Rating")]
    public class RatingController : Controller
    {
        private readonly CrmContext _context;

        public RatingController(CrmContext context)
        {
            _context = context;
        }

        // GET: api/Rating
        [HttpGet]
        public IEnumerable<Rating> GetRatings(string sort="none")
        {
            IQueryable<Rating> q = _context.Ratings.AsQueryable();
            switch (sort.ToLower())
            {
                case "value":
                    return q.OrderBy(s => s.RatingValue);
            }
            return q.ToList();
        }

        // GET: api/Rating/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRating([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rating = await _context.Ratings.SingleOrDefaultAsync(m => m.Id == id);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Rating/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating([FromRoute] long id, [FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.Id)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Rating
        [HttpPost]
        public async Task<IActionResult> PostRating([FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
        }

        // DELETE: api/Rating/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rating = await _context.Ratings.SingleOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return Ok(rating);
        }

        private bool RatingExists(long id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }
    }
}
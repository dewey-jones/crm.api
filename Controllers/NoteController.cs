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
    [Route("api/Note")]
    public class NoteController : Controller
    {
        private readonly CrmContext _context;

        public NoteController(CrmContext context)
        {
            _context = context;
        }

        // GET: api/note
        [HttpGet]
        public IEnumerable<Note> GetAll([FromQuery] string sort = "none")
        {
            IQueryable<Note> q = _context.Notes.AsQueryable();
            switch (sort.ToLower())
            {
                case "notetext":
                    return q.OrderBy(s => s.NoteText);
                case "contactdate":
                    return q.OrderByDescending(s => s.ContactDate);
            }
            return q.ToList();
        }

        // GET: api/note/getByContact?contactId=4
        [HttpGet("getByContact")]
        public IEnumerable<Note> GetByContact([FromQuery] long contactId)
        {
            if (ModelState.IsValid)
            {
                return _context.Notes.Where(m => m.ContactId == contactId);
            }
            return new List<Note>();
        }

        // GET: api/Note/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.Notes.SingleOrDefaultAsync(m => m.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // PUT: api/Note/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote([FromRoute] long id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: api/Note
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // DELETE: api/Note/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.Notes.SingleOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok(note);
        }

        private bool NoteExists(long id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
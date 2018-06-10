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
    [Route("api/contact")]
    public class ContactController : Controller
    {
        private readonly CrmContext _context;

        public ContactController(CrmContext context)
        {
            _context = context;
        }

        // GET: api/contact
        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            return _context.Contacts.ToList();
        }

        // GET: api/contact
        [HttpGet]
        //public IEnumerable<Contact> GetContacts([FromQuery] long companyId)
        //{
        //    var contacts = _context.Contacts.ToList();
        //    //string companyId = HttpContext.Request.Query["companyId"];
        //    if (companyId > 0)
        //    {
        //        contacts = contacts.Where<Contact>(contact => contact.CompanyId == companyId).ToList();
        //    }
        //    return contacts;
        //}

        // GET: api/contact/getByCompany?companyId=4
        [HttpGet("getByCompany")]
        public IEnumerable<Contact> GetByCompany([FromQuery] long companyId)
        {
            if (ModelState.IsValid)
            {
                return _context.Contacts.Where(m => m.CompanyId == companyId);
            }
            return new List<Contact>();
        }

        //// GET api/values
        //[HttpGet]
        //public IActionResult Get([FromQuery]QueryParameters parameters)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return Ok(new[] { parameters.A.ToString(), parameters.B.ToString() });
        //    }
        //    return BadRequest();
        //}

        // GET: api/Contact/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.SingleOrDefaultAsync(m => m.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] long id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new ObjectResult(contact);
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.SingleOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        private bool ContactExists(long id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
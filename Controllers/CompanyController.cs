﻿using System;
using System.Collections.Generic;
using System.Linq;
using crmApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace crmApi.Controllers
{
    [Route("api/company")]
    public class CompanyController : Controller
    {
        private readonly CrmContext _context;

        public CompanyController(CrmContext context)
        {
            _context = context;

            if (_context.Companies.Count() == 0)
            {
                _context.Companies.Add(new Company { CompanyName = "Company1" });
                _context.SaveChanges();
            }
        }

        // GET: api/company
        // Test comment 2
        [HttpGet]
        public IEnumerable<Company> GetAll()
        {
            return _context.Companies.ToList();
        }

        // GET api/company/5
        [HttpGet("{id}", Name = "GetCompany")]
        public IActionResult GetById(long id)
        {
            var item = _context.Companies.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Company item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Companies.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetCompany", new { id = item.Id }, item);
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Company item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var company = _context.Companies.FirstOrDefault(t => t.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            company.CompanyName = item.CompanyName;
            company.Sales = item.Sales;
            company.Employees = item.Employees;
            company.Addr1 = item.Addr1;
            company.Addr2 = item.Addr2;
            company.City = item.City;
            company.State = item.State;
            company.Zip = item.Zip;
            company.Phone = item.Phone;
            company.Contact = item.Contact;
            company.Mail = item.Mail;
            company.MailDate = item.MailDate;
            company.CallOn = item.CallOn;
            company.CompanyNotes = item.CompanyNotes;
            company.Rating = item.Rating;
            company.Nearby = item.Nearby;
            company.Appropriate = item.Appropriate;
            company.Consultants = item.Consultants;

            _context.Companies.Update(company);
            _context.SaveChanges();

            // Must .subscribe to Observable in Angular, so must return an object that can be typed
            //return new NoContentResult();
            return new ObjectResult(company);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var company = _context.Companies.FirstOrDefault(t => t.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            _context.SaveChanges();
            return new ObjectResult(company);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using LibrarySystemAPI.BusinessLayer;
using LibrarySystemAPI.Models;

namespace LibrarySystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class bookController : Controller
    {
        IBookHandler bookHandler;
        public bookController(IBookHandler handler)
        {
            bookHandler = handler;
        }

        [HttpGet("{name}")]
        public ActionResult<book> GetWithName(string name)
        {
            var book = bookHandler.GetBook(name);
            if(book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public ActionResult Post(book bookk)
        {
            
            if (bookHandler.InsertBook(bookk))
            {
                return Created(nameof(Post), bookk);
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateBook(book book, int id)
        {
            if(bookHandler.UpdateBook(id, book))
            {
                return Created(nameof(GetWithName), book);
            }
            return BadRequest();
        }
        [HttpDelete("{name}")]
        public ActionResult DeleteBook(string name)
        {
            if (bookHandler.DeleteBook(name))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPut("return/{name}")]
        public ActionResult ReturnBook(string name)
        {
            if (bookHandler.ReturnBook(name))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}

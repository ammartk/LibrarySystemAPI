using LibrarySystemAPI.BusinessLayer;
using LibrarySystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace LibrarySystemAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        IUserHandler userHandler;
        IBookHandler bookHandler;
        public UserController(IUserHandler userHandler, IBookHandler bookHandler)
        {
            this.userHandler = userHandler;
            this.bookHandler = bookHandler;
        }
        [HttpGet("{name}")]
        public ActionResult<user> GetUser(int id)
        {
            return Ok(this.userHandler.GetUser(id));
        }
        [HttpPost]
        public ActionResult InsertUser(user user)
        {
            if (userHandler.InsertUser(user))
            {
                return Created(nameof(GetUser), user);
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public ActionResult PutUser(user user, int id)
        {
            if(userHandler.UpdateUser(id, user))
            {
                return Created(nameof(GetUser), user);
            }
            return BadRequest();
        }
        [HttpPost("{userid}/{bookname}")]
        public ActionResult IssueBook(int userid, string bookname)
        {
            var issue = bookHandler.IssueBook(userid, bookname);
            if (issue != null)
            {
                return Created(nameof(GetUser), issue);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (userHandler.DeleteUser(id))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpGet("fine/{id}")]
        public ActionResult<int> CalculateFine(int id)
        {
            int fine = userHandler.CalculateFine(id);
            return Ok(fine);
        }
    }
}

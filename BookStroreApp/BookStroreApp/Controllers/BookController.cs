using BusinessLayer.Interface;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStroreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookManagementBL;
        public BookController(IBookBL bookManagementBL)
        {
            this.bookManagementBL = bookManagementBL;
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult AddBook([FromForm] RequestBook Book)
        {
            if (Book == null)
            {
                return BadRequest("Book is null.");
            }
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    RespnseBook book = bookManagementBL.AddBook(Book);
                    if (book != null)
                    {
                        return Ok(new { success = true, Message = "book added", book });
                    }
                }
                return BadRequest(new { success = false, Message = "book adding Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet]

        public IActionResult GetBooks()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID =Convert.ToInt64( claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);

                    ICollection<RespnseBook> books = bookManagementBL.GetCustomerBooks();
                    if (books != null)
                    {
                        return Ok(new { success = true, Message = "books fetched", books });
                    }
                }
                return BadRequest(new { success = false, Message = "books fetch Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("sortbyprice/{sort}")]
        public IActionResult GetPriceSortBooks( bool sort)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID = Convert.ToInt64(claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);

                    ICollection<RespnseBook> books = bookManagementBL.GetCustomersortBooks(CustomerID,sort);
                    if (books != null)
                    {
                        return Ok(new { success = true, Message = "books fetched", books });
                    }
                }
                return BadRequest(new { success = false, Message = "books fetch Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [Authorize (Roles = Role.Admin)]
        [HttpDelete("{BookID}")]
        public IActionResult DeleteBook(long BookID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    bool result = bookManagementBL.DeleteBook(BookID);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "book deleted" });
                    }
                }
                return BadRequest(new { success = false, Message = "book delete Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize (Roles = Role.Admin)]
        [HttpPut("{BookID}")]
        public IActionResult UpdateBook(long BookID, RespnseBook Book)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    RespnseBook book = bookManagementBL.UpdateBook(BookID, Book);
                    if (book != null)
                    {
                        return Ok(new { success = true, Message = "book updated", book });
                    }
                }
                return BadRequest(new { success = false, Message = "book update Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}

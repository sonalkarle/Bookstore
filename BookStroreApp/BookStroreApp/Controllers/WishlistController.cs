using BusinessLayer.Interface;
using CommonLayer.ResponseModel;
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
    [Authorize(Roles = Role.User)]
    public class WishlistController : ControllerBase
    {

        readonly IWishlist customerWishListBL;

        public WishlistController(IWishlist customerWishListBL)
        {
            this.customerWishListBL = customerWishListBL;
        }
        [HttpGet]
        public IActionResult AddBookToWishList()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID = Convert.ToInt64(claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);
                    ICollection<Customerwishlist> WishList = customerWishListBL.GetWishList(CustomerID);
                    if (WishList != null)
                    {
                        return Ok(new { success = true, WishList });
                    }
                }
                return BadRequest(new { success = false, Message = "wish list is empty" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("{BookID}")]
        public IActionResult AddBookToWishList(long BookID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID =Convert.ToInt64(claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);
                    ICollection<Customerwishlist> WishList = customerWishListBL.AddBookToWishList(CustomerID, BookID);
                    if (WishList != null)
                    {
                        return Ok(new { success = true, Message = "book added to WishList", WishList });
                    }
                }
                return BadRequest(new { success = false, Message = "book add to WishList Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [HttpDelete("{BookID}")]
        public IActionResult RemoveBookFromWishList(long BookID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID =Convert.ToInt64(claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);
                    ICollection<Customerwishlist> WishList = customerWishListBL.RemoveBookFromWishList(CustomerID, BookID);
                    if (WishList != null)
                    {
                        return Ok(new { success = true, Message = "Book removed From WishList", WishList });
                    }
                }
                return BadRequest(new { success = false, Message = "Book remove from WishList Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}

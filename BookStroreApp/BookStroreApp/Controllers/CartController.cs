using BusinessLayer.Interface;
using CommonLayer.RequestModel;
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
   
    public class CartController : ControllerBase
    {
        ICartBL customerCartBL;

        public CartController(ICartBL customerCartBL)
        {
            this.customerCartBL = customerCartBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpGet]
        public IActionResult GetCart()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID =Convert.ToInt64(claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);
                    ICollection<CustomerCart> cart = customerCartBL.GetCart(CustomerID);
                    if (cart != null)
                    {
                        return Ok(new { success = true, cart });
                    }
                }
                return BadRequest(new { success = false, Message = "cart empty" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("Update/{BookID}")]
        public IActionResult AddBookToCart(long BookID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID =Convert.ToInt64(claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);
                    ICollection<CustomerCart> cart = customerCartBL.AddBookToCart(CustomerID, BookID);
                    if (cart != null)
                    {
                        return Ok(new { success = true, Message = "book added to cart", cart });
                    }
                }
                return BadRequest(new { success = false, Message = "book add to cart Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPut("{BookID}/{Quantity}")]
        public IActionResult UpdateBookInCart(long BookID, long Quantity)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null && Quantity >= 0)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID =Convert.ToInt64(claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);
                    ICollection<CustomerCart> cart = customerCartBL.UpdateBookInCart(CustomerID, BookID, Quantity);
                    if (cart != null)
                    {
                        return Ok(new { success = true, Message = "book updated in cart", cart });
                    }
                }
                return BadRequest(new { success = false, Message = "book update Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("{BookID}")]
        public IActionResult RemoveBookFromCart(long BookID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID = Convert.ToInt64(claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);
                    ICollection<CustomerCart> cart = customerCartBL.RemoveBookFromCart(CustomerID, BookID);
                    if (cart != null)
                    {
                        return Ok(new { success = true, Message = "book removed from cart", cart });
                    }
                }
                return BadRequest(new { success = false, Message = "book removed from cart Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

    }
}

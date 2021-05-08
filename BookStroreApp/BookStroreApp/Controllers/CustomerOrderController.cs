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
    [Authorize(Roles = Role.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        ICustomerOrderBL customerOrderBL;

        public CustomerOrderController(ICustomerOrderBL customerOrderBL)
        {
            this.customerOrderBL = customerOrderBL;
        }

        [HttpPost("{AddressID}")]
        public IActionResult PlaceOrder(long AddressID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long CustomerID = Convert.ToInt64(claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value);
                    CustomerOrder Order = customerOrderBL.PlaceOrder(CustomerID, AddressID);
                    if (Order != null)
                    {
                        return Ok(new { success = true, Message = "order placed successfully", Order });
                    }
                }
                return BadRequest(new { success = false, Message = "order place Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}

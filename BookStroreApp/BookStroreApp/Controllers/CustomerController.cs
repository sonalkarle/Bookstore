using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStroreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CustomerController : ControllerBase
    {
        ICustomerBL bookBL;
        private IConfiguration Configuration { get; }

        public CustomerController(ICustomerBL bookBL)                           //Constructor n passing an object to controller
        {                                                                           //to get an access of IEmployeeBL
            this.bookBL = bookBL;
        }


        /// <summary>
        /// Post api
        /// </summary>
        /// <param name="employeeDetailModel"></param>
        /// <returns></returns>
        [HttpPost("Register")]                                                                    //Creating a Post Api
        public IActionResult RegisterDetails(RegisterCustomer registration)                                        //Here return type represents the result of an action method
        {
            try
            {
                if (ModelState.IsValid)
                {

                    CustomerUser result = this.bookBL.RegisterDetails(registration);                   //getting the data from BusinessLayer
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, Message = "Register details is added Successfully" });   //(smd format)    //this.Ok returns the data in json format
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, Message = "Register details is added  Unsuccessfully" });
                    }
                }

                else
                {
                    throw new Exception("Model is not valid");
                }

            }


            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }


        [HttpPost("Login")]
        public IActionResult LoginUser(LoginCustomer Customer)
        {
            if (Customer == null)
            {
                return BadRequest("Customer is null.");
            }
            try
            {
                CustomerUser result = bookBL.LoginCustomer(Customer);
                if (result != null)
                {
                    return Ok(new { success = true, Message = "Customer login Successful", Customer = result });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "Customer login Unsuccessful" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        /// <summary>
        /// Forget password of Registered ID
        /// </summary>
        /// <param name="forgetPasswordModel"></param>
        /// <returns></returns>
        [HttpPost("ForgetPassword")]
        public ActionResult ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MSMQModel result = bookBL.ForgetPassword(forgetPasswordModel);                   //getting the data from BusinessLayer
                    var msmq = new MSMQ(Configuration);
                    msmq.MSMQSender(result);
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, Message = "Your password has been forget sucessfully now you can reset your password" });   //(smd format)    //this.Ok returns the data in json format
                    }

                    else
                    {
                        return this.Ok(new { Success = true, Message = "Other User is trying to login from your account" });   //(smd format)    //this.Ok returns the data in json format
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;
                    resetPasswordModel.Email = Email;
                    bool result = bookBL.ResetCustomerAccountPassword(resetPasswordModel);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "password changed successfully" });
                    }
                }
                return BadRequest(new { success = false, Message = "password change unsuccessfull" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }


    }
}

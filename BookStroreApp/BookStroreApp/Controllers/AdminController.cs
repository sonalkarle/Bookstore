using BusinessLayer.IbookBL;
using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStroreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase

    {
        IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)                           //Constructor n passing an object to controller
        {                                                                           //to get an access of IEmployeeBL
            this.adminBL = adminBL;
        }


        /// <summary>
        /// Post api
        /// </summary>
        /// <param name="employeeDetailModel"></param>
        /// <returns></returns>
        [HttpPost("Register")]                                                                    //Creating a Post Api
        public IActionResult RegisterAdminDetails(RegisterAdmin registration)                                        //Here return type represents the result of an action method
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Admin result = this.adminBL.RegisterAdminDetails(registration);                   //getting the data from BusinessLayer
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, Message = "Admin details is added Successfully" });   //(smd format)    //this.Ok returns the data in json format
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, Message = "Admin details is added  Unsuccessfully" });
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
        public IActionResult Login(LoginAdmin login)                                        //Here return type represents the result of an action method
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string result = this.adminBL.AdminLogin(login);                   //getting the data from BusinessLayer
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, Message = "Login Successfully", data = result });   //(smd format)    //this.Ok returns the data in json format
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, Message = "Login  Unsuccessfully" });
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

    }
}

using FundooManagerLayer.Interface;
using FundooModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Register the User into the Table/Documents
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")] //6/8
        public async Task<IActionResult> Register([FromBody] RegisterModel userData) ////frombody attribute says value read from body of the request
        {
            try
            {
                var result = await this.manager.Register(userData);
                //this.logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
                if (result!=null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Register Successfull",Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<RegisterModel>() { Status = false, Message = "Register UnSuccessfull", Data = null });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")] //6/8
        public async Task<IActionResult> Login([FromBody] LoginModel userData) ////frombody attribute says value read from body of the request
        {
            try
            {
                var resultLogin =  this.manager.Login(userData);
                if (resultLogin != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Login Successfull", Data = resultLogin });
                    // 
                }
                else
                {
                    return this.BadRequest(new ResponseModel<RegisterModel>() { Status = false, Message = "Login UnSuccessfull", Data = null });
                    //
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// For reset the passwod in the User Register
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("reset")] //6/8
        public async Task<IActionResult> ResetPassword([FromBody] ResetModel userData) ////frombody attribute says value read from body of the request
        {
            try
            {
                var result =  this.manager.ResetPassword(userData);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Reset Successfull", Data = result });
                    //
                }
                else
                {
                    return this.BadRequest(new ResponseModel<ResetModel>() { Status = false, Message = "Reset UnSuccessfull", Data = null });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// If the password is forgotten this is called
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("forget")] //6/8
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var forgetResult = this.manager.ForgetPassword(email);
                if (forgetResult != null)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Forget Password" });
                    //
                }
                else
                {
                    return this.BadRequest(new ResponseModel<ForgetModel>() { Status = false, Message = "Forget UnSuccessfull", Data = null });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}

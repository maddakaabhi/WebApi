using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserBusiness business;
        private readonly IBus bus;
        public UserController(IUserBusiness business, IBus bus,ILogger<UserController> logger)
        {
            this.business = business;
            this.bus = bus;
            this.logger = logger;
        }


        //request url: localhost//456/api/User/Reg
        [HttpPost]
        [Route("reg")]

        public ActionResult Register(RegisterModel model)
        {
            var Checkemail = business.checkemail(model.Email);
            if (Checkemail)
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Register Failed" });
            }
            else
            {
                var checkregister = business.Register(model);
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Register successfull", Data = checkregister });
            }
        }

        [HttpPost]
        [Route("loginmethod")]
        public ActionResult login(LoginModel loginModel) 
        {
            logger.LogInformation("Login started");
            var checklogin=business.login(loginModel);
            if(checklogin != null)
            {
                logger.LogTrace("login success");
                return Ok(new ResponseModel<string> { Success = true, Message = "User found", Data = checklogin });

            }
            else
            {
                logger.LogTrace("login failed");
                return Ok(new ResponseModel<string> { Success=false,Message="User not found",Data=checklogin});
            }
        }

        [HttpGet]
        [Route("getallusers")]
        
        public List<UserEntity> GetUsers()
        {
            var Allusers=business.GetAllUsers();
            if (Allusers != null)
            {
                return Allusers;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("checkemailid")]

        public bool checkemail(string email)
        {
            var findemail=business.checkemail(email);
            if (findemail)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [Route("forgot-password")]

        public async Task<IActionResult> UserForgotPassword(string email)
        {
            try
            {
                if (business.checkemail(email))
                {
                    Send send = new Send();
                    ForgotPasswordModel forgotPasswordModel = business.UserForgotPassword(email);
                    send.SendingMail(forgotPasswordModel.Email, forgotPasswordModel.Token);

                    Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
                    var endPoint = await bus.GetSendEndpoint(uri);

                    await endPoint.Send(forgotPasswordModel);
                    return Ok(new ResponseModel<string> { Success = true, Message = "email send successfull", Data = forgotPasswordModel.Token });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "email  not send successfull", Data = email });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]

        public ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            string email = User.FindFirst("Email").Value;
            var checkUpdate = business.ResetPassword(resetPasswordModel, email);
            if (checkUpdate)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Password Updated Successfully", Data = checkUpdate });
            }
            else
            {
                return Ok(new ResponseModel<bool> { Success = false, Message = "Password not Updated", Data = checkUpdate });
            }
        }

        [HttpDelete]
        [Route("deleteuser")]

        public ActionResult Delete(string email)
        {

            if (business.DeleteUser(email))
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "User Deleted Successfully",Data=true });


            }
            else
            {
                return Ok(new ResponseModel<bool> { Success = false, Message = "User does not exist" });
            }

        }

        [HttpPost]
        [Route("addproduct")]
        public ActionResult ProductAdd(ProductAddModel productaddmodel)
        {
            var result= business.ProductAdd(productaddmodel);
            if(result != null)
            {
                return Ok(new ResponseModel<ProductEntity> { Success=true,Message="Product details add successfully",Data=result});
            }
            else
            {
                return Ok(new ResponseModel<ProductEntity> { Success = false, Message = "Product details not added" });
            }
        }




    }
}

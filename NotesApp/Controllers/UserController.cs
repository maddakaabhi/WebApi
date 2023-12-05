using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using GreenPipes.Caching;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
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
        private readonly IDistributedCache _cache;
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
        
        //public List<UserEntity> GetUsers()
        //{
        //    var Allusers=business.GetAllUsers();
        //    if (Allusers != null)
        //    {
        //        return Allusers;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<UserEntity>> GetAll(string UserId,bool enableCache)
        {
            if (!enableCache)
            {
                return business.GetAllUsers();
            }
            string cacheKey = UserId;

            // Trying to get data from the Redis cache
            byte[] cachedData = await _cache.GetAsync(cacheKey);
            List<UserEntity> users = new List<UserEntity>();
            if (cachedData != null)
            {
                // If the data is found in the cache, encode and deserialize cached data.
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                users = JsonSerializer.Deserialize<List<UserEntity>>(cachedDataString);
            }
            else
            {
                // If the data is not found in the cache, then fetch data from database
                users = business.GetAllUsers();

                // Serializing the data
                string cachedDataString = JsonSerializer.Serialize(users);
                var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Add the data into the cache
                await _cache.SetAsync(cacheKey, dataToCache, options);
            }
            return users;
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
        [Route("loginsession")]
        public ActionResult LoginSession(LoginModel loginModel)
        {
            var result=business.LoginSession(loginModel);
            if(result != null)
            {
                HttpContext.Session.SetInt32("UserId",result.UserId);
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Login Successfully", Data = result });
            }
            else
            {
                return Ok(new ResponseModel<UserEntity> { Success = false, Message = "Login Failed" });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("updatefirstname")]

        public ActionResult Updatename(string firstname)
        {
            string email = User.FindFirst("Email").Value;
            var result=business.Updatefirstname(firstname,email);

            if (result !=null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "updated firstname",Data=result });
            }
            else
            {
                ;
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Data not found"});
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

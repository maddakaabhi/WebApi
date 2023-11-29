using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Interfaces;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly IConfiguration _config;

        private readonly NotesDBContext notesdbcontext;
        public UserRepo(NotesDBContext notesdbcontext, IConfiguration _config)
        {
            this.notesdbcontext = notesdbcontext;
            this._config = _config;
        }
        public UserEntity Register(RegisterModel register)
        {
            var Checkemail = notesdbcontext.Users.ToList().Find(x => x.Email == register.Email);
            UserEntity user = new UserEntity();
            if (Checkemail ==null)
            {
               
                user.FirstName = register.FirstName;
                user.LastName = register.LastName;
                user.Email = register.Email;
                user.Password = EncodePassword(register.Password);
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                notesdbcontext.Users.Add(user);
                notesdbcontext.SaveChanges();
                return user;
            }
            else
            {
                return user;
            }
        }
        public static string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public string login(LoginModel loginModel)
        {
            UserEntity userEntity=notesdbcontext.Users.ToList().Find(x=>x.Email==loginModel.LoginEmail && x.Password==EncodePassword(loginModel.LoginPassword));
            if (userEntity != null) 
            {
                var token=GenerateToken(userEntity.Email,userEntity.UserId);
                return token;
            }
            else
            {
                return null;
            }
        }

        public string GenerateToken(string Email, int Userid)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("Userid",Userid.ToString())
            };
            var token = new JwtSecurityToken(_config["jwt:Issuer"],
                _config["jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        public List<UserEntity> GetAllUsers()
        {
            List<UserEntity> userEntities =new List<UserEntity>();
            foreach(UserEntity userEntity in notesdbcontext.Users)
            {
                userEntities.Add(userEntity);
            }
            return userEntities;


        }
        public bool checkemail(string email) 
        {
            UserEntity entity = notesdbcontext.Users.ToList().Find(x=>x.Email==email);
            if (entity != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ForgotPasswordModel UserForgotPassword(string email)
        {
            try
            {
                
                    var result = notesdbcontext.Users.ToList().Find(x => x.Email == email);
                    ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();  
                    forgotPasswordModel.Email = result.Email;
                    forgotPasswordModel.Token = GenerateToken(result.Email, result.UserId);
                    forgotPasswordModel.UserId = result.UserId;
                    return forgotPasswordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(ResetPasswordModel resetPasswordModel, string email) 
        {
            var result = notesdbcontext.Users.ToList().Find(x=>x.Email==email);
            if (checkemail(email))
            {
                result.Password = EncodePassword(resetPasswordModel.Password);
                result.UpdatedAt = DateTime.Now;
                notesdbcontext.SaveChanges();
                return true;
            }
            return false;
           
            
        }

        public bool DeleteUser(string email)
        {
            var user = notesdbcontext.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                notesdbcontext.Users.Remove(user);
                notesdbcontext.SaveChanges();
                return true;
            }
            return false;
        }

        public UserEntity LoginSession(LoginModel loginModel)
        {
            UserEntity userEntity = notesdbcontext.Users.FirstOrDefault(x=>x.Email==loginModel.LoginEmail && x.Password==EncodePassword(loginModel.LoginPassword));
            
            if(userEntity != null)
            {
                return userEntity;
            }
            else { return null; }
        }

        public ProductEntity ProductAdd(ProductAddModel productAddModel)
        {
            
            ProductEntity entity = new ProductEntity();
            entity.Comment = productAddModel.ProductComment;
            entity.Rating=productAddModel.ProductRating;
            notesdbcontext.Add(entity);
            notesdbcontext.SaveChanges();
            return entity;

        }
        

    }
}

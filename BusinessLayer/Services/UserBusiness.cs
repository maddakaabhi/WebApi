using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo repo;
        public UserBusiness(IUserRepo repo)
        {
            this.repo = repo;
        }
        public UserEntity Register(RegisterModel register)
        {
            return repo.Register(register);
        }
        public string login(LoginModel loginModel)
        {
            return repo.login(loginModel);
        }
        public List<UserEntity> GetAllUsers()
        {
            return repo.GetAllUsers();
        }
        public bool checkemail(string email)
        {
            return repo.checkemail(email);
        }

        public ForgotPasswordModel UserForgotPassword(string email)
        {
            return repo.UserForgotPassword(email);
        }

        public bool ResetPassword(ResetPasswordModel resetPasswordModel,string email)
        {
            return repo.ResetPassword(resetPasswordModel,email);
        }

        public bool DeleteUser(string email) 
        { 
            return repo.DeleteUser(email);
        }

        public ProductEntity ProductAdd(ProductAddModel productaddmodel)
        {
            return repo.ProductAdd(productaddmodel);
        }
        
    }
}

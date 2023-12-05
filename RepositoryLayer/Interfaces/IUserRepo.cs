using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        UserEntity Register(RegisterModel register);
        string login(LoginModel loginModel);
        List<UserEntity> GetAllUsers();

        bool checkemail(string email);

        ForgotPasswordModel UserForgotPassword(string email);

        bool ResetPassword(ResetPasswordModel resetPasswordModel,string email);

        bool DeleteUser(string email);

        UserEntity LoginSession(LoginModel loginModel);

        ProductEntity ProductAdd(ProductAddModel productmodel);

        UserEntity Updatefirstname(string firstname, string email);
    }

}
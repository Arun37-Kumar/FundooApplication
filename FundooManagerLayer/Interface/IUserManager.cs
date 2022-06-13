using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManagerLayer.Interface
{
    public interface IUserManager
    {

        /// <summary>
        /// Register Model 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<RegisterModel> Register(RegisterModel user);

        /// <summary>
        /// Login Model
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        RegisterModel Login(LoginModel loginData);

        /// <summary>
        /// Reset Password Model
        /// </summary>
        /// <param name="reset"></param>
        /// <returns></returns>
        RegisterModel ResetPassword(ResetModel reset);

        Task<bool> ForgetPassword(string email);

    }
}

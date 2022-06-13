using FundooManagerLayer.Interface;
using FundooModelLayer;
using FundooRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManagerLayer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Register with encoded password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterModel> Register(RegisterModel user)
        {
            user.Password = EncodePasswordToString64(user.Password);
            try
            {
                return await this.repository.Register(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Login User Manage 8/6
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public  RegisterModel Login(LoginModel login)
        {
            //login.Password = EncodePasswordToString64(login.Password);
            try
            {
                return  this.repository.Login(login);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Reset the Password
        /// </summary>
        /// <param name="reset"></param>
        /// <returns></returns>
        public  RegisterModel ResetPassword(ResetModel reset)
        {
            try
            {
                return  this.repository.ResetPassword(reset);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> ForgetPassword(string email)
        {
            try
            {
                return await this.repository.ForgetPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Encoding(Encryption) the Password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncodePasswordToString64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodeData = Convert.ToBase64String(encData_byte);
                return encodeData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode : " + ex.Message);
            }
        }
    }
}

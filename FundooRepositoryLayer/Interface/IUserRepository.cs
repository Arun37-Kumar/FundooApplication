using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Interface
{
    public interface IUserRepository
    {
        Task<RegisterModel> Register(RegisterModel model);
        RegisterModel Login(LoginModel loginData);
        Task<bool> ForgetPassword(string forget);
        RegisterModel ResetPassword(ResetModel password);


    }
}

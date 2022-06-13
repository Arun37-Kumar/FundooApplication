using Experimental.System.Messaging;
using FundooModelLayer;
using FundooRepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<RegisterModel> User;

        private readonly IConfiguration configuration;

        /// <summary>
        /// For Configuration setting and connection for MongoDb
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="configuration"></param>
        public UserRepository(IFundooDatabaseSettings settings, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            User = database.GetCollection<RegisterModel>("User");
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RegisterModel> Register(RegisterModel user)
        {
            try
            {
                await this.User.InsertOneAsync(user);
                return user;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// For Login Check 8/6
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public RegisterModel Login(LoginModel login)
        {
            try
            {
                var checkEmail =  this.User.AsQueryable().Where(x => x.Email == login.Email).FirstOrDefault();
                if (checkEmail != null)
                {
                    return checkEmail;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Reset the passsword 6/9
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> ForgetPassword(string email)
        {
            try
            {
                var checkEmail = await this.User.AsQueryable().Where(x => x.Email == email).FirstOrDefaultAsync();
                if (checkEmail != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(this.configuration["Credentials:email"]);
                    mail.To.Add(email);
                    mail.Subject = "Reset Password for FundooNotes";
                    this.SendMSMQ();
                    mail.Body = this.ReceiveMSMQ();

                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Reset Password 6/9
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public  RegisterModel ResetPassword(ResetModel reset)
        {
            try
            {
                var checkEmail =  this.User.AsQueryable().Where(x => x.Email == reset.Email).FirstOrDefault();
                if (checkEmail != null)
                {
                     User.UpdateOne(x => x.Email == reset.Email,
                        Builders<RegisterModel>.Update.Set(x => x.Password, reset.Password));
                    return checkEmail;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Send MSMQ
        /// </summary>
        public void SendMSMQ()
        {
            MessageQueue msgQueue;
            if (MessageQueue.Exists(@".\Private$\FundooNote"))
            {
                msgQueue = new MessageQueue(@".\Private$\FundooNote");
            }
            else
            {
                msgQueue = MessageQueue.Create(@".\Private$\FundooNote");
            }
            msgQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = "This is Reset Password link. Reset Link =>";
            msgQueue.Label = "Mail Body";
            msgQueue.Send(body);
        }

        /// <summary>
        /// Receive MSMQ 6/9
        /// </summary>
        /// <returns></returns>
        public string ReceiveMSMQ()
        {
            MessageQueue msgQueue = new MessageQueue(@".\Private$\Fundoo");
            var recievedMessage = msgQueue.Receive();
            recievedMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return recievedMessage.Body.ToString();
        }

        /// <summary>
        /// Generate token 6/9
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                { new Claim(ClaimTypes.Email, email) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

    }
}

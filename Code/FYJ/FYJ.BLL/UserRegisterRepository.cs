using FYJ.Constant;
using FYJ.IBLLStrategy;
using FYJ.Model;
using FYJ.Utility;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Data.Entity;

namespace FYJ.BLL
{
    public class UserRegisterRepository : IRegister
    {
        private ApplicationContext db = new ApplicationContext();

        /// <summary>
        /// User regist
        /// </summary>
        /// <param name="model"></param>
        public void Register(RegisterModel model)
        {
            //create user
            User dbUser = new User();
            dbUser.UserName = model.Email;
            dbUser.Password = Encryption.CreateSHA256HashString(model.Passoword + Encryption.GetRandomSalt(Security.SALT_BYTE_NUMBER));
            dbUser.Email = model.Email;
            dbUser.EmailConfirm = false;
            dbUser.IsLock = false;
            dbUser.IsDelete = false;
            dbUser.LockDate = null;
            dbUser.RoleId = 3;//User
            dbUser.CrDate = DateTime.Now;
            dbUser.CrUserId = 0;

            db.Entry(dbUser).State = EntityState.Added;
            db.SaveChanges();

            //update maile confirm code
            if (dbUser != null)
            {
                dbUser.EmailCode = dbUser.UserId.ToString();
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Get user model from user json
        /// </summary>
        /// <param name="registerJson"></param>
        /// <returns></returns>
        public RegisterModel GetJsonModel(string registerJson)
        {
            //TODO : CHECK JSON FORMAT
            return JsonConvert.DeserializeObject<RegisterModel>(registerJson);
        }
    }
}

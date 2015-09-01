using FYJ.IBLLStrategy;
using FYJ.Model;
using Newtonsoft.Json;
using System;

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
            User dbUser = new User();
            dbUser.UserName = model.Email;
            dbUser.Password = model.Passoword;
            dbUser.Email = model.Email;
            dbUser.EmailCode = "";
            dbUser.EmailConfirm = false;
            dbUser.IsLock = false;
            dbUser.IsDelete = false;
            dbUser.LockDate = null;
            dbUser.RoleId = 1;//User
            dbUser.CrDate = DateTime.Now;
            dbUser.CrUserId = 0;

            db.User.Add(dbUser);
            db.SaveChanges();
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

using FYJ.Constant;
using FYJ.IBLLStrategy;
using FYJ.Model;
using FYJ.Utility;
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
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    //create user
                    User dbUser = new User();
                    dbUser.UserName = model.Email;
                    dbUser.Salt = Encryption.GetRandomSalt(Security.SALT_BYTE_NUMBER);
                    dbUser.Password = Encryption.CreateSHA256HashString(model.Password + dbUser.Salt);
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
                        dbUser.EmailCode = StringTool.GenerateMailCode() + dbUser.UserId.ToString();
                        db.SaveChanges();
                    }
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Log.Error("Register user failed." + ex.ToString());
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}

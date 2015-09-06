using FYJ.Model;
using FYJ.Utility;
using Newtonsoft.Json;

namespace FYJ.BLL
{
    public class UserRegisterValidation
    {
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

        /// <summary>
        /// Check user input from front-end
        /// </summary>
        /// <returns></returns>
        public bool CheckUserInput(string mail, string generateCode, string verifyCode)
        {
            bool result = false;
            if (RegExp.IsEmail(mail) && string.Equals(generateCode, verifyCode))
                result = true;
            return result;
        }
    }
}

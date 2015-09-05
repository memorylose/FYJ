using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.Utility
{
    public class StringTool
    {
        /// <summary>
        /// Get mail confirming code
        /// </summary>
        /// <returns></returns>
        public static string GenerateMailCode()
        {
            Random random = new Random();
            int firstTime = random.Next(100000, 999999);
            string secondTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            return firstTime.ToString() + secondTime;
        }
    }
}

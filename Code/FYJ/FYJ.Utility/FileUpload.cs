using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.Utility
{
    public class FileOperator
    {
        /// <summary>
        /// Check file extension
        /// </summary>
        public static bool CheckImageExtension(System.IO.Stream fs)
        {
            bool ret = false;
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch (Exception ex)
            {
                Log.Error("Check image extension failed." + ex.ToString());
                return false;
            }
            //r.Close();
            //fs.Close();
            /*Description
              *4946/104116 txt
              *7173        gif 
              *255216      jpg
              *13780       png
              *6677        bmp
              *239187      txt,aspx,asp,sql
              *208207      xls.doc.ppt
              *6063        xml
              *6033        htm,html
              *4742        js
              *8075        xlsx,zip,pptx,mmap,zip
              *8297        rar   
              *01          accdb,mdb
              *7790        exe,dll           
              *5666        psd 
              *255254      rdp 
              *10056       bt种子 
              *64101       bat 
              *4059        sgf
              */

            String[] fileType = { "255216", "7173", "6677", "13780" };
            String[] fileExtName = { "jpg", "gif", "bmp", "png" };
            String fExt = "";
            for (int i = 0; i < fileType.Length; i++)
            {
                if (fileclass == fileType[i])
                {
                    fExt = fileExtName[i];
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        public static void CheckImageFileFolder(string folderPath, ref string rtnPath, ref string datePath)
        {
            rtnPath = string.Empty;
            datePath = string.Empty;

            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folderPath + "/" + DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd"))))
            {
                try
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folderPath + "/" + DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd")));
                }
                catch (Exception ex)
                {
                    Log.Error("Create year/month folder failed." + ex.ToString());
                }
            }

            //full path
            rtnPath = System.Web.HttpContext.Current.Server.MapPath(folderPath + "/" + DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/");

            //date folderd
            datePath = DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/";
        }

        public static void CheckLogFileFolder(string folderPath, ref string rtnPath)
        {
            rtnPath = string.Empty;

            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folderPath + "/" + DateTime.Now.ToString("yyyyMM"))))
            {
                try
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folderPath + "/" + DateTime.Now.ToString("yyyyMM")));
                }
                catch (Exception ex)
                {
                    Log.Error("Create year/month folder failed." + ex.ToString());
                }
            }

            rtnPath = System.Web.HttpContext.Current.Server.MapPath(folderPath + "/" + DateTime.Now.ToString("yyyyMM") + "/");
        }

        /// <summary>
        /// Generate image file name
        /// </summary>
        /// <returns></returns>
        public static string GenerateImageFileName()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int randomName = random.Next(10000, 99999);
            string dateName = DateTime.Now.ToString("yyyyMMddHHmmss");
            return randomName.ToString() + dateName;
        }
    }
}

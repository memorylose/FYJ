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
            catch
            {
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

        public static void CheckFileFolder(string folderPath, ref string imagePath)
        {
            imagePath = string.Empty;
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folderPath)))
            {
                try
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folderPath));
                }
                catch (Exception ex)
                {
                    //TODO log
                }
            }

            //Get current date folder
            string currentFolder = DateTime.Now.ToString("yyyyMM");
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folderPath + "/" + currentFolder)))
            {
                try
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folderPath + "/" + currentFolder));
                }
                catch (Exception ex)
                {
                    //TODO log
                }
            }
            imagePath = System.Web.HttpContext.Current.Server.MapPath(folderPath + "/" + currentFolder + "/");
        }
    }
}

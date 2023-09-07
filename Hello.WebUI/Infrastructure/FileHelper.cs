using Hello.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Hello.WebUI.Infrastructure
{
    public class FileHelper
    {

        private static int FILE_EXT_LENGHT = 3;

        public static string SaveImageFile(MultipartFileData fileData, string imageName, string root)
        {
            string newFile = string.Empty;
            string fileName = fileData.Headers.ContentDisposition.FileName;

            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                fileName = fileName.Trim('"');

            if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                fileName = Path.GetFileName(fileName);

            string fileExt = fileName.Substring(fileName.LastIndexOf(".") + 1, FILE_EXT_LENGHT);
            newFile = imageName + "." + fileExt;

            string filePath = Path.Combine(root, newFile);

            if (File.Exists(filePath))
                File.Delete(filePath);

            File.Move(fileData.LocalFileName, filePath);

            if (File.Exists(fileData.LocalFileName))
                File.Delete(fileData.LocalFileName);

            return newFile;
        }

        public static string ProductImageName(long id, int number)
        {
            return "hellorent_" + Encrypt.MD5Encrypt(id.ToString()) + "_" + Encrypt.MD5Encrypt(number.ToString());
        }
    }
}
using _3rdBackendProject.Models;
using _3rdBackendProject.ViewModels;
using System.Diagnostics;
using System.Web.WebPages.Html;

namespace _3rdBackendProject.Utilities.Extentions
{
    public static class CheckFileTypeExtention
    {
        public static bool CheckFileType(this IFormFile file, string fileType)
        {
            if (file.ContentType.Contains(fileType))
            {
                return true;
            }
            return false;
        }
        public static bool CheckFileSize(this IFormFile file, int fileSize)
        {
            if (file.Length <= fileSize * 1024) { return true; }

            return false;
        }

        public async static Task<string> CreateFile(this IFormFile file, string root, string folder)
        {

            string filename = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(root, folder, filename);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            fileStream.Close();
            return filename;
        }

        public static void DeleteItem(this string file, string root, string folder)
        {


            string path = Path.Combine(root, folder, file);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool CheckUserName(this string value)
        {

            for (int i = 0; i <= value.Length; i++)
            {
                if (!char.IsDigit(value[i]))
                {
                    //modelState.AddError("","Name or Surname Cant Contain Digit");
                    return true;
                }
                
            }
            return false;
        }
    
        public static string Capitalize(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            string firstLetter = value[0].ToString().ToUpper();
            string otherletter = value.Substring(1).ToLower();

            return firstLetter + otherletter;
        }
    }

}

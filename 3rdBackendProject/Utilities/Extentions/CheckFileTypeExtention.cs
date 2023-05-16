using _3rdBackendProject.Models;
using System.Diagnostics;

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

        public async static Task<string> CreateFile(this IFormFile file,string root,string folder)
        {

            string filename = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(root,folder, filename);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            fileStream.Close();
            return filename;
        }

        public static void DeleteItem(this string file,string root,string folder) {


            string path = Path.Combine(root, folder, file);

            if (File.Exists(path))
            {
               File.Delete(path);
            }
        }

        
    }
}

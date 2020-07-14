using System.Text;
using System.Linq;
using System.IO;
using System;

namespace ImageToANSI
{
    public class IOManager
    {
        private const string INPUT_PATH = "./img/in";
        private const string OUTPUT_PATH = "./img/out";
        private static readonly string[] VALID_EXTENSIONS = { ".jpg", ".jpeg", ".png" };
        public static string[] GetImagesToConvert()
        {
            return Directory.GetFiles(INPUT_PATH).Where(
                path => VALID_EXTENSIONS.Contains(Path.GetExtension(path).ToLower())
                ).ToArray();
        }

        public static void WriteFile(string fileName, StringBuilder sb)
        {
            using (StreamWriter outputFile = new StreamWriter($"{OUTPUT_PATH}/{DateTime.Now.Millisecond}_{fileName}.txt", true))
            {
                outputFile.WriteLine(sb.ToString());
            }
        }

        public static void CleanInputFolder()
        {
            var filesToDelete = Directory.GetFiles(INPUT_PATH).Where(
                path => VALID_EXTENSIONS.Contains(Path.GetExtension(path).ToLower())
                ).ToArray();
            filesToDelete.ToList().ForEach(file => File.Delete(file));
        }
    }
}
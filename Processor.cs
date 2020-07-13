using System.Runtime.Serialization;
using System.Text;
using System.IO;
using SkiaSharp;
using System;

namespace ImageToANSI
{
    public class Processor
    {
        public static void ProcessImages()
        {
            var images = IOManager.GetImagesToConvert();
            foreach (string imgPath in images)
            {
                var image = ImageProcessor.GetImage(imgPath);
                var sb = TransformToANSI(image);
                IOManager.WriteFile(image.FileName, sb);
            }
            //IOManager.CleanInputFolder();
        }

        private static StringBuilder TransformToANSI(ProcessedImage image)
        {
            StringBuilder sb = new StringBuilder();
            int col = 0, row = 0;
            for (int i = 0; i <= image.Pixels.Length; i++)
            {
                if (col >= image.Width)
                {
                    //reset sb
                    sb.Append("\n");
                    //reset col
                    col = 0;
                    row++;
                }
                if (row >= image.Height)
                    break;
                
                sb.Append(GetCharToWrite(image.Pixels[i]));
                col++;
            }
            return sb;
        }

        private static string GetCharToWrite(int pixelValue)
        {
            return pixelValue switch
            {
                var n when n >= 0 && n < 32 => "#",
                var n when n >= 32 && n < 64 => "%",
                var n when n >= 64 && n < 96 => "/",
                var n when n >= 96 && n < 128 => "-",
                var n when n >= 128 && n < 192 => ".",
                var n when n >= 192 && n < 256 => " ",
                _ => string.Empty
            };
        }
    }
}
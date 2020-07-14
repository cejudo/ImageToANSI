using System.Text;

namespace ImageToANSI
{
    public class ANSIConverter
    {
        public static StringBuilder Transform(ProcessedImage image)
        {
            StringBuilder sb = new StringBuilder();
            int col = 0, row = 0;
            for (int i = 0; i <= image.Pixels.Length; i++)
            {
                if (col >= image.Width)
                {
                    sb.Append("\n");
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
                var n when n >= 0 && n < 16 => "#",
                var n when n >= 16 && n < 32 => "%",
                var n when n >= 32 && n < 48 => "$",
                var n when n >= 48 && n < 64 => "*",
                var n when n >= 64 && n < 128 => "+",
                var n when n >= 128 && n < 192 => ".",
                var n when n >= 192 && n < 256 => " ",
                _ => string.Empty
            };
        }
    }
}
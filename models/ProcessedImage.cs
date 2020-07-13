namespace ImageToANSI
{
    public class ProcessedImage
    {
        public readonly int[] Pixels;
        public readonly int Width;
        public readonly int Height;

        public readonly string FileName;

        public ProcessedImage(int[] pixels, int width, int height, string fileName)
        {
            Pixels = pixels;
            Width = width;
            Height = height;
            FileName = fileName;
        }
    }
}
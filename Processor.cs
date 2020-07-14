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
                var sb = ANSIConverter.Transform(image);
                IOManager.WriteFile(image.FileName, sb);
            }
            //IOManager.CleanInputFolder();
        }
    }
}
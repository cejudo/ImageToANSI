using System;
using System.IO;
using SkiaSharp;

namespace ImageToANSI
{
    public class ImageProcessor
    {
        const int SIZE = 80;
        const int QUALITY = 80;
        public static ProcessedImage GetImage(string imagePath)
        {
            using (var input = File.OpenRead(imagePath))
            {
                using (var image = SKBitmap.Decode(input))
                {
                    // RESIZE IMAGE
                    int width, height;
                    if (image.Width > image.Height)
                    {
                        width = SIZE;
                        height = image.Height * SIZE / image.Width;
                    }
                    else
                    {
                        width = image.Width * SIZE / image.Height;
                        height = SIZE;
                    }
                    var resizedImage = image.Resize(new SKImageInfo(width, height), SKFilterQuality.Medium);

                    // TRANSFORM TO GRAYSCALE
                    var surface = SKSurface.Create(resizedImage.Info);
                    var canvas = surface.Canvas;
                    using (SKPaint paint = new SKPaint())
                    {
                        paint.ColorFilter =
                            SKColorFilter.CreateColorMatrix(new float[]
                            {
                                0.21f, 0.72f, 0.07f, 0, 0,
                                0.21f, 0.72f, 0.07f, 0, 0,
                                0.21f, 0.72f, 0.07f, 0, 0,
                                0,     0,     0,     1, 0
                            });

                        canvas.DrawBitmap(resizedImage, resizedImage.Info.Rect, paint);
                    }
                    var grayScaleImage = SKBitmap.Decode(surface.Snapshot().Encode(SKEncodedImageFormat.Jpeg, QUALITY)).Pixels;

                    // SET GRAYSCALE ARRAY 
                    int[] grayScalePixels = new int[grayScaleImage.Length];
                    var limit = grayScaleImage.Length;
                    for (int i = 0; i < limit; i++)
                    {
                        var pixel = grayScaleImage[i];
                        grayScalePixels[i] = pixel.Red;
                    }
                    return new ProcessedImage(grayScalePixels, resizedImage.Width, resizedImage.Height, Path.GetFileNameWithoutExtension(imagePath));
                }
            }
        }
    }
}
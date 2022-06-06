using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Microsoft.ML;
using Microsoft.ML.Transforms.Onnx;

namespace BIAI_App_sandbox2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string modelPath = @"D:\Downloads\model.onnx";
                string[] outputColumnNames = new[] { "dense_1" };
                string[] inputColumnNames = new[] { "sequential_input" };
                MLContext mlContext = new MLContext();
                Console.WriteLine("XD");
                OnnxScoringEstimator pipeline = mlContext.Transforms.ApplyOnnxModel(outputColumnNames, inputColumnNames, modelPath);


                float[] inputData =new float[150528];

                const int width = 224;
                const int height = 224;

                string exampleImage = @"D:\Downloads\kukurydza_plamistosc.jpg";
                Image image = Image.FromFile(exampleImage);
                Bitmap newImage = new Bitmap(width, height);
                Console.WriteLine(image.Height);
                Console.WriteLine(image.Width);
                newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                Rectangle rect = new Rectangle(0, 0, width, height);
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;


                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                for (int i = 0, j = 0; i < 50176; i++, j += 3)
                {
                    Color col = newImage.GetPixel(i % 224, i / 224);
                    inputData[j] = col.R;
                    inputData[j + 1] = col.G;
                    inputData[j + 2] = col.B;
                }

                Input input = new Input() {Data = inputData};

                var dataView = mlContext.Data.LoadFromEnumerable(new[] {input});
                var transformedValues = pipeline.Fit(dataView).Transform(dataView);
                var output = mlContext.Data.CreateEnumerable<Output>(transformedValues, reuseRowObject: false);
                Console.WriteLine("XD");
                float[] outputArr = output.Single().Data;
                foreach (float f in outputArr)
                {
                    Console.WriteLine(f);
                }
            }

            catch (TypeInitializationException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.HelpLink);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace BIAI_App
{
    partial class MainWindowViewModel
    {
        private void SelectPathCommandExecute(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ImagePath = openFileDialog.FileName;
                Analyzed = false;
            }
        }

        private void AnalyzeImageCommandExecute(object parameter)
        {
            float[] inputData = new float[ImageWidth * ImageHeight * 3];
            try
            {
                Image image = Image.FromFile(ImagePath);
                Bitmap newImage = new Bitmap(ImageWidth, ImageHeight);
                newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                Rectangle rect = new Rectangle(0, 0, ImageWidth, ImageHeight);

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

                var dataView = _mlContext.Data.LoadFromEnumerable(new[] {input});
                var transformedValue = _pipeline.Fit(dataView).Transform(dataView);
                var output = _mlContext.Data.CreateEnumerable<Output>(transformedValue, reuseRowObject: false);

                float[] outputArr = output.Single().Data;
                int labelIndex = outputArr.IndexOfMax();
                ImageClassifiedLabel = _labels[labelIndex];
                Analyzed = true;
            }
            catch (Exception e)
            {
                Analyzed = false;
                MessageBox.Show(e.Message);
            }
        }

        private void InvalidFeedbackCommandExecute(object parameter)
        {
            if (!Analyzed) return;
            LabelSelectDialogBox labelSelectDialogBox = new LabelSelectDialogBox();
            bool? b = labelSelectDialogBox.ShowDialog();
            if (b == true)
            {
                FeedbackDatabaseSingleton.Instance.AddNewImage(ImagePath, _labels[labelSelectDialogBox.SelectedIndex]);
                Analyzed = false;
            }
        }
    }
}

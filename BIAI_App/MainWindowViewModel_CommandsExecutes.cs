using System;
using System.Collections.Generic;
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
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ImagePath = openFileDialog.FileName;
            }
        }

        private void AnalyzeImageCommandExecute()
        {
            MessageBox.Show("Analyzing Image");
        }
    }
}

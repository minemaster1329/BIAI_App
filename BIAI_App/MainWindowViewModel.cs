using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using BIAI_App.Annotations;
using Microsoft.ML;
using Microsoft.Win32;

namespace BIAI_App
{
    partial class MainWindowViewModel : INotifyPropertyChanged
    {
        private MLContext _mlContext;
        private string _imagePath;
        public MainWindowViewModel()
        {
            SelectPathCommand = new RelaySyncCommand(SelectPathCommandExecute);
        }

        public RelaySyncCommand SelectPathCommand { get; }
        public RelaySyncCommand AnalyzeImageCommand { get; }
        public string ImagePath
        {
            get => _imagePath;
            private set
            {
                _imagePath = value;
                _mlContext = new MLContext();
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

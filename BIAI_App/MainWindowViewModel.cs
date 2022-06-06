using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using BIAI_App.Annotations;
using Microsoft.ML;
using Microsoft.ML.Transforms.Onnx;
using Microsoft.Win32;

namespace BIAI_App
{
    partial class MainWindowViewModel : INotifyPropertyChanged
    {
        string _modelPath = @"model.onnx";

        string[] _inputColumnNames = new[] { "sequential_input" };
        string[] _outputColumnNames = new[] { "dense_1" };

        private const int _imageWidth = 224;
        private const int _imageHeight = 224;

        private string[] _labels =
        {
            "Corn___Common_Rust",
            "Corn___Gray_Leaf_Spot",
            "Corn___Healthy",
            "Corn___Leaf_Blight",
            "Invalid",
            "Potato___Early_Blight",
            "Potato___Healthy",
            "Potato___Late_Blight",
            "Rice___Brown_Spot",
            "Rice___Healthy",
            "Rice___Hispa",
            "Rice___Leaf_Blast",
            "Wheat___Brown_Rust",
            "Wheat___Healthy",
            "Wheat___Yellow_Rust"
        };

        private MLContext _mlContext;
        private OnnxScoringEstimator _pipeline;

        private string _imagePath;
        private string _imageClassifiedLabel;
        public MainWindowViewModel()
        {
            _mlContext = new MLContext();
            _pipeline = _mlContext.Transforms.ApplyOnnxModel(_outputColumnNames, _inputColumnNames, _modelPath);
            SelectPathCommand = new RelaySyncCommand(SelectPathCommandExecute);
            AnalyzeImageCommand = new RelaySyncCommand(AnalyzeImageCommandExecute);
        }

        public RelaySyncCommand SelectPathCommand { get; }
        public RelaySyncCommand AnalyzeImageCommand { get; }
        public string ImagePath
        {
            get => _imagePath;
            private set
            {
                _imagePath = value;
                OnPropertyChanged();
            }
        }

        public string ImageClassifiedLabel
        {
            get => _imageClassifiedLabel;
            set
            {
                _imageClassifiedLabel = value;
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

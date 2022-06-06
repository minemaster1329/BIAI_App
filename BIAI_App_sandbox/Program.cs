// See https://aka.ms/new-console-template for more information

using Microsoft.ML;
using Microsoft.ML.Transforms.Onnx;
using BIAI_App_sandbox;

Console.WriteLine("Hello, World!");

try
{
    string modelPath = @"D:\Downloads\model.onnx";
    if (File.Exists(modelPath))
    {
        Console.WriteLine("Model file exists");
    }

    string[] outputColumnNames = new[] {"dense_9"};
    string[] inputColumnNames = new[] {"sequential_8"};
    MLContext mlContext = new MLContext();
    Console.WriteLine("XD");
    OnnxScoringEstimator pipeline = mlContext.Transforms.ApplyOnnxModel(outputColumnNames, inputColumnNames, modelPath);
}

catch (TypeInitializationException e)
{
    Console.WriteLine(e.StackTrace);
}

catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine(e.Message);
}

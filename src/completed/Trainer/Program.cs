using System;
using System.IO;
using GithubIssueClassifier.Model;
using Microsoft.ML;

namespace Trainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var mlContext = new MLContext();

            var dataView = mlContext.Data.LoadFromTextFile<GithubIssue>(
                "../Data/corefx-issues-train.tsv",
                separatorChar: '\t',
                hasHeader: true,
                allowSparse: false);

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "Area")
                .Append(mlContext.Transforms.Text.FeaturizeText("TitleFeaturized", "Title"))
                .Append(mlContext.Transforms.Text.FeaturizeText("DescriptionFeaturized", "Description"))
                .Append(mlContext.Transforms.Concatenate("Features", "TitleFeaturized", "DescriptionFeaturized"))
                .AppendCacheCheckpoint(mlContext);

            var trainer = mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features");

            var trainingPipeline = pipeline
                .Append(trainer)
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainedModel = trainingPipeline.Fit(dataView);

            using (var outputStream = File.OpenWrite("../Website/GithubClassifier.zip"))
            {
                mlContext.Model.Save(trainedModel, dataView.Schema, outputStream);
            }

            var results = mlContext.MulticlassClassification.CrossValidate(
                data: dataView, estimator: trainingPipeline, numberOfFolds: 5
            );

            foreach (var entry in results)
            {
                Console.WriteLine(entry.Metrics.ConfusionMatrix.GetFormattedConfusionTable());
            }
        }
    }
}

using Microsoft.ML;

namespace GithubIssueClassifier.Model
{
    public class GithubIssueLabeler
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _trainedModel;
        private readonly PredictionEngine<GithubIssue, GithubIssuePrediction> _predictionEngine;

        public GithubIssueLabeler(string modelFile)
        {
            _mlContext = new MLContext();
            _trainedModel = _mlContext.Model.Load(modelFile, out var inputSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<GithubIssue, GithubIssuePrediction>(_trainedModel);
        }

        public GithubIssuePrediction PredictLabel(GithubIssue issue)
        {
            var prediction = _predictionEngine.Predict(issue);
            return prediction;
        }
    }
}
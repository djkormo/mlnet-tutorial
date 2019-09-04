using Microsoft.ML.Data;

namespace GithubIssueClassifier.Model
{
    public class GithubIssuePrediction
    {
        [ColumnName("PredictedLabel")]
        public string Area { get; set; }

        public float[] Score { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Runtime.Learners;

namespace TheLittleSpoon.Data
{
    class MachineLearning
    {
        // This class describes the strcuture of the training data
        public class RelatedArticleData
        {
            [Column("0")]
            public float CurrentArticleId;

            // We only set this column while training (from the training data)
            [Column("1")]
            [ColumnName("Label")]
            public float Label;
        }


        // This class is built by the learning model
        public class RelatedArticlesPrediction
        {
            [ColumnName("PredictedLabel")]
            public float PredictedRelatedArticle;
        }


        // This method gets an article ID and predicts the most related article
        public static int GetRelatedArticle(string dataPath, int ArticleId)
        {
            // Create an environment for the learning process
            LocalEnvironment env = new LocalEnvironment();

            // Create a reader object to parse our training data from the training data file
            TextLoader reader = new TextLoader(env,
                new TextLoader.Arguments()
                {
                    Separator = ",",
                    HasHeader = true,
                    Column = new[]
                    {
                            new TextLoader.Column("CurrentArticleId", DataKind.R4, 0),
                            new TextLoader.Column("Label", DataKind.R4, 1)
                    }
                });

            // Read the training data
            IDataView trainingData = reader.Read(new MultiFileSource(dataPath));

            // Process the training data, set a target column and create a learning model (SDCA multi-class model)
            EstimatorChain<KeyToValueTransform> pipeline = new TermEstimator(env, "Label", "Label")
                   .Append(new ConcatEstimator(env, "Features", "CurrentArticleId"))
                   .Append(new SdcaMultiClassTrainer(env, new SdcaMultiClassTrainer.Arguments()))
                   .Append(new KeyToValueEstimator(env, "PredictedLabel"));

            // Train the learning model based on the training data
            TransformerChain<KeyToValueTransform> model = pipeline.Fit(trainingData);

            // Activate the model to make a prediction for the requested article
            RelatedArticlesPrediction prediction = model.MakePredictionFunction<RelatedArticleData, RelatedArticlesPrediction>(env).Predict(
                new RelatedArticleData()
                {
                    CurrentArticleId = (float)ArticleId
                });

            // Return the predicted articles ID
            return (int)prediction.PredictedRelatedArticle;
        }
    }
}

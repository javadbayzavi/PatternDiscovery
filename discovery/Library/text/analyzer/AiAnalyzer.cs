using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using discovery.Models;
using Microsoft.ML;

namespace discovery.Library.text.analyzer
{
    //Concrete analyzer which is using database engine to do anaylzing process
    public class AiAnalyzer : Analyzer
    {
        private List<int> selectedPatterns = new List<int>();

        public AiAnalyzer(ISubmitter submitter) : base(submitter)
        {
        }

        public override void Clean()
        {
        }

        public override void Filter()
        {
        }

        public override void Lemmatize()
        {
        }

        public override void Stemming()
        {
        }

        public override void Tokenize()
        {
            var mlcontext = new MLContext();
            var dbcontext = (discoveryContext)this._submitterEngine.GetContext();
            var data = mlcontext.Data.LoadFromEnumerable(dbcontext.dataset
                .Select(a => new datasetinputanalyzemodel
                { 
                    body = a.body,
                    ID = a.ID,
                    subject = a.subject
                }));

            var testtainData = mlcontext.Data.TrainTestSplit(data, testFraction : 0.2,"ID");
            var trainSet = testtainData.TrainSet;
            var testSet = testtainData.TestSet;
            var pipeline = mlcontext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: "Output")
                .Append(mlcontext.Transforms.Concatenate("Feaure", "subject","body"))
                .Append(mlcontext.MulticlassClassification.Trainers.OneVersusAll(mlcontext.BinaryClassification.Trainers.AveragedPerceptron("Label", "Features", numberOfIterations: 10))
                .Append(mlcontext.Transforms.Conversion.MapKeyToValue("PredictedLabel")));
            
            var crossValResults = mlcontext.MulticlassClassification.CrossValidate(data: data, estimator: pipeline, numberOfFolds: 10, labelColumnName: "Label");
            var metricsInMultipleFolds = crossValResults.Select(r => r.Metrics);
            var microAccuracyValues = metricsInMultipleFolds.Select(m => m.MicroAccuracy);
            var microAccuracyAverage = microAccuracyValues.Average();
            var macroAccuracyValues = metricsInMultipleFolds.Select(m => m.MacroAccuracy);
            var macroAccuracyAverage = macroAccuracyValues.Average();
            var logLossValues = metricsInMultipleFolds.Select(m => m.LogLoss);
            var logLossAverage = logLossValues.Average();
            var logLossReductionValues = metricsInMultipleFolds.Select(m => m.LogLossReduction);
            var logLossReductionAverage = logLossReductionValues.Average();

            var model = pipeline.Fit(data);
            var predictionEngine = mlcontext.Model.CreatePredictionEngine<datasetinputanalyzemodel, datasetpredictedmodel>(model);


        }


        public override void Transform()
        {
        }

        public override void SubmitResult()
        {
            //Do some data transaciton befor submit
            this._submitterEngine.Submit();
        }
    }
}

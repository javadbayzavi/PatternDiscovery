using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using discovery.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;

namespace discovery.Library.analyzer
{
    //Concrete analyzer which is using database engine to do anaylzing process
    public class AiAnalyzer : Analyzer
    {
        private IDataView _data;
        private IDataView _keys;
        private MLContext _analyzeEngine;
        private ITransformer _trainedModel;
        private IEstimator<ITransformer> _pipeline;
        private PredictionEngine<datasetinputanalyzemodel, datasetpredictedmodel> _predictionEngine;

        public AiAnalyzer(ISubmitter submitter) : base(submitter)
        {
            //Initialize Text analyzer engine
            this._analyzeEngine = new MLContext();
            var dbcontext = (discoveryContext)this._submitterEngine.GetContext();

            //Load text dataset into data componenet of text analyzer engine
            this._data = this._analyzeEngine.Data.LoadFromEnumerable(dbcontext.dataset
                .Select(a => new datasetinputanalyzemodel
                {
                    body =  a.body,
                }).ToList());

        }

        public override void Clean()
        {
            //Remove unnecessary word form the input text    
            this._pipeline = this._pipeline.Append(this._analyzeEngine.Transforms.Text.RemoveDefaultStopWords("words"));
        }

        public override void Filter()
        {
            //normalize text before any processing 
            this._pipeline = this._analyzeEngine.Transforms.Text.NormalizeText("body");
        }

        public override void Lemmatize()
        {
            this._pipeline = this._pipeline.Append(this._analyzeEngine.Transforms.Text.ProduceWordBags("Score", "words",
                                ngramLength: 1, useAllLengths: false,
                                weighting: NgramExtractingEstimator.WeightingCriteria.Tf));
        }

        public override void Stemming()
        {
        }

        public override void Tokenize()
        {
            this._pipeline = this._pipeline.Append(this._analyzeEngine.Transforms.Text.TokenizeIntoWords("words","body"));
        }


        public override void Transform()
        {
            //Apply training to the model
            this._trainedModel = this._pipeline.Fit(this._data);
            this._keys = this._trainedModel.Transform(this._data);
            this._predictionEngine = this._analyzeEngine.Model.CreatePredictionEngine<datasetinputanalyzemodel, datasetpredictedmodel>(this._trainedModel);
            string[] patterns = ((string)this._pattern).Split(",");
            //in a loop 
            foreach (var ptrn in patterns)
            {
                var res = this._predictionEngine.Predict(new datasetinputanalyzemodel() { body = ptrn });

                VBuffer<ReadOnlyMemory<char>> slotNames = default;
                this._keys.Schema["Score"].GetSlotNames(ref
                    slotNames);

                var BagOfWordFeaturesColumn = this._keys.GetColumn<VBuffer<
                    float>>(this._keys.Schema["Score"]);

                var slots = slotNames.GetValues();
                Console.Write("N-grams: ");
                List<string> keyNames = new List<string>();
               // var tti = slots.ToArray().Where(a => a.)

                foreach (var featureRow in BagOfWordFeaturesColumn)
                {
                    foreach (var item in featureRow.Items())
                        if(slots[item.Key].ToString().Contains(ptrn))
                            keyNames.Add($"{slots[item.Key]}  ");
                }
            }

        }

        public override void SubmitResult()
        {
            //Do some data transaciton befor submit
            this._submitterEngine.Submit();
        }
    }
}

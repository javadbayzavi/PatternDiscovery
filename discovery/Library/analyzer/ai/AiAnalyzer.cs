using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Annytab.Stemmer;
using discovery.Library.Core;
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
        private List<datasetinputanalyzemodel> _rawdata = new List<datasetinputanalyzemodel>();
        private IDataView _keys;
        private MLContext _analyzeEngine;
        private ITransformer _trainedModel;
        private IEstimator<ITransformer> _pipeline;
        private PredictionEngine<datasetinputanalyzemodel, datasetpredictedmodel> _predictionEngine;
        private IStemmer _stemmerEngine = new EnglishStemmer();

        public AiAnalyzer(ISubmitter submitter, int scenario) : base(submitter, scenario)
        {
            //Initialize Text analyzer engine
            this._analyzeEngine = new MLContext();
        }

        public override void Clean()
        {
            //Remove unnecessary word form the input text    
            this._pipeline = this._pipeline.Append(this._analyzeEngine.Transforms.Text.RemoveDefaultStopWords("words"));
        }

        public override void Filter()
        {
            var dbcontext = (discoveryContext)this._submitterEngine.GetContext();


            string[] patterns = ((string)this._pattern).Split(",");
            foreach (var item in patterns)
            {
                if (item == "")
                    continue;

                var ptrn = dbcontext.patterns.Find(Convert.ToInt32(item));

                if (ptrn != null)
                    this._rawdata.Add(new datasetinputanalyzemodel()
                    {
                        body = ptrn.title,
                        ID = ptrn.ID,
                    });
            }
            //inject data in to machine
            this._data = this._analyzeEngine.Data.LoadFromEnumerable(this._rawdata);

        }

        public override void Lemmatize()
        {
            this._pipeline = this._pipeline.Append(this._analyzeEngine.Transforms.Text.ProduceWordBags("Score", "words",
                                ngramLength: 1, useAllLengths: false,
                                weighting: NgramExtractingEstimator.WeightingCriteria.Tf));
        }

        public override void Stemming()
        {
            foreach (var rawItem in this._rawdata)
            {
                rawItem.lemmatizedbody = String.Join(" ", this._stemmerEngine.GetSteamWords(rawItem.body.Split(" ")));
            }

            //normalize text before any processing 
            this._pipeline = this._analyzeEngine.Transforms.Text.NormalizeText("lemmatizedbody");
        }

        public override void Tokenize()
        {
            this._pipeline = this._pipeline.Append(this._analyzeEngine.Transforms.Text.TokenizeIntoWords("words", "lemmatizedbody"));
        }


        public override void Transform()
        {
            //Apply training to the model
            this._trainedModel = this._pipeline.Fit(this._data);
            this._keys = this._trainedModel.Transform(this._data);
            this._predictionEngine = this._analyzeEngine.Model
                .CreatePredictionEngine<datasetinputanalyzemodel, datasetpredictedmodel>(this._trainedModel);

            
            //List<resultviewmodel> foundpatterns = new List<resultviewmodel>();
            var dbcontext = (discoveryContext)this._submitterEngine.GetContext();

            var targetSet = dbcontext.dataset.Where(datItem => datItem.scenarioid == this._currentscenario);
            
            //in a loop all the dataset will be examined for selected pattern in a trained model
            foreach (var datasetItem in targetSet)
            {
                var res = this._predictionEngine.Predict(new datasetinputanalyzemodel()
                { 
                    lemmatizedbody = String.Join(" ", this._stemmerEngine.GetSteamWords(datasetItem.body.Split(" "))) 
                });

                VBuffer<ReadOnlyMemory<char>> slotNames = default;
                this._keys.Schema["Score"].GetSlotNames(ref
                    slotNames);

                var BagOfWordFeaturesColumn = this._keys.GetColumn<VBuffer<
                    float>>(this._keys.Schema["Score"]);

                var slots = slotNames.GetValues();

                foreach (var featureRow in BagOfWordFeaturesColumn)
                {
                    //This inner foreach run for one iteration
                    foreach (var item in featureRow.Items())
                    {
                        string key = $"{slots[item.Key]}";

                        if (Convert.ToInt32(res.Score[item.Key]) > 0)
                        {
                            foreach (var fndptrn in this._rawdata.Where(a => a.lemmatizedbody.Contains(key)))
                            {
                                //Check to add to current found keywords
                                if (this.results.Any(fdpt => fdpt.patternid == fndptrn.ID && fdpt.datasetitemid == datasetItem.ID))
                                {
                                    this.results.First(fdpt => fdpt.patternid == fndptrn.ID && fdpt.datasetitemid == datasetItem.ID).count += Convert.ToInt32(res.Score[item.Key]);
                                }
                                else
                                {
                                    //Add new found patterns
                                    this.results.Add(new result()
                                    {
                                        count = Convert.ToInt32(res.Score[item.Key]),
                                        datasetitemid = datasetItem.ID,
                                        patternid = fndptrn.ID,
                                        scenarioid = this._currentscenario                                        
                                    });
                                }

                            }
                        }
                    }
                }
                //Check for memory dump action
                if (new performancetuner().MemoryOveload(ref this.results))
                {
                    //this.SubmitResult();
                    var emmergency = new emergncyDbContext();
                    emmergency.result.AddRange(this.results);
                    emmergency.SaveChanges();
                    //free dynamic memory
                    this.results = new List<result>();
                    //foundpatterns.Clear();
                    //targetSet.Remove(datasetItem);
                }

            }
        }

        public override void SubmitResult()
        {
            //Set context of this analyzer to discovery DbContext
            var context = (discoveryContext)this._submitterEngine.GetContext();

            //load result into the submitter context for adding to database
            context.result.AddRange(this.results);
            //Do some data transaciton befor submit
            this._submitterEngine.Submit();
        }
    }
}

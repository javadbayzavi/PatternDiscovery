using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.Data;

namespace discovery.Models
{
    //This model hold the prediction result of ai analyzing
    public class datasetpredictedmodel : result
    {
        [ColumnName("PredictedLabel")]
        public float Prediction { get; set; }

        public float Probability { get; set; }

        public float[] Score { get; set; }

    }
}


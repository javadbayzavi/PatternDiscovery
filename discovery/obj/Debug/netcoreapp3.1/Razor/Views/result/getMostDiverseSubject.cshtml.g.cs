#pragma checksum "E:\PatternDiscovery\discovery\Views\result\getMostDiverseSubject.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9fac1ddc11952013cedf157d5f65b2dfc44a337d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_result_getMostDiverseSubject), @"mvc.1.0.view", @"/Views/result/getMostDiverseSubject.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\PatternDiscovery\discovery\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9fac1ddc11952013cedf157d5f65b2dfc44a337d", @"/Views/result/getMostDiverseSubject.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"14f4dac5d67e71ab7dddc1e2dca3fe94aa8cf45f", @"/Views/_ViewImports.cshtml")]
    public class Views_result_getMostDiverseSubject : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""card card-blue h-100"">
    <div class=""card-header"">
        <h3 class=""card-title"">Top 10 most diverse subjects</h3>

        <div class=""card-tools"">
            <button type=""button"" class=""btn btn-tool"" data-card-widget=""collapse"">
                <i class=""fas fa-minus""></i>
            </button>
            <button type=""button"" class=""btn btn-tool"" data-card-widget=""remove"">
                <i class=""fas fa-times""></i>
            </button>
        </div>
    </div>
    <div class=""card-body"">
        <canvas id=""barMostDiverseChart"" style=""min-height: 250px; height: 250px; max-height: 250px; max-width: 100%;""></canvas>
    </div>
    <!-- /.card-body -->
</div>
<script>
    function drawDiverseChart(chartData) {
        var barLabels = [];
        var barData = [];

        for (let index = 0; index < chartData.length; index++) {
            //Caculation of others
            //if (index > 9) {
            //    if (index == 10) {
            //        barLabels.");
            WriteLiteral(@"push(""Others"");
            //        barData.push(chartData[index].count);
            //    }
            //    else
            //        barData[10] += chartData[index].count;
            //}
            //else {
                barLabels.push(chartData[index].subject);
                barData.push(chartData[index].count);
            //}
        }

        var Data = {
            labels: barLabels,
            datasets: [
                {
                    label: 'Subject',
                    backgroundColor: 'rgba(60,141,188,0.9)',
                    borderColor: 'rgba(60,141,188,0.8)',
                    pointRadius: true,
                    pointColor: '#3b8bba',
                    pointStrokeColor: 'rgba(60,141,188,1)',
                    pointHighlightFill: '#fff',
                    pointHighlightStroke: 'rgba(60,141,188,1)',
                    data: barData
                },
            ]
        }

        var barChartCanvas = $('#barMostDiverseChart').ge");
            WriteLiteral(@"t(0).getContext('2d')
        var barChartData = $.extend(true, {}, Data);
            barChartData.datasets[0] = Data.datasets[0];
        var barOptions = {
            maintainAspectRatio: false,
            responsive: true,
            datasetFill: false,
            scaleOverride: true,
            scaleSteps: 1,
            scaleStepWidth: 100,
            scaleStartValue: 0
        }

        new Chart(barChartCanvas, {
            type: 'bar',
            data: barChartData,
            options: barOptions
        });

    }

    $.ajax({
        type: ""GET"",
        url: '/api/chart/subjectWithMostDiversePattern/10',
        contentType: ""application/json"",
        dataType: ""json"",
        success: function (chData) {
            drawDiverseChart(chData)
        }
    });


</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

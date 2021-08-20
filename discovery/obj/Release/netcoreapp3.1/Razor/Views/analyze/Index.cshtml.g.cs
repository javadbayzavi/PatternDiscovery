#pragma checksum "E:\PatternDiscovery\discovery\Views\analyze\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e9d6f57ac85b1938f6bd1fc67cc5da948720b49a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_analyze_Index), @"mvc.1.0.view", @"/Views/analyze/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9d6f57ac85b1938f6bd1fc67cc5da948720b49a", @"/Views/analyze/Index.cshtml")]
    public class Views_analyze_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/analyze/ai"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/analyze/conventional"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\PatternDiscovery\discovery\Views\analyze\Index.cshtml"
  
    bool scenario = (bool)ViewBag.currentScenario;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\PatternDiscovery\discovery\Views\analyze\Index.cshtml"
 if (scenario)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div id=""infomodal"" class=""modal"" tabindex=""-1"" role=""dialog"" data-backdrop=""static"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"">Text analyzer</h5>
                </div>
                <div class=""modal-body"">
                    <p>There is no active scenario. First refer to Scenarios and start one scenario from the list</p>
                </div>
                <div class=""modal-footer"">
                    ");
#nullable restore
#line 17 "E:\PatternDiscovery\discovery\Views\analyze\Index.cshtml"
               Write(Html.ActionLink("Scenario List", "Index", "scenario"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <script>\r\n        $(\'#infomodal\').modal(null)\r\n    </script>\r\n");
#nullable restore
#line 25 "E:\PatternDiscovery\discovery\Views\analyze\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 27 "E:\PatternDiscovery\discovery\Views\analyze\Index.cshtml"
 if ((bool)ViewBag.notready)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div id=""infomodal2"" class=""modal"" tabindex=""-1"" role=""dialog"" data-backdrop=""static"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"">Text analyzer</h5>
                </div>
                <div class=""modal-body"">
                    <p>The current scenario is not ready for analysis. Please check the current status of it in scenario list and follow the right steps.</p>
                </div>
                <div class=""modal-footer"">
                    ");
#nullable restore
#line 39 "E:\PatternDiscovery\discovery\Views\analyze\Index.cshtml"
               Write(Html.ActionLink("Data Perparation", "Index", "import"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <script>\r\n        $(\'#infomodal2\').modal(null)\r\n    </script>\r\n");
#nullable restore
#line 47 "E:\PatternDiscovery\discovery\Views\analyze\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""card"">
    <div class=""card-header card-primary"">
        <h3> Wellcome to Text Analyzing Module</h3>
    </div>
    <div class=""card-body"">
        <p>
            In this module the application provide to different methodology for goin deep into datasets and fetch the patterns
            <ol>
                <li>Conventional Method</li>
                <li>AI based Method</li>
            </ol>
        </p>
        <div class=""row"">
            <div class=""col-md-6 col-sm-12"">
                <div class=""card h-100"">
                    <div class=""card-header bg-gradient-orange"">
                        <b>AI Based Method</b>
                        <i class=""fa fa-robot float-right""></i>
                    </div>
                    <div class=""card-body"">
                        <p>
                            This method mainly works with Machine Learning library. The text minning run on ML.NET class library. A open source &Aacute; powerfull framework which offers dive");
            WriteLiteral("rse functionalities in various AI fields.\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"card-footer card-link text-center font-weight-bold\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9d6f57ac85b1938f6bd1fc67cc5da948720b49a7484", async() => {
                WriteLiteral("AI Based");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </div>
                </div>
            </div>
            <div class=""col-md-6 col-sm-12"">
                <div class=""card h-100"">
                    <div class=""card-header bg-gradient-green"">
                        <b>Conventional Method</b>
                        <i class=""fab fa-industry float-right""></i>
                    </div>
                    <div class=""card-body"">
                        <p>
                            This methodology heavely relies on database engine performance. The method uses common text search and string function in order to filter dataset and retrieve results
                        </p>
                    </div>
                    <div class=""card-footer card-link text-center font-weight-bold"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9d6f57ac85b1938f6bd1fc67cc5da948720b49a9384", async() => {
                WriteLiteral("Conventional");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
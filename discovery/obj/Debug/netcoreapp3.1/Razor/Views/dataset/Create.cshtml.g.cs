#pragma checksum "E:\PatternDiscovery\discovery\Views\dataset\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2886fbd9bcd98bafe19ab8e3de61c483f28dfe6d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_dataset_Create), @"mvc.1.0.view", @"/Views/dataset/Create.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2886fbd9bcd98bafe19ab8e3de61c483f28dfe6d", @"/Views/dataset/Create.cshtml")]
    public class Views_dataset_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<discovery.Models.dataset>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/dataset/List"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("~/dataset/Create"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", new global::Microsoft.AspNetCore.Html.HtmlString("post"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n<div class=\"card card-primary\">\r\n    <div class=\"card-header\">\r\n        \r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2886fbd9bcd98bafe19ab8e3de61c483f28dfe6d3718", async() => {
                WriteLiteral(" <span class=\"fa fa-fast-backward fa-backward float-right\"></span>");
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
            WriteLiteral("\r\n    </div>\r\n    <!-- /.card-header -->\r\n    <!-- form start -->\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2886fbd9bcd98bafe19ab8e3de61c483f28dfe6d4930", async() => {
                WriteLiteral("\r\n        ");
#nullable restore
#line 11 "E:\PatternDiscovery\discovery\Views\dataset\Create.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
        <div class=""card-body"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <div class=""form-group"">
                <label asp-for=""text"" class=""control-label""> Title: </label>
                <div class=""col"">
                    ");
#nullable restore
#line 17 "E:\PatternDiscovery\discovery\Views\dataset\Create.cshtml"
               Write(Html.TextBoxFor(model => model.subject,"",new { @class ="form-control"}));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                    <span asp-validation-for=""text"" class=""text-danger""></span>
                </div>
            </div>
            <div class=""form-group"">
                <label asp-for=""text"" class=""control-label""> Author: </label>
                ");
#nullable restore
#line 23 "E:\PatternDiscovery\discovery\Views\dataset\Create.cshtml"
           Write(Html.TextBoxFor(model => model.author, "", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                <span asp-validation-for=\"text\" class=\"text-danger\"></span>\r\n            </div>\r\n            <div class=\"form-group\">\r\n                <label asp-for=\"text\" class=\"control-label\"> Date: </label>\r\n                ");
#nullable restore
#line 28 "E:\PatternDiscovery\discovery\Views\dataset\Create.cshtml"
           Write(Html.TextBoxFor(model => model.date, "", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                <span asp-validation-for=\"text\" class=\"text-danger\"></span>\r\n            </div>\r\n            <div class=\"form-group\">\r\n                <label asp-for=\"text\" class=\"control-label\"> Body: </label>\r\n                ");
#nullable restore
#line 33 "E:\PatternDiscovery\discovery\Views\dataset\Create.cshtml"
           Write(Html.TextAreaFor(model => model.body,7,50,new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                <span asp-validation-for=""text"" class=""text-danger""></span>
            </div>
        </div>
        <div class=""card-footer"">
            <div class=""form-group"">
                <input type=""submit"" value=""Create"" class=""btn btn-primary"" />
            </div>
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<discovery.Models.dataset> Html { get; private set; }
    }
}
#pragma warning restore 1591

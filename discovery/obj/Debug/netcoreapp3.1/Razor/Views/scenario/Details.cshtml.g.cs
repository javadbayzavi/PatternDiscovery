#pragma checksum "E:\PatternDiscovery\discovery\Views\scenario\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "294bd09779bd6fb297f020cb8d4063fc6daa30c5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_scenario_Details), @"mvc.1.0.view", @"/Views/scenario/Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"294bd09779bd6fb297f020cb8d4063fc6daa30c5", @"/Views/scenario/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"14f4dac5d67e71ab7dddc1e2dca3fe94aa8cf45f", @"/Views/_ViewImports.cshtml")]
    public class Views_scenario_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<discovery.Models.scenarioviewmodel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/scenario"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("<div class=\"card\">\r\n    <div class=\"card-header\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "294bd09779bd6fb297f020cb8d4063fc6daa30c53341", async() => {
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
            WriteLiteral(@"
    </div>
    <!-- /.card-header -->
    <div class=""card-body"">
        <table id=""datasettbl"" class=""table table-bordered table-hover table-striped"">
            <thead>
                <tr>
                    <th>
                        ID
                    </th>
                    <td>
                        ");
#nullable restore
#line 18 "E:\PatternDiscovery\discovery\Views\scenario\Details.cshtml"
                   Write(Model.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>\r\n                        Name\r\n                    </th>\r\n                    <td>\r\n                        ");
#nullable restore
#line 26 "E:\PatternDiscovery\discovery\Views\scenario\Details.cshtml"
                   Write(Model.name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>\r\n                        Version\r\n                    </th>\r\n                    <td>\r\n                        ");
#nullable restore
#line 34 "E:\PatternDiscovery\discovery\Views\scenario\Details.cshtml"
                   Write(Model.sversion.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>\r\n                        Date\r\n                    </th>\r\n                    <td>\r\n                        ");
#nullable restore
#line 42 "E:\PatternDiscovery\discovery\Views\scenario\Details.cshtml"
                   Write(Model.createddate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>\r\n                        Source\r\n                    </th>\r\n                    <td>\r\n                        ");
#nullable restore
#line 50 "E:\PatternDiscovery\discovery\Views\scenario\Details.cshtml"
                   Write(Model.datasource);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>\r\n                        Source Type\r\n                    </th>\r\n                    <td>\r\n                        ");
#nullable restore
#line 58 "E:\PatternDiscovery\discovery\Views\scenario\Details.cshtml"
                   Write(Model.sourcetype.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>\r\n                        Analyze Method\r\n                    </th>\r\n                    <td>\r\n                        ");
#nullable restore
#line 66 "E:\PatternDiscovery\discovery\Views\scenario\Details.cshtml"
                   Write(Model.method.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>\r\n                        Status\r\n                    </th>\r\n                    <td>\r\n                        ");
#nullable restore
#line 74 "E:\PatternDiscovery\discovery\Views\scenario\Details.cshtml"
                   Write(Model.status.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n\r\n            </thead>\r\n        </table>\r\n    </div>\r\n    <!-- /.card-body -->\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<discovery.Models.scenarioviewmodel> Html { get; private set; }
    }
}
#pragma warning restore 1591

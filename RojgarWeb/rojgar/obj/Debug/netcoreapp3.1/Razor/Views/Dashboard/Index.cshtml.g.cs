#pragma checksum "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80b1c6ce07812cf9dff88a97a92dd7c8aa58659e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_Index), @"mvc.1.0.view", @"/Views/Dashboard/Index.cshtml")]
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
#line 1 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\_ViewImports.cshtml"
using rojgar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\_ViewImports.cshtml"
using rojgar.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"80b1c6ce07812cf9dff88a97a92dd7c8aa58659e", @"/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7788033d5439d4bbf04830617a3396598c0ba017", @"/Views/_ViewImports.cshtml")]
    public class Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("hold-transition skin-blue sidebar-mini"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 3 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
  
    ViewBag.Title = "Dashboard";

#line default
#line hidden
#nullable disable
            WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80b1c6ce07812cf9dff88a97a92dd7c8aa58659e4576", async() => {
                WriteLiteral(@"
    <meta charset=""utf-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <title>Vigyan Dhara | Dashboard</title>
    <meta content=""width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no"" name=""viewport"">
    <link rel=""icon"" href=""assets/images/favicon.png"">
    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"">
    <link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"">
    <link rel=""stylesheet"" href=""components/Ionicons/css/ionicons.min.css"">
    <link rel=""stylesheet"" href=""assets/css/style.css"">
    <link rel=""stylesheet"" href=""assets/css/AdminLTE.css"">
    <link rel=""stylesheet"" href=""assets/css/allskin.css"">
    <link href=""https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap"" rel=""stylesheet"">
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80b1c6ce07812cf9dff88a97a92dd7c8aa58659e6477", async() => {
                WriteLiteral("\r\n    <div class=\"wrapper\">\r\n\r\n        <div class=\"content-wrapper\">\r\n\r\n            <section class=\"content-header\">\r\n                <h1>Dashboard</h1>\r\n                <ol class=\"breadcrumb\">\r\n                    <li>");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80b1c6ce07812cf9dff88a97a92dd7c8aa58659e6974", async() => {
                    WriteLiteral("<i class=\"fa fa-home\"></i> Home");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"</li>
                    <li class=""active"">Dashboard</li>
                </ol>
            </section>
            <section class=""content"">
                <div class=""row"">
                    <div class=""col-lg-3 col-xs-6"">
                        <div class=""small-box bg-aqua"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 39 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
                               Write(ViewBag.totaluser);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                                <p>No. of users</p>
                            </div>
                            <div class=""icon"">
                                <i class=""fa fa-book""></i>
                            </div>
                            <a href=""#"" class=""small-box-footer"">More info <i class=""fa fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <div class=""col-lg-3 col-xs-6"">
                        <div class=""small-box bg-aqua"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 51 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
                               Write(ViewBag.totalcategory);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                                <p>No. of categories</p>
                            </div>
                            <div class=""icon"">
                                <i class=""fa fa-user""></i>
                            </div>
                            <a href=""#"" class=""small-box-footer"">More info <i class=""fa fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <div class=""col-lg-3 col-xs-6"">
                        <div class=""small-box bg-aqua"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 63 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
                               Write(ViewBag.totaljobs);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                                <p>No. of jobs</p>
                            </div>
                            <div class=""icon"">
                                <i class=""ion ion-person-add""></i>
                            </div>
                            <a href=""#"" class=""small-box-footer"">More info <i class=""fa fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <div class=""col-lg-3 col-xs-6"">
                        <div class=""small-box bg-aqua"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 75 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
                               Write(ViewBag.totalrequest);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                                <p>No. of request</p>
                            </div>
                            <div class=""icon"">
                                <i class=""fa fa-users""></i>
                            </div>
                            <a href=""#"" class=""small-box-footer"">More info <i class=""fa fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                </div>
                <div class=""row"">
                    <div class=""col-lg-3 col-xs-6"">
                        <div class=""small-box bg-aqua"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 89 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
                               Write(ViewBag.totalprocessed);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                                <p>No. of processed</p>
                            </div>
                            <div class=""icon"">
                                <i class=""fa fa-video-camera""></i>
                            </div>
                            <a href=""#"" class=""small-box-footer"">More info <i class=""fa fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <div class=""col-lg-3 col-xs-6"">
                        <div class=""small-box bg-aqua"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 101 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
                               Write(ViewBag.totalcomplete);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                                <p>No. of completed</p>
                            </div>
                            <div class=""icon"">
                                <i class=""ion ion-stats-bars""></i>
                            </div>
                            <a href=""#"" class=""small-box-footer"">More info <i class=""fa fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <div class=""col-lg-3 col-xs-6"">
                        <div class=""small-box bg-aqua"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 113 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
                               Write(ViewBag.totalcanceled);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                                <p>No. of canceled</p>
                            </div>
                            <div class=""icon"">
                                <i class=""fa fa-file""></i>
                            </div>
                            <a href=""#"" class=""small-box-footer"">More info <i class=""fa fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <div class=""col-lg-3 col-xs-6"">
                        <div class=""small-box bg-aqua"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 125 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
                               Write(ViewBag.totalpayment);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                                <p>No.of payments</p>
                            </div>
                            <div class=""icon"">
                                <i class=""fa fa-toggle-right""></i>
                            </div>
                            <a href=""#"" class=""small-box-footer"">More info <i class=""fa fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                    <div class=""col-lg-3 col-xs-6"">
                        <div class=""small-box bg-aqua"">
                            <div class=""inner"">
                                <h3>");
#nullable restore
#line 137 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\Index.cshtml"
                               Write(ViewBag.totalrefunds);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                                <p>No.of refunds</p>
                            </div>
                            <div class=""icon"">
                                <i class=""fa fa-toggle-right""></i>
                            </div>
                            <a href=""#"" class=""small-box-footer"">More info <i class=""fa fa-arrow-circle-right""></i></a>
                        </div>
                    </div>
                </div>
            </section>
            </div>
        </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n                <script>\r\n                    highlightMenuItem(\"Dashboard\");\r\n                </script>\r\n            ");
            }
            );
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
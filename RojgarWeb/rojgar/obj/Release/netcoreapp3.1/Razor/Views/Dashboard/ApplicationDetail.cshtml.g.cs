#pragma checksum "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4a1c1406fa3974a040f3516df20a90c7846a50f4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_ApplicationDetail), @"mvc.1.0.view", @"/Views/Dashboard/ApplicationDetail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4a1c1406fa3974a040f3516df20a90c7846a50f4", @"/Views/Dashboard/ApplicationDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7788033d5439d4bbf04830617a3396598c0ba017", @"/Views/_ViewImports.cshtml")]
    public class Views_Dashboard_ApplicationDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<rojgar.Models.ApplicationHistory>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
  
    ViewData["Title"] = "ApplicationDetail";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Application Detail</h1>\r\n\r\n<div>\r\n    <h4>Application History</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 15 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.JobId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 18 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.JobId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 21 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.JobPost));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 24 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.JobPost.JobPostId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 27 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.PaymentId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 30 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.PaymentId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 33 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.Category));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 36 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.Category));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 39 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.FormFee));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 42 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.FormFee));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 45 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.FormFillingFee));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 48 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.FormFillingFee));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 51 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.AdmitCardFee));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 54 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.AdmitCardFee));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 57 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 60 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 63 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.Remark));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 66 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.Remark));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 69 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.User));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 72 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.User.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 75 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayNameFor(model => model.TimeStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 78 "C:\Users\mande\Desktop\Rojgar\RojgarWeb\rojgar\Views\Dashboard\ApplicationDetail.cshtml"
       Write(Html.DisplayFor(model => model.TimeStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<rojgar.Models.ApplicationHistory> Html { get; private set; }
    }
}
#pragma warning restore 1591

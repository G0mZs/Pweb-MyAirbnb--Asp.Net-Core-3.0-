#pragma checksum "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4314887411b0b83e0ea49d614e45dea808c496f9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Clients), @"mvc.1.0.view", @"/Views/User/Clients.cshtml")]
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
#line 1 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\_ViewImports.cshtml"
using PwebTP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\_ViewImports.cshtml"
using PwebTP.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4314887411b0b83e0ea49d614e45dea808c496f9", @"/Views/User/Clients.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"129e29b7ea923ab050372b84cc2bcd00c9e5d113", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Clients : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<PwebTP.Models.ApplicationUser>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
  
    ViewData["Title"] = "Clients";
     Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""container mt-3 mb-3"">
    <h1>Clients</h1>
    <div class=""table"" style=""padding:5rem;"">
        <table id=""searchTable"" class=""table table-bordered table-hover table-sm"">
            <thead>
                <tr>
                    <th style=""vertical-align:middle"">
                        Id
                    </th>
                    <th style=""vertical-align:middle"">
                        Name
                    </th>
                    <th style=""vertical-align:middle"">
                        Role
                    </th>
                    <th style=""vertical-align:middle"">
                        Username
                    </th>
                    <th style=""vertical-align:middle"">
                        Email
                    </th>
                    <th style=""vertical-align:middle"">
                        Email Confirmed
                    </th>
                    <th style=""vertical-align:middle"">
                        Phone Number
   ");
            WriteLiteral("                 </th>\r\n                    <th></th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 40 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 44 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 47 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 50 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Role));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 53 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                       Write(Html.DisplayFor(modelItem => item.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 56 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 59 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                       Write(Html.DisplayFor(modelItem => item.EmailConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 62 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                       Write(Html.DisplayFor(modelItem => item.PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4314887411b0b83e0ea49d614e45dea808c496f97925", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 65 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                                                   WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4314887411b0b83e0ea49d614e45dea808c496f910099", async() => {
                WriteLiteral("Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 66 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                                                      WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                           \r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 70 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
        </table>
    </div>
</div>

<style>

    h1 {
        padding-top: 1em;
        padding-bottom: 10px;
        font-family: 'Playfair Display';
    }

    .table {
        background: #fff;
        overflow-y: auto;
        box-shadow: 0px 10px 50px 0px rgba(0,0,0,0.5);
        border-radius: 5px;
    }

    table {
        width: 100%;
        text-align: center;
        border-collapse: collapse;
    }

        table thead th,
        table tbody td {
            padding: 15px;
            border: none;
            font-weight: 600;
            font-size: 14px;
        }

        table thead th {
            vertical-align:middle;
            background: rgb(33,33,39);
            background: linear-gradient(90deg, rgba(33,33,39,1) 0%, rgba(33,33,39,1) 100%, rgba(33,33,39,1) 100%);
            color: white;
            font-size: 16px;
            position: sticky;
            top: -1%;
            font-family: 'Playfair Display';
      ");
            WriteLiteral(@"  }


        table tbody td {
            border-bottom: 1px solid rgba(0,0,0,0.1)
        }

         .page-item.active .page-link {
            color: #fff !important;
            background-color: #000 !important;
            border-color: #000 !important; 
        }

    .page-link {
        color: #000 !important;
        background-color: #fff !important;
        border: 1px solid #dee2e6 !important; 
    }

    .page-link:hover {
        color: #fff !important;
        background-color: #000 !important;
        border-color: #000 !important; 
    }

</style>


");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\r\n");
#nullable restore
#line 144 "C:\Users\FNAC\source\repos\PwebTP\PwebTP\Views\User\Clients.cshtml"
       await Html.RenderPartialAsync("_ValidationScriptsPartial"); 

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
    <script>
        $(document).ready(function () {

            $('#searchTable').DataTable({

                ""scrollY"": ""450px"",
                ""scrollCollapse"": true,
                ""paging"": true,
                ""ordering"": false

            });

        });


    </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<PwebTP.Models.ApplicationUser>> Html { get; private set; }
    }
}
#pragma warning restore 1591

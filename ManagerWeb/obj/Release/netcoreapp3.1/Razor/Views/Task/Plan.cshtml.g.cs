#pragma checksum "D:\Project\ManagerWeb\ManagerWeb\Views\Task\Plan.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7457a476a9f91720234f0281180f0d37b7867eb0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Task_Plan), @"mvc.1.0.view", @"/Views/Task/Plan.cshtml")]
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
#line 1 "D:\Project\ManagerWeb\ManagerWeb\Views\_ViewImports.cshtml"
using ManagerWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Project\ManagerWeb\ManagerWeb\Views\_ViewImports.cshtml"
using ManagerWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7457a476a9f91720234f0281180f0d37b7867eb0", @"/Views/Task/Plan.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2a2ce5ee53f42c583453120fec9697699466f8c3", @"/Views/_ViewImports.cshtml")]
    public class Views_Task_Plan : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/echarts.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7457a476a9f91720234f0281180f0d37b7867eb03706", async() => {
                WriteLiteral("\r\n    <title></title>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7457a476a9f91720234f0281180f0d37b7867eb03991", async() => {
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
                WriteLiteral("\r\n");
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7457a476a9f91720234f0281180f0d37b7867eb05794", async() => {
                WriteLiteral(@"
    <div id=""main"" style=""width: 1500px;height:600px; position:absolute;top:70px""></div>
    <script type=""text/javascript"">
        var myChart = echarts.init(document.getElementById('main')); 
     
        //指定图表的配置项和数据
        var titles = ['.Net', 'ImageProcess', 'Plan'];
        var builderJson = ");
#nullable restore
#line 13 "D:\Project\ManagerWeb\ManagerWeb\Views\Task\Plan.cshtml"
                     Write(Html.Raw(ViewData["builderjson"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(@";        
        

        var option = {
                        //分别设置标题居中主要代码
            title:[
                {
                    text:titles[0],
                    left:'25%',
                    top:'1%',
                    textAlign:'center'
                },
                {
                    text:titles[1],
                    left:'73%',
                    top:'1%',
                    textAlign:'center'
                },
                {
                    text:titles[2],
                    left:'25%',
                    top:'50%',
                    textAlign:'center'
                }
        
            ],
            grid:[
                {x:'7%',y:'7%',width:'38%',height:'38%'},
                {x2:'7%',y:'7%',width:'38%',height:'38%'},
                {x:'7%',y2:'7%',width:'85%',height:'38%'}
            ],

            xAxis: [{
                type: 'value',
                max: builderJson.all,
                splitLine: {
          ");
                WriteLiteral(@"          show: false
                }
            }, {
                type: 'value',
                max: builderJson.all,
                gridIndex: 1,
                splitLine: {
                    show: false
                },
            },{
                type: 'value',
                max: builderJson.all,
                gridIndex: 2,
                splitLine: {
                    show: false
                },
            }],
            yAxis: [{
                type: 'category',
                gridIndex: 0,
                data: Object.keys(builderJson.donet),
                axisLabel: {
                    interval: 0,
                    rotate: 30
                },
                splitLine: {
                    show: false
                }
            }, {
                gridIndex: 1,
                type: 'category',
                data: Object.keys(builderJson.imageprocess),
                axisLabel: {
                    interval: 0,
        ");
                WriteLiteral(@"            rotate: 30
                },
                splitLine: {
                    show: false
                }
            },{
                gridIndex: 2,
                type: 'category',
                data: Object.keys(builderJson.plan),
                axisLabel: {
                    interval: 0,
                    rotate: 30
                },
                splitLine: {
                    show: false
                }
            }],
            series: [{
                type: 'bar',
                stack: 'donet',
                z: 3,
                label: {
                    normal: {
                        position: 'right',
                        show: true
                    }
                },
                data: Object.keys(builderJson.donet).map(function (key) {
                    return builderJson.donet[key];
                })
            }, {
                type: 'bar',
                stack: 'donet',
                silent: true");
                WriteLiteral(@",
                itemStyle: {
                    normal: {
                        color: '#eee'
                    }
                },
                data: Object.keys(builderJson.donet).map(function (key) {
                    return builderJson.all - builderJson.donet[key];
                })
            }, {
                type: 'bar',
                stack: 'imageprocess',
                xAxisIndex: 1,
                yAxisIndex: 1,
                z: 3,
                label: {
                    normal: {
                        position: 'right',
                        show: true
                    }
                },
                data: Object.keys(builderJson.imageprocess).map(function (key) {
                    return builderJson.imageprocess[key];
                })
            }, {
                type: 'bar',
                stack: 'imageprocess',
                silent: true,
                xAxisIndex: 1,
                yAxisIndex: 1,
              ");
                WriteLiteral(@"  itemStyle: {
                    normal: {
                        color: '#eee'
                    }
                },
                data: Object.keys(builderJson.imageprocess).map(function (key) {
                    return builderJson.all - builderJson.imageprocess[key];
                })
            },{
                type: 'bar',
                stack: 'plan',
                xAxisIndex: 2,
                yAxisIndex: 2,
                z: 3,
                label: {
                    normal: {
                        position: 'right',
                        show: true
                    }
                },
                data: Object.keys(builderJson.plan).map(function (key) {
                    return builderJson.plan[key];
                })
            }, {
                type: 'bar',
                stack: 'plan',
                silent: true,
                xAxisIndex: 2,
                yAxisIndex: 2,
                itemStyle: {
                    ");
                WriteLiteral(@"normal: {
                        color: '#eee'
                    }
                },
                data: Object.keys(builderJson.plan).map(function (key) {
                    return builderJson.all - builderJson.plan[key];
                })
            }]
        };
        //显示图表
        myChart.setOption(option);
    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
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
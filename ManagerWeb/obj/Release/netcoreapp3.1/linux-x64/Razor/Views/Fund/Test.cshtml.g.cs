#pragma checksum "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7b9201b592515aa8138d4eb47cff69a1e8e9a9d4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Fund_Test), @"mvc.1.0.view", @"/Views/Fund/Test.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7b9201b592515aa8138d4eb47cff69a1e8e9a9d4", @"/Views/Fund/Test.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2a2ce5ee53f42c583453120fec9697699466f8c3", @"/Views/_ViewImports.cshtml")]
    public class Views_Fund_Test : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#nullable restore
#line 1 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
  
    Layout = null;
    ViewData["Title"] = "Details";
    

#line default
#line hidden
#nullable disable
            WriteLiteral("<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7b9201b592515aa8138d4eb47cff69a1e8e9a9d43869", async() => {
                WriteLiteral("\r\n    <meta charset=\"utf-8\">\r\n    <title>");
#nullable restore
#line 9 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
      Write(ViewData["FundName"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</title>\r\n    <!-- 引入 echarts.js -->\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7b9201b592515aa8138d4eb47cff69a1e8e9a9d44423", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7b9201b592515aa8138d4eb47cff69a1e8e9a9d46226", async() => {
                WriteLiteral(@"
    <div style=""text-align:center"">
        <table border=""1"" cellspacing=""0"" cellpadding=""0"" style=""position:relative;top:15px;left:150px"">
            <tr><td>规模</td><td>手续费</td><td>非散率</td><td>持股率</td><td>任职时间</td><td>近1天</td><td>近1周</td><td>近1月</td><td>近3月</td><td>近6月</td><td>近1年</td><td>近2年</td><td>近3年</td><td>近5年</td><td>Linear1</td><td>Linear2</td><td>Linear3</td><td>Linear5</td><td>30天赎回</td><td>1年赎回</td></tr>
            <tr>
                <td>");
#nullable restore
#line 18 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
               Write(ViewData["TotalScale"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 19 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
               Write(ViewData["TotalFee"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 20 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
               Write(ViewData["OrgHoldRate"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 21 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
               Write(ViewData["StockRate"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 22 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
               Write(ViewData["DutyDate"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 1017, "\"", 1085, 2);
                WriteAttributeValue("", 1025, "color:", 1025, 6, true);
#nullable restore
#line 23 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 1031, Convert.ToDouble(ViewData["R1day"])>0?"red":"green", 1031, 54, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 23 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                    Write(ViewData["R1day"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 1142, "\"", 1211, 2);
                WriteAttributeValue("", 1150, "color:", 1150, 6, true);
#nullable restore
#line 24 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 1156, Convert.ToDouble(ViewData["R1week"])>0?"red":"green", 1156, 55, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 24 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                     Write(ViewData["R1week"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 1269, "\"", 1339, 2);
                WriteAttributeValue("", 1277, "color:", 1277, 6, true);
#nullable restore
#line 25 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 1283, Convert.ToDouble(ViewData["R1month"])>0?"red":"green", 1283, 56, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 25 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                      Write(ViewData["R1month"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 1398, "\"", 1468, 2);
                WriteAttributeValue("", 1406, "color:", 1406, 6, true);
#nullable restore
#line 26 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 1412, Convert.ToDouble(ViewData["R3month"])>0?"red":"green", 1412, 56, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 26 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                      Write(ViewData["R3month"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 1527, "\"", 1597, 2);
                WriteAttributeValue("", 1535, "color:", 1535, 6, true);
#nullable restore
#line 27 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 1541, Convert.ToDouble(ViewData["R6month"])>0?"red":"green", 1541, 56, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 27 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                      Write(ViewData["R6month"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 1656, "\"", 1725, 2);
                WriteAttributeValue("", 1664, "color:", 1664, 6, true);
#nullable restore
#line 28 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 1670, Convert.ToDouble(ViewData["R1year"])>0?"red":"green", 1670, 55, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 28 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                     Write(ViewData["R1year"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 1783, "\"", 1852, 2);
                WriteAttributeValue("", 1791, "color:", 1791, 6, true);
#nullable restore
#line 29 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 1797, Convert.ToDouble(ViewData["R2year"])>0?"red":"green", 1797, 55, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 29 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                     Write(ViewData["R2year"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 1910, "\"", 1979, 2);
                WriteAttributeValue("", 1918, "color:", 1918, 6, true);
#nullable restore
#line 30 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 1924, Convert.ToDouble(ViewData["R3year"])>0?"red":"green", 1924, 55, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 30 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                     Write(ViewData["R3year"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 2037, "\"", 2106, 2);
                WriteAttributeValue("", 2045, "color:", 2045, 6, true);
#nullable restore
#line 31 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 2051, Convert.ToDouble(ViewData["R5year"])>0?"red":"green", 2051, 55, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 31 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                     Write(ViewData["R5year"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 2164, "\"", 2234, 2);
                WriteAttributeValue("", 2172, "color:", 2172, 6, true);
#nullable restore
#line 32 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 2178, Convert.ToDouble(ViewData["Linear1"])>0?"red":"green", 2178, 56, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 32 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                      Write(ViewData["Linear1"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 2293, "\"", 2363, 2);
                WriteAttributeValue("", 2301, "color:", 2301, 6, true);
#nullable restore
#line 33 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 2307, Convert.ToDouble(ViewData["Linear2"])>0?"red":"green", 2307, 56, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 33 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                      Write(ViewData["Linear2"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 2422, "\"", 2492, 2);
                WriteAttributeValue("", 2430, "color:", 2430, 6, true);
#nullable restore
#line 34 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 2436, Convert.ToDouble(ViewData["Linear3"])>0?"red":"green", 2436, 56, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 34 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                      Write(ViewData["Linear3"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td");
                BeginWriteAttribute("style", " style=\"", 2551, "\"", 2621, 2);
                WriteAttributeValue("", 2559, "color:", 2559, 6, true);
#nullable restore
#line 35 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 2565, Convert.ToDouble(ViewData["Linear5"])>0?"red":"green", 2565, 56, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 35 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                      Write(ViewData["Linear5"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 36 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
               Write(ViewData["Over30d"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td><a");
                BeginWriteAttribute("href", " href=", 2741, "", 2825, 1);
#nullable restore
#line 37 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
WriteAttributeValue("", 2747, "http://fundf10.eastmoney.com/jjfl_"+@ViewData["FundNo"].ToString()+".html", 2747, 78, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" target=\"blank\">");
#nullable restore
#line 37 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
                                                                                                                     Write(ViewData["Over1yFee"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("</a></td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n    <!-- 为ECharts准备一个具备大小（宽高）的Dom -->\r\n    <div id=\"main\" style=\"width: 1500px;height:600px; position:absolute;top:70px\" ></div>\r\n    <script type=\"text/javascript\">\r\n        //dataX = ");
#nullable restore
#line 44 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
             Write(Html.Raw(ViewData["xStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        dataY = ");
#nullable restore
#line 45 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
           Write(Html.Raw(ViewData["yStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        dataYV1 = ");
#nullable restore
#line 46 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
             Write(Html.Raw(ViewData["y1ValueStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        dataYV2 = ");
#nullable restore
#line 47 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
             Write(Html.Raw(ViewData["y2ValueStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        dataYV3 = ");
#nullable restore
#line 48 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
             Write(Html.Raw(ViewData["y3ValueStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        dataYV4 = ");
#nullable restore
#line 49 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
             Write(Html.Raw(ViewData["y4ValueStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        dataYVM1 = ");
#nullable restore
#line 50 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
              Write(Html.Raw(ViewData["m1ValueStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        dataYVM2 = ");
#nullable restore
#line 51 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
              Write(Html.Raw(ViewData["m2ValueStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        dataYVD10 = ");
#nullable restore
#line 52 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
               Write(Html.Raw(ViewData["d10ValueStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        dataYVD5 = ");
#nullable restore
#line 53 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
              Write(Html.Raw(ViewData["d5ValueStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        fundName = \"");
#nullable restore
#line 54 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
               Write(ViewData["FundName"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("\";\r\n        fundNo = \"");
#nullable restore
#line 55 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
             Write(ViewData["FundNo"].ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral("\";\r\n        dataPoint = ");
#nullable restore
#line 56 "D:\Project\ManagerWeb\ManagerWeb\Views\Fund\Test.cshtml"
               Write(Html.Raw(ViewData["dataPointStr"].ToString()));

#line default
#line hidden
#nullable disable
                WriteLiteral(@";
        /*此段js来自百度Echart.js插件*/
        var dom = document.getElementById(""main"");
                    var myChart = echarts.init(dom);
                    var app = {};
                    option = null;
        option = {
            title: {
                text: fundNo+unescape(fundName.replace(/&#x/g,'%u').replace(/;/g,'')),
                subtext: '' 
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['净值','一年趋势','二年趋势','三年趋势','五年趋势','1月趋势','2月趋势','10日趋势','5日趋势']
            },
            toolbox: {
                show: true,
                feature: {
                    mark: { show: true },
                    dataView : {show: true, readOnly: false},     //数据视图按钮
                    magicType: { show: true, type: ['line', 'bar'] },
                    restore : {show: true},       //图表右上角刷新按钮
                    saveAsImage: { show: true }
                }
            },
            calcul");
                WriteLiteral(@"able: true,
            xAxis: [
                {
                    type: 'time',
                    boundaryGap: false,

                    //data: dataX
                }
            ],
            yAxis: [
                {
                    scale: true,
                    type: 'value',
                    axisLabel: {
                        formatter: function(v){
                            return v.toFixed(2);
                        }
                    }
                }
            ],
            dataZoom: [
                {   // 这个dataZoom组件，默认控制x轴。
                    type: 'slider', // 这个 dataZoom 组件是 slider 型 dataZoom 组件
                    start: 82,      // 左边在 10% 的位置。
                    end: 100         // 右边在 60% 的位置。
                }
            ],
            series: [
                {
                    name: '净值',
                    type: 'line',
                    symbol: ""none"",
                    symbolSize: 30,
                    ");
                WriteLiteral(@"data: dataY,
                    markPoint: {
                        //itemStyle: {
                        //    color: '#00CD68'
                        //},
                        symbolSize:50,
                        data: dataPoint
                    }//,
                    //markLine:{
                    //    data: [{ type: 'max', name: '最大值' }],
                    //    lineStyle:
                    //}

                },
                {
                    name: '一年趋势',
                    type: 'line',
                    symbol: ""none"",
                    data: dataYV1
                }
                ,
                {
                    name: '二年趋势',
                    type: 'line',
                    symbol: ""none"",
                    data: dataYV2
                }
                ,
                {
                    name: '三年趋势',
                    type: 'line',
                    symbol: ""none"",
                    data: dataYV3
        ");
                WriteLiteral(@"        }
                ,
                {
                    name: '五年趋势',
                    type: 'line',
                    symbol: ""none"",
                    data: dataYV4
                }
                ,
                {
                    name: '1月趋势',
                    type: 'line',
                    symbol: ""none"",
                    data: dataYVM1
                }
                ,
                {
                    name: '2月趋势',
                    type: 'line',
                    symbol: ""none"",
                    data: dataYVM2
                }
                ,
                {
                    name: '10日趋势',
                    type: 'line',
                    symbol: ""none"",
                    data: dataYVD10
                }
                ,
                {
                    name: '5日趋势',
                    type: 'line',
                    symbol: ""none"",
                    data: dataYVD5
                }
            ");
                WriteLiteral(@"]
        };

        ;
        if (option && typeof option === ""object"") {
            myChart.setOption(option, true);
        }

        myChart.on('click', { seriesName: '净值' }, function () {
            option.series[0].markPoint.data=null;
            myChart.setOption(option, true);
         });

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
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PagePrint.aspx.cs" Inherits="PagePrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .style1 {
            height: 18px;
        }

        .style {
            width: 100px;
            height: 30px;
        }

        .style4 {
            width: 100px;
        }
    </style>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=3GAwU9zGrWoSgINGo631G6NV3dLwrpnF"></script>

    <script language="javascript" type="text/javascript">
        function printme() {
            document.body.innerHTML = document.getElementById('DivPrint').innerHTML;
            window.print();
        }
    </script>

</head>
<body onload="theLocation()">
    <form id="form" runat="server">
        <div style="margin-left: auto; margin-right: auto; width: 1100px">
            <div style="height: 10px"></div>
            <div>
                <img src="Images/logo.png" />
            </div>
            <div style="height: 10px"></div>
            <div align="center">
                <a href="javascript:window.printme()">打印报表</a>
                &nbsp;|&nbsp;
                <input type="button" onclick="return exportToWord('DivPrint')"
                    value="导出页面" />
                &nbsp;|&nbsp;
                <a href="javascript:window.opener=null; window.open('','_self'); window.close();">关闭窗口</a>
                &nbsp;|&nbsp;
                <a href="Index.aspx">返回主页</a>
            </div>
            <script src="JS/jquery-word.min.js" type="text/javascript"></script>
            <script src="JS/jquery.wordexport.js" type="text/javascript"></script>
            <script src="JS/FileSaver.js" type="text/javascript"></script>
            <script type="text/javascript" language="javascript">
                function exportToWord(controlId) {
                    var control = document.getElementById(controlId);
                    try {
                        var oWD = new ActiveXObject("Word.Application");
                        var oDC = oWD.Documents.Add("", 0, 1);
                        var oRange = oDC.Range(0, 1);
                        var sel = document.body.createTextRange();
                        try {
                            sel.moveToElementText(control);
                        } catch (notE) {
                            alert("导出数据失败，没有数据可以导出。");
                            window.close();
                            return;
                        }
                        sel.select();
                        sel.execCommand("Copy");
                        oRange.Paste();
                        oWD.Application.Visible = true;
                    }
                    catch (e) {
                        alert("导出数据失败，需要在客户机器安装Microsoft Office Word(不限版本)，将当前站点加入信任站点，允许在IE中运行ActiveX控件。");
                        try { oWD.Quit(); } catch (ex) { }
                    }
                }

            </script>
            <div id="DivPrint" style="margin-left: auto; margin-right: auto; width: 900px">
                <div style="height: 7px"></div>
                <p align="center" style="font-size: 28px">超声波局部放电检验报告</p>
                <table id="test" width="100%" border="1" cellspacing="0" cellpadding="0" style="border: solid #C0C0C0 0px">
                    <tr align="center">
                        <td style="width: 100px; height: 24px">检测日期</td>
                        <td colspan="3">
                            <asp:Label ID="lblCheckTime" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">检测人员</td>
                        <td colspan="3">
                            <asp:Label ID="lblCheckName" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td style="width: 100px; height: 24px">线路名称</td>
                        <td colspan="3">
                            <asp:Label ID="lblLineName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">线路编号</td>
                        <td colspan="3">
                            <asp:Label ID="lblLineNO" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td style="width: 100px; height: 24px">设备种类</td>
                        <td colspan="3">
                            <asp:Label ID="lblProductType" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">缺陷程度</td>
                        <td colspan="3">
                            <asp:Label ID="lblDefectLevel" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td style="width: 100px; height: 24px">距离(m)</td>
                        <td style="width: 100px">
                            <asp:Label ID="lblDistance" runat="server" Text="Label"></asp:Label>
                        </td>

                        <td style="width: 100px; height: 24px">温度(℃)</td>
                        <td style="width: 100px">
                            <asp:Label ID="lblTemperature" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">湿度(%)</td>
                        <td style="width: 100px">
                            <asp:Label ID="lblHumidity" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">地域特征</td>
                        <td></td>
                    </tr>
                    <tr align="center">
                        <td style="width: 100px; height: 24px">经度</td>
                        <td style="width: 100px">
                            <asp:Label ID="lblLongitude" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">纬度</td>
                        <td style="width: 100px">
                            <asp:Label ID="lblLatitude" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">最大 dB 值</td>
                        <td style="width: 100px">
                            <asp:Label ID="lblMaxdB" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">平均 dB 值</td>
                        <td style="width: 100px">
                            <asp:Label ID="lblAvgdB" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td style="width: 100px; height: 24px">诊断建议</td>
                        <td colspan="7">
                            <asp:Label ID="lblSuggestion" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>幅值曲线</td>
                        <td colspan="7" style="width:800px; height: 220px">
                            <p></p>
                            <img src="DrawPicture.aspx" style="width:790px;  height: 200px" />
                        </td>
                    </tr>
                    <tr align="center" style="height: 24px">
                        <td colspan="4">杆塔编号照片</td>
                        <td colspan="4">杆塔全景照片</td>
                    </tr>
                    <tr align="center" style="height: 300px">

                        <td colspan="4">
                            <img src='<%= Session["PoleNOPicPath"] %>' style="width: 380px; height: 280px" />
                        </td>
                        <td colspan="4">
                            <img src='<%= Session["WholePicPath"] %>' style="width: 380px; height: 280px" />
                        </td>
                        <%--<td colspan="2">
                            <img src='<%= Session["WholePicPath"] %>' style="width: 320px; height: 280px" />
                        </td>
                        <td colspan="2">
                            <img src='<%= Session["BadProductPicPath"] %>' style="width: 320px; height: 280px" />
                        </td>--%>
                    </tr>
                    <tr align="center" style="height: 24px">
                        <td colspan="4">缺陷局部照片</td>
                        <td colspan="4">检测位置信息</td>
                    </tr>
                    <tr align="center" style="height: 300px">

                        <td colspan="4">
                            <img src='<%= Session["BadProductPicPath"] %>' style="width: 380px; height: 280px" />
                        </td>
                        <td colspan="4" style="width: 400px; height: 300px">
                            <div id="allmap" style="width: 400px; height: 300px"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 10px"></div>
            <div style="background-color: #E2E9EF">
                <div style="margin-left: auto; margin-right: auto; width: 900px">
                    <div style="font-family: Arial, Helvetica, sans-serif; font-size: 12px">
                        <p>销售电话：0371-56977880/56977890/63812255&nbsp;&nbsp;&nbsp;&nbsp;售后电话：0371-56977875/56977873</p>
                        <p>传真：0371-63813838&nbsp;&nbsp;&nbsp;&nbsp;QQ：613712485/613712493&nbsp;&nbsp;&nbsp;&nbsp;邮箱：hnhbcom@163.com</p>
                        <p>地址：郑州市金水区丰产路&nbsp;21&nbsp;号&nbsp;SOHO&nbsp;世纪城东塔&nbsp;8&nbsp;楼&nbsp;C&nbsp;座</p>
                        <p>Copyright&nbsp;2009-2017&nbsp;www.hnhbdq.com    All&nbsp;rights&nbsp;reserved.&nbsp;&nbsp;&nbsp;&nbsp;河南宏博测控技术有限公司&nbsp;版权所有</p>
                        <p>豫&nbsp;ICP&nbsp;备&nbsp;09018549&nbsp;号</p>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
                //百度地图 API 功能
                var map = new BMap.Map("allmap");
                map.centerAndZoom(new BMap.Point(113.6747, 34.7896), 13);
                map.enableScrollWheelZoom(true);
                //用经纬度设置地图中心点
                function theLocation() {
                    var new_point = new BMap.Point(<%= Session["LongitudePrint"]%>, <%= Session["LatitudePrint"]%>);
                    //创建标注
                    var marker = new BMap.Marker(new_point);
                    //将标注添加到地图中
                    map.addOverlay(marker);
                    map.panTo(new_point);
                }
</script>


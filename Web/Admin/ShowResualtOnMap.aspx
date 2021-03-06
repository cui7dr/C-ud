﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowResualtOnMap.aspx.cs" Inherits="ShowResualtOnMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <style type="text/css">
        <!-- body, html {
            height: 80%;
            margin: 0;
            font-family: "微软雅黑";
        }

        #allmap {
            width: 100%;
            height: 600px;
        }

        p {
            margin-left: 5px;
            font-size: 14px;
        }
    </style>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SGX8c14ztPo2OlXS2SykEBBA"></script>
    <script src="../JS/jquery.js" type="text/javascript"></script>
    <title>位置信息展示</title>
</head>
<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="margin-left: auto; margin-right: auto; margin-top: 2%; width: 80%; height: 80%;">
            <p style="font-size: large; color: Blue">位置信息展示</p>
            <p id="select">
                按条件查询
                <asp:DropDownList ID="ddlSelectItem" runat="server">
                    <asp:ListItem>所有数据</asp:ListItem>
                    <asp:ListItem>故障数据</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
    <asp:Button ID="btnSearch" runat="server" Text="搜索" Height="25px" Width="55px" BorderColor="White" ForeColor="#0066CC" OnClick="btnSearch_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="返回" Height="25px" Width="55px"
                    OnClick="Button1_Click" />
            </p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div id="allmap"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //百度地图API功能
    var map = new BMap.Map("allmap");
    var point = new BMap.Point(113.6746, 34.7896);
    map.centerAndZoom(point, 8);
    //启用滚轮放大缩小  
    map.enableScrollWheelZoom(true);
    // 编写自定义函数,创建标注

    //字符串、经度
    var lon = "<%=Session["Longitude"] %>";
    //字符串、纬度
    var lat = "<%=Session["Latitude"] %>";
    //字符串、公司名称
    var com = "<%=Session["ComPanyName"] %>";

    var comarr = new Array();
    //存储到数组
    comarr = com.split(","); 
    var lonarr = new Array();
    lonarr = lon.split(",");
    var latarr = new Array();
    latarr = lat.split(","); 

    var opts = {
        //信息窗口宽度
        width: 250,
        //信息窗口高度
        height: 60,
        //信息窗口标题
        title: "公司名称", 
        //设置允许信息窗发送短息
        enableMessage: true
    };

    for (var i = 0; i < lonarr.length; i++) {
        //创建标注
        var marker = new BMap.Marker(new BMap.Point(lonarr[i], latarr[i]));
        var content = comarr[i];
        //将标注添加到地图中
        map.addOverlay(marker);
        addClickHandler(content, marker);
    }

    function addClickHandler(content, marker) {
        marker.addEventListener("click", function (e) {
            openInfo(content, e)
        }
        );
    }

    function openInfo(content, e) {
        var p = e.target;
        var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
        //创建信息窗口对象 
        var infoWindow = new BMap.InfoWindow(content, opts);
        //开启信息窗口
        map.openInfoWindow(infoWindow, point);
    }
</script>

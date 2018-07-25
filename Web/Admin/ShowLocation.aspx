<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowLocation.aspx.cs" Inherits="ShowLocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>地图展示</title>
    <style type="text/css"> 
        body, html {
            width: 100%;
            height: 100%;
            margin: 0;
            font-family: "微软雅黑"
        }

        #allmap {
            height: 70%;
            width: 80%
        }

        #r-result {
            width: 100%;
            font-size: 14px
        }
    </style>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SGX8c14ztPo201XS2SykEBBA"></script>
</head>
<body onload="theLocation()">
    <form id="form1" runat="server">
        <div style="height: 15px"></div>
        <div id="allmap" style="position: absolute; top: 10%; left: 10%"></div>
        <br />
        <br />
    </form>
</body>
</html>
<script type="text/javascript">
    //百度地图 API 功能
    var map = new BMap.Map("allmap");
    map.centerAndZoom(new BMap.Point(113.5747, 34.7896), 13);
    map.enableScrollWheelZoom(true);

    //用经纬度设置地图中心点
    function theLocation() {
        var new_point = new BMap.Point(<%=Session["Longitude"]%>,<%=Session["Latitude"]%>);
        //创建标注
        var marker = new BMap.Marker(new_point);
        //将标注添加到地图中
        map.addOverlay(marker);
        map.panTo(new_point);
    }
</script>

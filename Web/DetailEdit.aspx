<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailEdit.aspx.cs" Inherits="DetailEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>信息编辑</title>
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script src="JS/jquery.datetimepicker.js" type="text/javascript"></script>
    <link href="CSS/jquery.datetimepicker.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
        }

        .style3 {
            height: 21px;
        }

        .style4 {
            height: 14px;
        }

        .style5 {
            width: 120px;
        }

        .style6 {
            width: 121px;
            height：16px;
        }

        .style7 {
            width: 121px;
            height: 21px;
        }
    </style>
    <!-- 电杆编号照片上传预览功能 -->
    <script type="text/javascript">
        function setImagePreviemFileNOPic(avalue) {
            var docObj = document.getElementById("FileNOPic");
            var imgObjPreview = document.getElementById("preview");
            if (docObj.files && docObj.files[0]) {
                //火狐下，直接设置属性
                imgObjPreview.style.display = "block";
                imgObjPreview.style.width = "200px";
                imgObjPreview.style.width = "180px";
                //火狐 7 以下版本
                //imgObjPreview.src = docObj.files[0].getAsDataURL();
                //火狐 7 以上版本
                imgObjPreview.src = window.URL.createObjectURL(docObj.files[0]);
            }
            else {
                // IE 下，使用滤镜
                docObj.select();
                var imgSrc = document.selection.createRange().text;
                var localImgId = document.getElementById("localImg");
                //必须设置初始大小
                localImgId.style.width = "200px";
                localImgId.style.height = "180px";
                //图片异常的捕捉，防止用户恶意修改后缀来伪造图片
                try {
                    localImgId.style.filter = "progid: DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                    localImgId.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
                }
                catch (e) {
                    alert("您上传的图片格式不正确，请重新选择再上传！");
                    return false;
                }
                imgObjPreview.style.display = "none";
                document.selection.empty();
            }
            return true;
        }
    </script>
    <!-- 整体照片上传预览功能 -->
    <script type="text/javascript">
        function setImagePreviemFileFullPic(avalue) {
            var docObj = document.getElementById("FileFullPic");
            var imgObjPreview = document.getElementById("preview");
            if (docObj.files && docObj.files[0]) {
                //火狐下，直接设置属性
                imgObjPreview.style.display = "block";
                imgObjPreview.style.width = "200px";
                imgObjPreview.style.width = "180px";
                //火狐 7 以上版本
                imgObjPreview.src = window.URL.createObjectURL(docObj.files[0]);
            }
            else {
                docObj.select();
                var imgSrc = document.selection.createRange().text;
                var localImgId = document.getElementById("localImg");
                localImgId.style.width = "200px";
                localImgId.style.height = "180px";
                try {
                    localImgId.style.filter = "progid: DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                    localImgId.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
                }
                catch (e) {
                    alert("您上传的图片格式不正确，请重新选择再上传！");
                    return false;
                }
                imgObjPreview.style.display = "none";
                document.selection.empty();
            }
            return true;
        }
    </script>
    <!-- 不良照片上传预览功能 -->
    <script type="text/javascript">
        function setImagePreviemFileBadPic(avalue) {
            var docObj = document.getElementById("FileBadPic");
            var imgObjPreview = document.getElementById("preview");
            if (docObj.files && docObj.files[0]) {
                imgObjPreview.style.display = "block";
                imgObjPreview.style.width = "200px";
                imgObjPreview.style.width = "180px";
                imgObjPreview.src = window.URL.createObjectURL(docObj.files[0]);
            }
            else {
                docObj.select();
                var imgSrc = document.selection.createRange().text;
                var localImgId = document.getElementById("localImg");
                localImgId.style.width = "200px";
                localImgId.style.height = "180px";
                try {
                    localImgId.style.filter = "progid: DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                    localImgId.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
                }
                catch (e) {
                    alert("您上传的图片格式不正确，请重新选择再上传！");
                    return false;
                }
                imgObjPreview.style.display = "none";
                document.selection.empty();
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
        <div style="margin-left: auto; margin-right: auto; width: 1004px">
            <div>
                <img src="Images/logo.png" />
            </div>
            <div style="height: 20px"></div>
            <div>
                <div style="margin-left: auto; margin-right: auto; width: 1000px">
                    <div>
                        <h2 align="center">用户检测报告</h2>
                        <br />
                        <table id="Table1" runat="server" cellpadding="0" cellspacing="0" class="style1" style="border-color: #000000; border-width: thin; font-size: small; color: #000000; line-height; 25px">
                            <tr>
                                <td class="style3" style="font-size: small; text-align: center">线路名称</td>
                                <td>
                                    <asp:TextBox ID="txtLineName" runat="server" Width="210px"></asp:TextBox></td>
                                <td class="style3" style="font-size: small; text-align: center">线路等级</td>
                                <td style="height: 21px">
                                    <asp:DropDownList ID="ddlDistance" runat="server" Height="21px" Width="220px">
                                        <asp:ListItem Value="12">10 kV</asp:ListItem>
                                        <asp:ListItem Value="12">25 kV</asp:ListItem>
                                        <asp:ListItem Value="12">35 kV</asp:ListItem>
                                        <asp:ListItem Value="12" Selected="True">110 kV</asp:ListItem>
                                        <asp:ListItem Value="12">220 kV</asp:ListItem>
                                        <asp:ListItem Value="12">500 kV</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; text-align: center">线路编号</td>
                                <td>
                                    <asp:TextBox ID="txtLineNO" runat="server" Width="210px"></asp:TextBox></td>
                                <td class="style5" style="font-size: small; text-align: center">设备状态</td>
                                <td style="height: 21px">
                                    <asp:DropDownList ID="ddlProductState" runat="server" Height="21px" Width="220px">
                                        <asp:ListItem Value="裂纹">裂纹</asp:ListItem>
                                        <asp:ListItem Value="侵蚀">侵蚀</asp:ListItem>
                                        <asp:ListItem Value="碳化">碳化</asp:ListItem>
                                        <asp:ListItem Value="击穿">击穿</asp:ListItem>
                                        <asp:ListItem Value="变位">变位</asp:ListItem>
                                        <asp:ListItem Value="腐蚀">腐蚀</asp:ListItem>
                                        <asp:ListItem Value="污秽">污秽</asp:ListItem>
                                        <asp:ListItem Value="剥离">剥离</asp:ListItem>
                                        <asp:ListItem Value="良好" Selected="True">良好</asp:ListItem>
                                        <asp:ListItem Value="无光泽">无光泽</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; text-align: center">设备种类</td>
                                <td style="height: 21px">
                                    <asp:DropDownList ID="ddlProductType" runat="server" Height="22px" Width="214px">
                                        <asp:ListItem Value="开关">开关</asp:ListItem>
                                        <asp:ListItem Value="重合器">重合器</asp:ListItem>
                                        <asp:ListItem Value="避雷器">避雷器</asp:ListItem>
                                        <asp:ListItem Value="变压器">变压器</asp:ListItem>
                                        <asp:ListItem Value="隔离刀闸">隔离刀闸</asp:ListItem>
                                        <asp:ListItem Value="电缆终端盒">电缆终端盒</asp:ListItem>
                                        <asp:ListItem Value="悬式绝缘子">悬式绝缘子</asp:ListItem>
                                        <asp:ListItem Value="柱式绝缘子" Selected="True">柱式绝缘子</asp:ListItem>
                                        <asp:ListItem Value="跌落式熔断器">跌落式熔断器</asp:ListItem>
                                        <asp:ListItem Value="电缆线夹绝缘套">电缆线夹绝缘套</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="style5" style="font-size: small; text-align: center">检测人员</td>
                                <td>
                                    <asp:TextBox ID="txtCheckName" runat="server" Width="216px" Height="16px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="style2" style="font-size: small; text-align: center">诊断建议</td>
                                <td class="style2" colspan="3">
                                    <asp:TextBox ID="txtSuggestion" runat="server" Height="16px" Width="210px"></asp:TextBox></td>
                                <%-- --<td class="style6" style="font-size: small; text-align: center">&nbsp;</td>
                                <td class="style2">&nbsp;</td>--%>
                            </tr>
                            <tr>
                                <td class="style2" style="font-size:small; text-align:center">&nbsp;</td>
                                <td class="style2">&nbsp;</td>
                                <td class="style6" style="font-size:small; text-align:center">&nbsp;</td>
                                <td class="style2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style2" style="font-size:small; text-align:center">&nbsp;</td>
                                <td class="style2">&nbsp;</td>
                                <td class="style6" style="font-size:small; text-align:center">&nbsp;</td>
                                <td class="style2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style2" style="font-size:small; text-align:center" colspan="2">&nbsp;</td>
                                <td class="style2" style="font-size:small; text-align:center" colspan="2" rowspan="5">
                                    <div id="localImg">
                                        <img id="preview" src="Images/ImagePreview.jpg" width="200" height="180" style="display:block; width:200px; height:180px" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2" style="font-size:small; text-align:center">编号照片</td>
                                <td class="style2">
                                    <input type="file" name="file" id="FileNOPic" accept="Image/*" runat="server" onchange="javascript:setImagePreviewFileNOPic();" />
                                    <asp:Button ID="btnNOPic" runat="server" Text="上传" OnClick="btnNOPic_Click" />
                                    <asp:Label ID="lblNOPic" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2" style="font-size:small; text-align:center">整体照片</td>
                                <td class="style2">
                                    <input type="file" name="Pic" id="FileFullPic" accept="Image/*" runat="server" onchange="javascript:setImagePreviewFileFullPic();" />
                                    <asp:Button ID="btnFullPic" runat="server" Text="上传" OnClick="btnFullPic_Click" />
                                    <asp:Label ID="lblFullPic" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2" style="font-size:small; text-align:center">不良照片</td>
                                <td class="style2">
                                    <input type="file" name="Pic" id="FileBadPic" accept="Image/*" runat="server" onchange="javascript:setImagePreviewFileBadPic();" />
                                    <asp:Button ID="btnBadPic" runat="server" Text="上传" OnClick="btnBadPic_Click" />
                                    <asp:Label ID="lblBadPic" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                 </td>
                            </tr>
                            <tr>
                                <td class="style4" style="font-size:small; text-align:center" colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style3" style="font-size:small; text-align:center">&nbsp;</td>
                                <td class="style3">&nbsp;</td>
                                <td class="style7" style="font-size:small; text-align:center">&nbsp;</td>
                                <td class="style3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style3" style="font-size:small; text-align:center">检测日期</td>
                                <td class="style3">
                                    <input id="datetimepickerEnd" style="width:210px" type="text" runat="server" />
                                </td>
                                <td class="style7" style="font-size:small; color:red; text-align:center">输入日期</td>
                                <td class="style3">
                                    <input id="datetimepickerStart" style="width:210px" type="text" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style3" style="font-size:small; text-align:center">频率</td>
                                <td class="style3">
                                    <asp:TextBox ID="txtFrequency" runat="server" Width="210px"></asp:TextBox>
                                </td>
                                <td class="style5" style="font-size:small; text-align:center">管理编号</td>
                                <td class="style3">
                                    <asp:TextBox ID="txtManagerNO" runat="server" Width="210px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3" style="font-size:small; text-align:center">温度(℃)</td>
                                <td class="style3">
                                    <asp:TextBox ID="txtTemperature" runat="server" Width="210px"></asp:TextBox>
                                </td>
                                <td class="style5" style="font-size:small; text-align:center">湿度(%)</td>
                                <td class="style3">
                                    <asp:TextBox ID="txtHumidity" runat="server" Width="210px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3" style="font-size:small; text-align:center">经度</td>
                                <td class="style3">
                                    <asp:TextBox ID="txtLongitude" runat="server" Width="210px"></asp:TextBox>
                                </td>
                                <td class="style5" style="font-size:small; text-align:center">纬度</td>
                                <td class="style3">
                                    <asp:TextBox ID="txtLatitude" runat="server" Width="210px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3" style="font-size:small; text-align:center">最大dB</td>
                                <td class="style3">
                                    <asp:TextBox ID="txtMaxdB" runat="server" Width="210px"></asp:TextBox>
                                </td>
                                <td class="style5" style="font-size:small; text-align:center">平均dB</td>
                                <td class="style3">
                                    <asp:TextBox ID="txtAvgdB" runat="server" Width="210px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3" style="font-size:small; text-align:center">检测距离(m)</td>
                                <td class="style3">
                                    <asp:TextBox ID="txtDistance" runat="server" Width="210px"></asp:TextBox>
                                </td>
                                <td class="style7" style="font-size:small; text-align:center">&nbsp;</td>
                                <td class="style3">&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    <br /><br />
                    <div style="margin-left:80%">
                        <asp:Button ID="Button1" runat="server" Text="保存" OnClick="Button1_Click" Height="28px" Width="70px" BorderColor="White" ForeColor="#0066CC" />
                        &nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="列表" OnClick="Button2_Click" Height="28px" Width="70px" BorderColor="White" ForeColor="#0066CC" />
                    </div>
                    <br />
                </div>
            </div>
            <div style="margin-left: auto; margin-right: auto; width: 1004px; background-color: #E2E9EF">
                <div style="margin-left: auto; margin-right: auto; width: 800px">
                    
                </div>
            </div>
        </div>
    </form>
</body>
</html>

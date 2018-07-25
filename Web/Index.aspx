<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript">
        function ChangeGet(SelectCheckBox) {
            //找到页面所有 input
            var objs = document.getElementsByTagName("input");
            for (var i = 0; i < objs.length; i++) {
                //找到 input 中的 checkBox
                if (objs[i].type.toLowerCase() == "checkBox")
                    //所有 checkBox 为 false
                    objs[i].checked = false;
            }
            //找到选中的 checkBox
            var SelectCheckBoxID = SelectCheckBox.id;
            //选中 checkBox 为 true
            document.getElementById(SelectCheckBoxID).checked = true;
        }
    </script>
    <title></title>
    <link href="CSS/jquery.datetimepicker.css" rel="Stylesheet" type="text/css" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script src="JS/jquery.datetimepicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="margin-left: auto; margin-right: auto; width: 1004px">
            <div style="height: 10px"></div>
            <div>
                <img src="Images/logo.png" alt="" />
            </div>
            <div style="height: 10px"></div>
            <div style="height: 30px">
                <asp:Label ID="Label3" runat="server" Text="欢迎您：    "></asp:Label>
                <asp:Label ID="lblWelcomeInfo" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                &nbsp;&nbsp;
                <a href="ModifyPwd.aspx" style="text-decoration: none">
                    <asp:Label ID="lblChangePwd" runat="server" Text="更改密码" Font-Size="Small" ForeColor="Blue"></asp:Label></a>
                &nbsp;<asp:Label ID="Label7" runat="server" Text="|" ForeColor="Gray" Font-Size="Small"></asp:Label>
                &nbsp;<a href="Suggestion.aspx" style="text-decoration: none"><asp:Label ID="lblSuggestion" runat="server" Text="建议反馈" Font-Size="Small" ForeColor="Blue"></asp:Label></a>
                &nbsp;<asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="Gray" Text="|"></asp:Label>
                &nbsp;<a href="ShowResultOnMap4User.aspx" target="_blank" style="text-decoration: none"><asp:Label ID="Label6" runat="server" Text="位置记录" Font-Size="Small" ForeColor="Blue" Font-Bold="true"></asp:Label></a>
                <br />
                <!-- 提示登录用户收到的消息条数 -->
                <div id="div_Message" runat="server">
                    <a href="UserMessageList.aspx">你有<asp:Label ID="lblCount" runat="server" Text="Label" ForeColor="Red"></asp:Label></a>
                </div>
            </div>
            <div align="right">
                <asp:Button ID="btnReadyUpload" runat="server" Text="上传新文件" BackColor="#3366CC" Height="32px" Width="90px" ForeColor="White" OnClick="btnReadyUpload_Click" />
            </div>
            <div style="height: 5px"></div>
            <div style="padding: 5px; border: 1px solid #C0C0C0">
                <div style="color: blue; font-size: small">
                    <asp:Label ID="Label1" runat="server" Text="检测日期：" ForeColor="Red"></asp:Label>
                    <input type="text" id="datatimepickerStart" style="height: 18px; width: 100px" runat="server" />
                    <asp:Label ID="Label2" runat="server" Text="  -&nbsp;"></asp:Label>
                    <input type="text" id="datatimepickerEnd" style="height: 18px; width: 100px" runat="server" />
                    &nbsp;线路名称：<asp:TextBox ID="txtWayName" runat="server" Height="21px" Width="100px"></asp:TextBox>
                    &nbsp;线路编号：<asp:TextBox ID="txtWayNO" runat="server" Height="21px" Width="100px"></asp:TextBox>
                    &nbsp;管理编号：<asp:TextBox ID="txtManagerNO" runat="server" Height="21px" Width="100px"></asp:TextBox>
                    <br />
                    <br />
                    设备种类：
                    <asp:DropDownList ID="ddlProductType" runat="server" Height="28px" Width="210px">
                        <asp:ListItem Value="开关">开关</asp:ListItem>
                        <asp:ListItem Value="避雷器">避雷器</asp:ListItem>
                        <asp:ListItem Value="变压器">变压器</asp:ListItem>
                        <asp:ListItem Value="重合器">重合器</asp:ListItem>
                        <asp:ListItem Value="隔离刀闸">隔离刀闸</asp:ListItem>
                        <asp:ListItem Value="电缆终端盒">电缆终端盒</asp:ListItem>
                        <asp:ListItem Value="悬式绝缘子">悬式绝缘子</asp:ListItem>
                        <asp:ListItem Value="柱式绝缘子" Selected="True">柱式绝缘子</asp:ListItem>
                        <asp:ListItem Value="跌落式熔断器">跌落式熔断器</asp:ListItem>
                        <asp:ListItem Value="电缆线夹绝缘套">电缆线夹绝缘套</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;设备种类：
                    <asp:DropDownList ID="ddlProductState" runat="server" Height="28px" Width="210px">
                        <asp:ListItem Value="变位">变位</asp:ListItem>
                        <asp:ListItem Value="剥离">剥离</asp:ListItem>
                        <asp:ListItem Value="腐蚀">腐蚀</asp:ListItem>
                        <asp:ListItem Value="击穿">击穿</asp:ListItem>
                        <asp:ListItem Value="裂纹">裂纹</asp:ListItem>
                        <asp:ListItem Value="良好" Selected="True">良好</asp:ListItem>
                        <asp:ListItem Value="侵蚀">侵蚀</asp:ListItem>
                        <asp:ListItem Value="碳化">碳化</asp:ListItem>
                        <asp:ListItem Value="污秽">污秽</asp:ListItem>
                        <asp:ListItem Value="无光泽">无光泽</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;缺陷程度：
                    <asp:TextBox ID="txtBadLVStart" runat="server" Height="21px" Width="70px"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtBadLVStart" ErrorMessage="请输入数字" MaximumValue="100" MinimumValue="0" Type="Double" Display="Dynamic"></asp:RangeValidator>
                    &nbsp;-&nbsp;
                    <asp:TextBox ID="txtBadLVEnd" runat="server" Height="21px" Width="70px"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtBadLVEnd" ErrorMessage="请输入数字" MaximumValue="100" MinimumValue="0" Type="Double" Display="Dynamic"></asp:RangeValidator>
                    <br />
                    <br />
                </div>
                <div align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="搜  索" BackColor="#3366CC" Height="32px" Width="70px" ForeColor="White" OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnSearchAll" runat="server" Text="查看全部" BackColor="#3366CC" Height="32px" Width="70px" ForeColor="White" OnClick="btnSearchAll_Click" />
                </div>
            </div>
            <br />
            <div align="right">
                <asp:Button ID="btnPrint" runat="server" Text="报  表" BackColor="#3366CC" Height="32px" Width="70px" ForeColor="White" OnClick="btnPrint_Click" />
            </div>
            <div style="text-align: center">
                <asp:GridView ID="GridView" runat="server" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="GridView_PageIndexChanging" OnRowDataBound="GridView_RowDataBound" OnRowDeleting="GridView_RowDeleting" OnRowEditing="GridView_RowEditing" OnRowCommand="GridView_RowCommand">
                    <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPoloNoPicPath" runat="server" Text='<%#Eval("PoleNOPicPath") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblWholePicPath" runat="server" Text='<%#Eval("WholePicPath") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblBadProductPicPath" runat="server" Text='<%#Eval("BadProductPicPath") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSoundURL" runat="server" Text='<%#Eval("SoundURL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblLongitude" runat="server" Text='<%#Eval("Longitude") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblLatitude" runat="server" Text='<%#Eval("Latitude") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblReportDataID" runat="server" Text='<%#Eval("ReportDataID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="选择报表">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编号">
                            <ItemTemplate>
                                <asp:Label ID="lblLineNO" runat="server" Text='<%#Eval("LineNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="检测日期">
                            <ItemTemplate>
                                <asp:Label ID="lblcheckTime" runat="server" Text='<%#Eval("checkTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="设备种类">
                            <ItemTemplate>
                                <asp:Label ID="lblProductType" runat="server" Text='<%#Eval("CheckProductType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="设备状态">
                            <ItemTemplate>
                                <asp:Label ID="lblProductState" runat="server" Text='<%#Eval("ProductState") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编号照片">
                            <ItemTemplate>
                                <div id="PoleNOPic">
                                    <a href="<%#Eval("PoleNOPicPath") %>" title="点击看大图" target="_blank">
                                        <img id="PoleNOPicPath" runat="server" src='<%#Eval("PoleNOPicPath") %>' align="left" height="70" width="70" /></a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="整体照片">
                            <ItemTemplate>
                                <div id="WholePic">
                                    <a href="<%#Eval("WholePicPath") %>" title="点击看大图" target="_blank">
                                        <img id="WholePicPath" runat="server" src='<%#Eval("WholePicPath") %>' align="left" height="70" width="70" /></a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="不良照片">
                            <ItemTemplate>
                                <div id="BadProductPic">
                                    <a href="<%#Eval("BadProductPicPath") %>" title="点击看大图" target="_blank">
                                        <img id="BadProductPicPath" runat="server" src='<%#Eval("BadProductPicPath") %>' align="left" height="70" width="70" /></a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="频率">
                            <ItemTemplate>
                                <asp:Label ID="lblFrequency" runat="server" Text='<%#Eval("Frequency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="最大dB值">
                            <ItemTemplate>
                                <asp:Label ID="lblMaxdB" runat="server" Text='<%#Eval("MaxdB") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="平均dB值">
                            <ItemTemplate>
                                <asp:Label ID="lblAvgdB" runat="server" Text='<%#Eval("AVGdB") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="缺陷程度">
                            <ItemTemplate>
                                <asp:Label ID="lblDefectLevel" runat="server" Text='<%#Eval("DefectLevel") %>' ForeColor="Red"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="音频">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:ImageButton ID="IbtnMusicPlay" runat="server" CommandName="PlayMucic" ImageUrl="~/Web/Images/MusicPlay.png" Height="40px" Width="40px" />
                                        <asp:ImageButton ID="IbtnMusicStop" runat="server" CommandName="StopMusic" ImageUrl="~/Web/Images/MusicStop.png" Height="40px" Width="40px" Visible="false" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CausesValidation="false" Text="编辑">编辑</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" Text="删除" CommandArgument='<%#Eval("ReportDataID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle ForeColor="#333333" BackColor="#CCCCCC" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <br />
            <br />
            <div style="height: 10px"></div>
            <div style="background-color: #E2E9EF"></div>
            <div style="margin-left: auto; margin-right: auto; width: 800px">
                <div style="font-family: Arial, Helvetica, sans-serif; font-size: 12px">
                    <p>销售电话：0371-56977880/56977890/63812255&nbsp;&nbsp;&nbsp;&nbsp;售后电话：0371-56977875/56977873</p>
                    <p>传真：0371-63813838&nbsp;&nbsp;&nbsp;&nbsp;QQ：613712485/613712493&nbsp;&nbsp;&nbsp;&nbsp;邮箱：hnhbcom@163.com</p>
                    <p>地址：郑州市金水区丰产路&nbsp;21&nbsp;号&nbsp;SOHO&nbsp;世纪城东塔&nbsp;8&nbsp;楼&nbsp;C&nbsp;座</p>
                    <p>Copyright&nbsp;2009-2017&nbsp;www.hnhbdq.com    All&nbsp;rights&nbsp;reserved.&nbsp;&nbsp;&nbsp;&nbsp;河南宏博测控技术有限公司&nbsp;版权所有</p>
                    <p>豫&nbsp;ICP&nbsp;备&nbsp;09018549&nbsp;号</p>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            $('#datetimepicker_mask').datetimepicker({
                mask: '9999/19/39 29:59'
            });
            $('#datetimepicker').datetimepicker();
            $('#datetimepicker').datetimepicker({
                value: '2017/12/01 00:00', step: 10
            });
            $('#open').click(function () {
                $('#datetimepicker4').datetimepicker('show');
            });
            $('#close').click(function () {
                $('#datetimepicker4').datetimepicker('hide');
            });
            $('#destory').click(function () {
                if ($('#datetimepicker6').data('xdsoft_datetimepicker')) {
                    $('#datetimepicker6').datetimepicker('destory');
                    this.value = 'create';
                } else {
                    $('#datetimepicker6').datetimepicker();
                    this.value = 'destory';
                }
            });
            var logic = function (currentDateTime) {
                if (currentDateTime.getDay() == 6) {
                    this.setOptions({
                        minTime: '1:00'
                    });
                } else {
                    this.setOptions({
                        minTime: '1:00'
                    });
                }
            };
            $('#datetimepickerStart').datetimepicker({
                onChangeDateTime: logic,
                onShow: logic
            });
            $('#datetimepickerEnd').datetimepicker({
                onChangeDateTime: logic,
                onShow: logic
            });
        </script>
    </form>
</body>
</html>

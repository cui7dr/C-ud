<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportManager.aspx.cs" Inherits="ReportManager" %>

<html>
<head id="Head1" runat="server">
    <title>报表管理</title>
    <link href="../CSS/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery.datetimepicker.js" type="text/javascript"></script>
    <script src="../JS/jquery.js" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
        <div style="margin-left: auto; margin-right: auto; width: 1004px">
            <div style="height: 20px"></div>
            <br />
            <div>
                <asp:Button ID="Button4" runat="server" Height="30px" Width="100px" Text="用户管理" BackColor="#99CCFF" OnClick="Button4_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button5" runat="server" Height="30px" Width="100px" Text="设备管理" BackColor="#99CCFF" OnClick="Button5_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button6" runat="server" Height="30px" Width="100px" Text="记录管理" BackColor="#99CCFF" OnClick="Button6_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button7" runat="server" Height="30px" Width="100px" Text="报表管理" BackColor="Red" OnClick="Button7_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button8" runat="server" Height="30px" Width="100px" Text="建议管理" BackColor="#99CCFF" OnClick="Button8_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button9" runat="server" Height="30px" Width="100px" Text="通知管理" BackColor="#99CCFF" OnClick="Button9_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button10" runat="server" Height="30px" Width="100px" Text="位置展示" BackColor="#99CCFF" OnClick="Button10_Click" />
            </div>
            <div style="height: 15px"></div>
            <div style="height: 7px"></div>
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
                    设备种类：<asp:DropDownList ID="ddlProductType" runat="server" Height="28px" Width="210px"></asp:DropDownList>
                    &nbsp;设备状态：<asp:DropDownList ID="ddlProductState" runat="server" Height="28px" Width="210px"></asp:DropDownList>
                    &nbsp;缺陷程度：<asp:TextBox ID="txtBadLVStart" runat="server" Height="21px" Width="70px"></asp:TextBox>
                    &nbsp;-&nbsp;<asp:TextBox ID="txtBadLVEnd" runat="server" Height="21px" Width="70px"></asp:TextBox>
                    <br />
                    <br />
                </div>
                <div style="margin-left: 95%">
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" BackColor="#3366CC" Height="32px" Width="49px" ForeColor="White" OnClick="btnSearch_Click" />
                </div>
            </div>
            <br />
            <div style="text-align: center">
                <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView_PageIndexChanging" OnRowDataBound="GridView_RowDataBound" OnRowDeleting="GridView_RowDeleting" OnRowEditing="GridView_RowEditing">
                    <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="编号">
                            <ItemTemplate>
                                <asp:Label ID="lblLineNo" runat="server" Text='<%#Eval("LineNo") %>'></asp:Label>
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
                                    <a href="<%#Eval("PoleNOPicPath") %>" title="点击查看大图" target="_blank">
                                        <img id="PoleNOPicPath" runat="server" src='<%#Eval("PoleNOPicPath") %>' align="left" height="60" width="60" />
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="整体照片">
                            <ItemTemplate>
                                <div id="WholePic">
                                    <a href="<%#Eval("WholePicPath") %>" title="点击查看大图" target="_blank">
                                        <img id="WholePicPath" runat="server" src='<%#Eval("WholePicPath") %>' align="left" height="60" width="60" />
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="不良照片">
                            <ItemTemplate>
                                <div id="BadPic">
                                    <a href="<%#Eval("BadProductPicPath") %>" title="点击查看大图" target="_blank">
                                        <img id="BadPicPath" runat="server" src='<%#Eval("BadProductPicPath") %>' align="left" height="60" width="60" />
                                    </a>
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
                                <asp:Label ID="lblAvgdB" runat="server" Text='<%#Eval("AvgdB") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="缺陷程度">
                            <ItemTemplate>
                                <asp:Label ID="lblDefectLevel" runat="server" Text='<%#Eval("DefectLevel") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="详情">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CausesValidation="false" Text="详情">详情</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnDel" runat="server" Text="详情" CommandArgument='<%#Eval("ReportDataID") %>' CommandName="Delete">删除</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="true" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="true" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="true" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
    </form>
    <script>
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
</body>
</html>



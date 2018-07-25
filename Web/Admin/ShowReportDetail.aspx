<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="MasterPage.master" CodeFile="ShowReportDetail.aspx.cs" Inherits="ShowReportDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/jquery.js" type="text/javascript"></script>
    <script src="../JS/jquery.datetimepicker.js" type="text/javascript"></script>
    <link href="../CSS/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            height: 16px;
        }

        .style3 {
            height: 19px;
        }

        .style4 {
            height: 13px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="margin-left:auto; margin-right:auto; margin-top:50px; width:1004px">
            <br />
            <div>
                <asp:Button ID="Button4" runat="server" Height="30px" Width="100px" Text="用户管理" BackColor="#99CCFF" OnClick="Button4_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button5" runat="server" Height="30px" Width="100px" Text="设备管理" BackColor="#99CCFF" OnClick="Button5_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button6" runat="server" Height="30px" Width="100px" Text="记录管理" BackColor="#99CCFF" OnClick="Button6_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button7" runat="server" Height="30px" Width="100px" Text="报表管理" BackColor="Red" OnClick="Button7_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button8" runat="server" Height="30px" Width="100px" Text="建议管理" BackColor="#99CCFF" OnClick="Button8_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button9" runat="server" Height="30px" Width="100px" Text="通知管理" BackColor="#99CCFF" OnClick="Button9_Click" />
            </div>
            <div style="height:21px"></div>
            <div>
                <asp:Label ID="Label2" runat="server" Text="用户检测报告" Font-Size="Larger" ForeColor="Blue" Font-Bold="true"></asp:Label>
                <br />
                <table id="Tabel" runat="server" cellpadding="0" cellspacing="0" class="style1" style="border-color:#000000; border-width:thin; font-size:small; color:#000000; line-height:25px">
                    <tr>
                        <td class="style3" style="font-size:small; text-align:center">线路名称</td>
                        <td>
                            <asp:TextBox ID="txtLineName" Width="190px" runat="server"></asp:TextBox>
                        </td>
                        <td style="font-size:small; text-align:center">线路等级</td>
                        <td>
                            <asp:DropDownList ID="ddlDistance" runat="server" Height="16px" Width="190px">
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
                        <td class="style3" style="font-size:small; text-align:center">线路编号</td>
                        <td>
                            <asp:TextBox ID="txtLineNo" Width="190px" runat="server"></asp:TextBox>
                        </td>
                        <td style="font-size:small; text-align:center">设备状态</td>
                        <td>
                            <asp:DropDownList ID="ddlProductState" runat="server" Height="23px" Width="190px">
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
                        <td class="style3" style="font-size:small; text-align:center">设备种类</td>
                        <td>
                            <asp:DropDownList ID="ddlProductType" runat="server" Height="23px" Width="220px">
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
                        <td style="font-size:small; text-align:center">检测人员</td>
                        <td>
                            <asp:TextBox ID="txtCheckName" runat="server" Height="16px" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" style="font-size:small; text-align:center">诊断建议</td>
                        <td class="style2">
                            <asp:TextBox ID="txtSuggestion" runat="server" Height="18px" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" style="font-size:small; text-align:center" colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2" style="font-size:small; text-align:center">编号照片</td>
                        <td class="style2" colspan="2">
                            <input type="file" name="Pic" id="FileNoPic" accept="image/*" runat="server" />
                            <asp:Button ID="btnNoPic" runat="server" Text="上传" OnClick="btnNoPic_Click" />
                            <asp:Label ID="lblNoPic" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" style="font-size:small; text-align:center">整体照片</td>
                        <td class="style2" colspan="2">
                            <input type="file" name="Pic" id="FileFullPic" accept="image/*" runat="server" />
                            <asp:Button ID="btnFullPic" runat="server" Text="上传" OnClick="btnFuLLPic_Click" />
                            <asp:Label ID="lblFullPic" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" style="font-size:small; text-align:center">不良照片</td>
                        <td class="style2" colspan="2">
                            <input type="file" name="Pic" id="FileBadPic" accept="image/*" runat="server" />
                            <asp:Button ID="btnBadPic" runat="server" Text="上传" OnClick="btnBadPic_Click" />
                            <asp:Label ID="lblBadPic" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" style="font-size:small; text-align:center" colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size:small; text-align:center">检测日期:</td>
                        <td class="style3">
                            <input id="datetimepickerEnd" style="width:190px" type="text" runat="server" />

                        </td>
                        <td class="style3" style="font-size:small; text-align:center">输入日期:</td>
                        <td class="style3">
                            <input id="datetimepickerStart" style="width:190px" type="text" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size:small; text-align:center">频  率:</td>
                        <td class="style3">
                            <asp:TextBox ID="txtFrequency" Width="190px" runat="server"></asp:TextBox>

                        </td>
                        <td class="style3" style="font-size:small; text-align:center">管理编号:</td>
                        <td class="style3">
                            <asp:TextBox ID="txtManagerNo" Width="190px" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size:small; text-align:center">温度(℃):</td>
                        <td class="style3">
                            <asp:TextBox ID="txtTemperature" Width="190px" runat="server"></asp:TextBox>

                        </td>
                        <td class="style3" style="font-size:small; text-align:center">湿度(%):</td>
                        <td class="style3">
                            <asp:TextBox ID="txtHumidity" Width="190px" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size:small; text-align:center">维  度:</td>
                        <td class="style3">
                            <asp:TextBox ID="txtLongitude" Width="190px" runat="server"></asp:TextBox>

                        </td>
                        <td class="style3" style="font-size:small; text-align:center">经  度:</td>
                        <td class="style3">
                            <asp:TextBox ID="txtLatitude" Width="190px" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size:small; text-align:center">最大dB值:</td>
                        <td class="style3">
                            <asp:TextBox ID="txtMaxdB" Width="190px" runat="server"></asp:TextBox>

                        </td>
                        <td class="style3" style="font-size:small; text-align:center">平均dB值:</td>
                        <td class="style3">
                            <asp:TextBox ID="txtAvgdB" Width="190px" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="font-size:small; text-align:center">距离(m):</td>
                        <td class="style3">
                            <asp:TextBox ID="txtDistance" Width="190px" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                </table>
            </div>
            <br />
                <div style="margin-left:80%">
                    <asp:Label ID="Label11" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <br /><br />
                    <asp:Button ID="Button1" runat="server" Text="保存" OnClick="Button1_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="列表" OnClick="Button2_Click" />
                </div>
                <br /><br /><br />
        </div>
    </div>
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
</asp:Content>

<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="ModifyPwd.aspx.cs" Inherits="ModifyPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>修改密码</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-left:auto; margin-right:auto; margin-top:50px; width:1004px">
        <br />
        <div style="margin-left:42%; margin-right:auto">
            <asp:Label ID="Label1" runat="server" Text="修改密码" Font-Size="30px" ForeColor="Gray"></asp:Label>
        </div>
        <div style="margin-left:auto; margin-right:auto; width:50%; margin-top:50px; height:250px">
            <div style="border-color:#C0C0C0; margin-top25px">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="height:200px">
                    <tr>
                        <td style="width:50%; text-align:center; line-height:35px">
                            <asp:Label ID="label2" runat="server" Text="原密码" ForeColor="Blue"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr>
                        <td style="width:50%; text-align:center; line-height:35px">
                            <asp:Label ID="Label3" runat="server" Text="新密码" ForeColor="Blue"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50%; text-align:center; line-height:35px">
                            <asp:Label ID="Label4" runat="server" Text="确定密码" ForeColor="Blue"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPwd2" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50%; text-align:center; line-height:35px">
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="建议使用 6-16 位非纯数字（请勿使用特殊字符）" ForeColor="Red" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnModify" runat="server" Text="确认" BackColor="White" BorderColor="White" Height="28px" Width="70px" ForeColor="#0066CC" OnClientClick="if(!confirm('确定修改？')) return flase;" OnClick="btnModify_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="取消" BackColor="White" BorderColor="White" Height="28px" Width="70px" ForeColor="#0066CC" OnClick="btnReset_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="返回" BackColor="White" BorderColor="White" Height="28px" Width="70px" ForeColor="#0066CC" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br /><br />
    </div>
</asp:Content>

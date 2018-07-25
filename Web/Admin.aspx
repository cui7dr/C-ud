<%@ Page Title="管理员登录" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 89%;
        }  

        .style2 {
            height: 20px;
        }

        .style3 { 
            width: 192px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin-left: auto; margin-right: auto; width: 1004px">
        <br />
        <div style="width: 600px; margin-left: auto; margin-right: auto">
            <div>
                <img src="Images/adminInfo.jpg" style="width: 600px" />
            </div>
            <br />
            <table class="style1">
                <tr>
                    <td align="right" class="style3">
                        <asp:Label ID="Label1" runat="server" Text="用户名" ForeColor="#0066CC" Font-Size="Large"></asp:Label>
                    </td>
                    <td align="center" height="35px">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style3" height="35px">
                        <asp:Label ID="Label2" runat="server" Text="密码" ForeColor="#0066CC"></asp:Label>
                    </td>
                    <td aligh="center" height="35px">
                        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">&nbsp;</td>
                    <td align="center" height="10px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="style2" colspan="2">
                        <div style="width: 222px; margin-left: auto; margin-right: auto" align="center">
                            <asp:Button ID="btnLogin" runat="server" Text="登录" Height="30px" Width="70px" BorderColor="White" OnClick="btnLogin_Click" ForeColor="#0066CC" />
                            <asp:Button ID="btnReset" runat="server" Text="重置" Height="30px" Width="70px" BorderColor="White" OnClick="btnReset_Click" ForeColor="#0066CC" />
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <div>
                <img src="Images/AdminInfoButton.jpg" style="width: 600px" />
            </div>
        </div>
    </div>
    <br />
    <br />
</asp:Content>

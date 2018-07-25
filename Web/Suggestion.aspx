<%@ Page Title="意见反馈" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="Suggestion.aspx.cs" Inherits="Suggestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
        }

        .style2 {
            width: 150px;
            height: 40px;
        }

        .style3 {
            height: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-left: auto; margin-right: auto; margin-top: 28px; width: 1004px">
        <div style="margin-left: 44%; margin-right: auto">
            <asp:Label ID="Label1" runat="server" Text="意见反馈" Font-Size="32px" ForeColor="Gray"></asp:Label>
        </div>
        <br />
        <div style="margin-left:auto; margin-right:auto; width:700px">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style2" align="center">
                        <asp:Label ID="Label2" Text="反馈人" runat="server"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="txtSugName" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" align="center">
                        <asp:Label ID="Label3" Text="反馈时间" runat="server"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="txtSugTime" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1" align="center">
                        <asp:Label ID="Label4" Text="反馈内容" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSugContent" TextMode="MultiLine" Width="500px" Height="20px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1" align="center" colspan="2" height="70px">
                        <asp:Button ID="btnPostSug" Text="提交" Height="28px" Width="70px" BorderColor="White" ForeColor="#0066CC" runat="server" OnClick="btnPostSug_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnCancle" Text="返回" Height="28px" Width="70px" BorderColor="White" ForeColor="#0066CC" runat="server" OnClick="btnCancle_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>
</asp:Content>

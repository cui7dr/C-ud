<%@ Page Title="通知列表" Language="C#" AutoEventWireup="true" MasterPageFile="~/Web/MasterPage.master" CodeFile="UserMessageList.aspx.cs" Inherits="UserMessageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div align="center" style="margin-left:auto; margin-right:auto">
            <asp:Label ID="Label1" Text="通知列表" Font-Size="30px" ForeColor="Gray" runat="server"></asp:Label>
        </div>
        <div style="height:14px" align="center"></div>
        <div id="divContentList" style="margin-left:auto; margin-right:auto; width:700px">
            <div style="font-size:14pt">
                <asp:Button ID="btnReaded" Text="全  部" BorderStyle="None" Width="70px" BorderColor="White" ForeColor="#0066CC" runat="server" OnClick="btnReaded_Click" />
                &nbsp;|&nbsp;
                <asp:Button ID="btnReading" Text="未  读" BorderStyle="None" Width="70px" BorderColor="White" ForeColor="#0066CC" runat="server" OnClick="btnReading_Click" />
                &nbsp;|&nbsp;
                <asp:Button ID="btnBack" Text="返  回" BorderStyle="None" Width="70px" BorderColor="White" ForeColor="#0066cc" runat="server" OnClick="btnBack_Click" />
            </div>
            <hr />
            <div style="margin-left:auto; margin-right:auto; width:700px">
                <asp:DataList ID="dlContent" runat="server" OnItemCommand="dlContent_ItemCommand" OnItemDataBound="dlContent_ItemDataBound">
                    <ItemTemplate>
                        <%--<input id="Hidden"="_viewState" type="hidden" />--%>
                        <table style="width:700px; background-color:#eee; font-size:12px">
                            <tr>
                                <td align="center" style="height:40px">
                                    <asp:HiddenField ID="hbMessID" Value='<%# Eval("MID") %>' runat="server" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="center" style="height:40px"><%# Eval("MContent") %></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td align="center" style="width:200px"><%# Eval("MTime") %></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td align="center">
                                    <asp:Button ID="btnRead" runat="server" BorderStyle="None" CommandArgument='<%Eval("MID") %>' CommandName="Update" ValidationGroup='<%#((DataListItem)Container).ItemIndex %>' Text="标记为已读" />
                                </td>
                            </tr>
                        </table>
                        <hr />
                    </ItemTemplate>
                </asp:DataList>
                <p align="center">
                    <asp:Label ID="lblInfo" Text="Label" runat="server"></asp:Label>
                    <asp:Button ID="btnPre" Text="上一页" Width="70px" BorderColor="White" ForeColor="#0066CC" runat="server" OnClick="btnPre_Click" />
                    <asp:Button ID="btnNext" Text="下一页" Width="70px" BorderColor="White" ForeColor="#0066CC" runat="server" OnClick="btnNext_Click" />
                </p>
            </div>
            <div align="center">
                <asp:Button ID="Button1" Text="全  部" BorderStyle="None" Width="70px" BackColor="White" ForeColor="#0066CC" runat="server" OnClick="btnReaded_Click" />
                &nbsp;|&nbsp;
                <asp:Button ID="Button2" Text="未  读" BorderStyle="None" Width="70px" BorderColor="White" ForeColor="#0066CC" runat="server" OnClick="btnReading_Click" />
                &nbsp;|&nbsp;
                <asp:Button ID="Button3" Text="返  回" BorderStyle="None" Width="70px" BorderColor="White" ForeColor="#0066cc" runat="server" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
</asp:Content>

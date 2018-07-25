<%@ Page Title="通知管理" Language="C#" AutoEventWireup="true" MasterPageFile="MasterPage.master" CodeFile="MessaggeManager.aspx.cs" Inherits="MessageManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>通知管理</title>
    <style type="text/css">
        #Button2 {
            height: 29px;
            width: 95px;
        }

        #Button1 {
            height: 36px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-left: auto; margin-right: auto; width: 1004px">
        <br />
        <div>
            &nbsp;<asp:Button ID="Button4" runat="server" Height="30px" Width="100px" Text="用户管理" BackColor="#99CCFF" OnClick="Button4_Click" />
            &nbsp;<asp:Button ID="Button5" runat="server" Height="30px" Width="100px" Text="设备管理" BackColor="#99CCFF" OnClick="Button5_Click" />
            &nbsp;<asp:Button ID="Button6" runat="server" Height="30px" Width="100px" Text="记录管理" BackColor="#99CCFF" OnClick="Button6_Click" />
            &nbsp;<asp:Button ID="Button7" runat="server" Height="30px" Width="100px" Text="报表管理" BackColor="#99CCFF" OnClick="Button7_Click" />
            &nbsp;<asp:Button ID="Button8" runat="server" Height="30px" Width="100px" Text="建议管理" BackColor="#99CCFF" OnClick="Button8_Click" />
            &nbsp;<asp:Button ID="Button9" runat="server" Height="30px" Width="100px" Text="通知管理" BackColor="Red" OnClick="Button9_Click" />
            &nbsp;<asp:Button ID="Button10" runat="server" Height="30px" Width="100px" Text="位置展示" BackColor="#99CCFF" OnClick="Button10_Click" />
        </div>
        <div style="height: 15px"></div>
        <div align="right">
            <asp:Label ID="Label1" runat="server" Text="用户名" ForeColor="Blue" Font-Size="Small"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddlName" runat="server"></asp:DropDownList>
            &nbsp;
            <asp:Label ID="Label2" runat="server" Text="内容" ForeColor="Blue" Font-Size="Small"></asp:Label>
            &nbsp;<asp:TextBox ID="txtMesContent" runat="server" Width="400px"></asp:TextBox>
            <asp:Button ID="btnPostMes" runat="server" Text="发送" Height="30px" Width="70px" BorderColor="White" ForeColor="#0066CC" OnClick="btnPostMes_Click" />
        </div>
        <div style="height: 10px"></div>
        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" Width="100%" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView_RowDataBound" OnRowDeleting="GridView_RowDeleting">
            <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="用户名">
                    <ItemTemplate>
                        <asp:Label ID="lblLoginID" runat="server" Text='<%#Eval("loginID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公司名称">
                    <ItemTemplate>
                        <asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("companyName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="通知时间">
                    <ItemTemplate>
                        <asp:Label ID="lblMTime" runat="server" Text='<%#Eval("MTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="通知内容">
                    <ItemTemplate>
                        <asp:Label ID="lblMContent" runat="server" Text='<%#Eval("MContent") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状  态">
                    <ItemTemplate>
                        <asp:Label ID="lblIsRead" runat="server" Text='<%#Eval("IsRead").ToString() == "0" ? "未读" : "已读" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删  除">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnDel" runat="server" Text="删除" CommandArgument='<%#Eval("MID") %>' CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="true" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="true" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD4" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <br />
        <br />
    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="UserManager.aspx.cs" Inherits="UserMnager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>用户管理</title>
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
            &nbsp;<asp:Button ID="Button4" runat="server" Height="30px" Width="100px" Text="用户管理" BackColor="Red" OnClick="Button4_Click" />
            &nbsp;<asp:Button ID="Button5" runat="server" Height="30px" Width="100px" Text="设备管理" BackColor="#99CCFF" OnClick="Button5_Click" />
            &nbsp;<asp:Button ID="Button6" runat="server" Height="30px" Width="100px" Text="记录管理" BackColor="#99CCFF" OnClick="Button6_Click" />
            &nbsp;<asp:Button ID="Button7" runat="server" Height="30px" Width="100px" Text="报表管理" BackColor="#99CCFF" OnClick="Button7_Click" />
            &nbsp;<asp:Button ID="Button8" runat="server" Height="30px" Width="100px" Text="建议管理" BackColor="#99CCFF" OnClick="Button8_Click" />
            &nbsp;<asp:Button ID="Button9" runat="server" Height="30px" Width="100px" Text="通知管理" BackColor="#99CCFF" OnClick="Button9_Click" />
            &nbsp;<asp:Button ID="Button10" runat="server" Height="30px" Width="100px" Text="位置展示" BackColor="#99CCFF" OnClick="Button10_Click" />
        </div>
        <div style="height: 15px"></div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="用户名"></asp:Label>&nbsp;
            <asp:TextBox ID="AddLoginID" runat="server"></asp:TextBox>&nbsp;
            <asp:Label ID="Label2" runat="server" Text="密码"></asp:Label>&nbsp;
            <asp:TextBox ID="AddLoginPwd" runat="server"></asp:TextBox>&nbsp;
            <asp:Label ID="Label3" runat="server" Text="公司名称"></asp:Label>&nbsp;
            <asp:TextBox ID="AddCompanyName" runat="server"></asp:TextBox>&nbsp;
            <asp:Label ID="Label4" runat="server" Text="公司地址"></asp:Label>&nbsp;
            <asp:TextBox ID="AddCompanyAddress" runat="server"></asp:TextBox>&nbsp;
        </div>
        <div style="height: 10px"></div>
        <div align="right">
            <asp:Button ID="Button3" runat="server" Height="35px" Width="60px" Text="添加" BackColor="#99CCFF" OnClick="Button3_Click" />
        </div>
        <div style="height: 15px"></div>
        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" Width="100%" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" OnPageIndexChanging="GridView_PageIndexChanging" OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowDataBound="GridView_RowDataBound" OnRowDeleting="GridView_RowDeleting" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating">
            <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="用户名">
                    <ItemTemplate>
                        <asp:Label ID="lblLoginID" runat="server" Text='<%#Eval("loginID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="密码">
                    <ItemTemplate>
                        <asp:Label ID="lblPwd" runat="server" Text='<%#Eval("loginPwd") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPwd" runat="server" Width="300px" Text='<%#Bind("loginPwd") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公司名称">
                    <ItemTemplate>
                        <asp:Label ID="lblcompanyName" runat="server" Text='<%#Eval("companyName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcompanyName" runat="server" Text='<%#Bind("companyName") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公司地址">
                    <ItemTemplate>
                        <asp:Label ID="lblcompanyAddress" runat="server" Text='<%#Eval("companyAddress") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcompanyAddress" runat="server" Text='<%#Bind("companyAddress") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="操作" ShowEditButton="true" />
                <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnDel" runat="server" Text="删除" CommandArgument='<%#Eval("UID") %>' CommandName="Delete"></asp:LinkButton>
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
    </div>
</asp:Content>

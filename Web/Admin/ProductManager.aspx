<%@ Page Title="设备管理" Language="C#" AutoEventWireup="true" MasterPageFile="MasterPage.master" CodeFile="ProductManager.aspx.cs" Inherits="ProductManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>设备管理</title>
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-left: auto; margin-right: auto; width: 1004px">
        <br />
        <div>
            <asp:Button ID="Button4" runat="server" Height="30px" Width="100px" Text="用户管理" BackColor="#99CCFF" OnClick="Button4_Click" />
            &nbsp;&nbsp;&nbsp;<asp:Button ID="Button5" runat="server" Height="30px" Width="100px" Text="设备管理" BackColor="Red" OnClick="Button5_Click" />
            &nbsp;&nbsp;&nbsp;<asp:Button ID="Button6" runat="server" Height="30px" Width="100px" Text="记录管理" BackColor="#99CCFF" OnClick="Button6_Click" />
            &nbsp;&nbsp;&nbsp;<asp:Button ID="Button7" runat="server" Height="30px" Width="100px" Text="报表管理" BackColor="#99CCFF" OnClick="Button7_Click" />
            &nbsp;&nbsp;&nbsp;<asp:Button ID="Button8" runat="server" Height="30px" Width="100px" Text="建议管理" BackColor="#99CCFF" OnClick="Button8_Click" />
            &nbsp;&nbsp;&nbsp;<asp:Button ID="Button9" runat="server" Height="30px" Width="100px" Text="通知管理" BackColor="#99CCFF" OnClick="Button9_Click" />
            &nbsp;&nbsp;&nbsp;<asp:Button ID="Button10" runat="server" Height="30px" Width="100px" Text="位置展示" BackColor="#99CCFF" OnClick="Button10_Click" />
        </div>
        <div style="height: 21px"></div>
        <div align="left">
            <asp:Label ID="Label1" runat="server" Text="版本号"></asp:Label>
            &nbsp;<asp:TextBox ID="AddVersion" runat="server" Width="200px"></asp:TextBox>
            &nbsp;<asp:Label ID="Label2" runat="server" Text="设备串号"></asp:Label>
            &nbsp;<asp:TextBox ID="AddIMEI" runat="server" Width="200px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button3" runat="server" BackColor="#99CCFF" Height="25px" Width="70px" OnClick="Button3_Click" Text="添加" />
        </div>
        <div style="height: 25px"></div>
        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" Width="100%" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" OnPageIndexChanging="GridView_PageIndexChanging" OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowDataBound="GridView_RowDataBound" OnRowDeleting="GridView_RowDeleting" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnSelectedIndexChanged="GridView_SelectedIndexChanged">
            <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="版本号">
                    <ItemTemplate>
                        <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("pVersion") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVersion" runat="server" Text='<%#Bind("pVersion") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="设备串号">
                    <ItemTemplate>
                        <asp:Label ID="lblIMEI" runat="server" Text='<%#Eval("pIMEI") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtIMEI" runat="server" Text='<%#Bind("pIMEI") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="公司名称">
                    <ItemTemplate>
                        <asp:Label ID="lblcompanyName" runat="server" Text='<%#Eval("companyName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hfcompanyName" runat="server" Value='<%#Eval("companyName") %>' />
                        <asp:DropDownList ID="ddlcompanyName" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:CommandField HeaderText="操作" ShowEditButton="true" />
                <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnDel" runat="server" Text="删除" CommandArgument='<%#Eval("PID") %>' CommandName="Delete"></asp:LinkButton>
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

<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SuggestionManager.aspx.cs" Inherits="SuggestionManager" %>

<asp:content id="Content1" contentplaceholderid="head" runat="Server">
    <title>建议管理</title>
    <style type="text/css">
        #Button2 {
            height: 29px;
            width: 95px;
        }

        #Button1 {
            height: 36px;
        }
    </style>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <div style="margin-left:auto; margin-right:auto; width:1004px">
        <br />
        <div>
            &nbsp;<asp:Button ID="Button4" runat="server" Height="30px" Width="100px" Text="用户管理" BackColor="#99CCFF" onclick="Button4_Click" />
            &nbsp;<asp:Button ID="Button5" runat="server" Height="30px" Width="100px" Text="设备管理" BackColor="#99CCFF" onclick="Button5_Click" />
            &nbsp;<asp:Button ID="Button6" runat="server" Height="30px" Width="100px" Text="记录管理" BackColor="#99CCFF" onclick="Button6_Click" />
            &nbsp;<asp:Button ID="Button7" runat="server" Height="30px" Width="100px" Text="报表管理" BackColor="#99CCFF" onclick="Button7_Click" />
            &nbsp;<asp:Button ID="Button8" runat="server" Height="30px" Width="100px" Text="建议管理" BackColor="Red" onclick="Button8_Click" />
            &nbsp;<asp:Button ID="Button9" runat="server" Height="30px" Width="100px" Text="通知管理" BackColor="#99CCFF" onclick="Button9_Click" />
            &nbsp;<asp:Button ID="Button10" runat="server" Height="30px" Width="100px" Text="位置展示" BackColor="#99CCFF" onclick="Button10_Click" />
        </div>
        <div style="height:15px;"></div>
        <div align="right">
            <asp:Label ID="Label1" runat="server" Text="按用户名搜索" ForeColor="Blue" Font-Size="Small"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="ddlName" runat="server"></asp:DropDownList>
            &nbsp;
            <asp:Button ID="btnSearch" runat="server" Height="30px" Width="70px" Text="搜索" BorderColor="White"  ForeColor="#0066CC" onclick="btnPostSug_Click" />
        </div>
        <div style="height:10px"></div>
        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" Width="100%" CellPadding="4" ForeColor="Black" GridLines="Vertical"  BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" onpageindexchanging="GridView_PageIndexChanging" onrowdatabound="GridView_RowDataBound" onrowdeleting="GridView_RowDeleting" onrowediting="GridView_RowEditing">
            <AlternatingRowStyle HorizontalAlign="Center" BackColor="White"/>
            <Columns>
                <asp:TemplateField HeaderText="登录ID">
                    <ItemTemplate>
                        <asp:Label ID="lblLoginID" runat="server" Text='<%#Eval("loginID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公司名称">
                    <ItemTemplate>
                        <asp:Label ID="lblcompanyName" runat="server" Text='<%#Eval("companyName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="建议时间">
                    <ItemTemplate>
                        <asp:Label ID="lblStime" runat="server" Text='<%#Eval("Stime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="建议内容">
                    <ItemTemplate>
                        <asp:Label ID="lblSContent" runat="server" Text='<%#Eval("SContent") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <asp:Label ID="lblIsRead" runat="server" Text='<%#Eval("IsRead").ToString()=="0"?"未读":"已读" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标记已读">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName=Edit CausesValidation=False Text="标记已读">标记已读</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnDel" runat="server" Text="删除" CommandArgument='<%#Eval("SID") %>' CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <br /><br />
    </div>
</asp:content>

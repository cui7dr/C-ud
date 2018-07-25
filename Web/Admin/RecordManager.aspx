<%@ Page Title="记录管理" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="RecordManager.aspx.cs" Inherits="RecordManager" %>

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
            &nbsp;<asp:Button ID="Button6" runat="server" Height="30px" Width="100px" Text="记录管理" BackColor="Red" OnClick="Button6_Click" />
            &nbsp;<asp:Button ID="Button7" runat="server" Height="30px" Width="100px" Text="报表管理" BackColor="#99CCFF" OnClick="Button7_Click" />
            &nbsp;<asp:Button ID="Button8" runat="server" Height="30px" Width="100px" Text="建议管理" BackColor="#99CCFF" OnClick="Button8_Click" />
            &nbsp;<asp:Button ID="Button9" runat="server" Height="30px" Width="100px" Text="通知管理" BackColor="#99CCFF" OnClick="Button9_Click" />
            &nbsp;<asp:Button ID="Button10" runat="server" Height="30px" Width="100px" Text="位置展示" BackColor="#99CCFF" OnClick="Button10_Click" />
        </div>
        <div style="height: 25px"></div>
        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" Width="100%" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" OnPageIndexChanging="GridView_PageIndexChanging" OnRowDataBound="GridView_RowDataBound" OnRowDeleting="GridView_RowDeleting" OnRowEditing="GridView_RowEditing">
            <AlternatingRowStyle HorizontalAlign="Center" BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="用户名">
                    <ItemTemplate>
                        <asp:Label ID="lblloginID" runat="server" Text='<%#Eval("loginID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公司名称">
                    <ItemTemplate>
                        <asp:Label ID="lblcompanyName" runat="server" Text='<%#Eval("companyName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="设备串号">
                    <ItemTemplate>
                        <asp:Label ID="lblIMEI" runat="server" Text='<%#Eval("pIMEI") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="记录号">
                    <ItemTemplate>
                        <asp:Label ID="lblRecordNO" runat="server" Text='<%#Eval("RecordNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="检测时间">
                    <ItemTemplate>
                        <asp:Label ID="lblcheckTime" runat="server" Text='<%#Eval("checkTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="温度(℃)">
                    <ItemTemplate>
                        <asp:Label ID="lblTemperature" runat="server" Text='<%#Eval("Temperature") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="湿度(%)">
                    <ItemTemplate>
                        <asp:Label ID="lblHumidity" runat="server" Text='<%#Eval("Humidity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="地图展示">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CausesValidation="false" Text="地图">地图展示</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnDel" runat="server" Text="删除" CommandArgument='<%#Eval("RecordID") %>' CommandName="Delete"></asp:LinkButton>
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
        <br /><br /> 
    </div>
</asp:Content>

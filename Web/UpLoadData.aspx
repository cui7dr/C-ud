<%@ Page Title="文件上传" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="UpLoadData.aspx.cs" Inherits="UpLoadData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="JS/uploadify-v2.1.0/example/css/default.css" rel="Stylesheet" type="text/css" />
    <link href="JS/uploadify-3.1/uploadify.css" rel="Stylesheet" type="text/css" />
    <script src="JS/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="JS/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="JS/uploadify-3.1/jquery.uploadify-3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var auth = "<%=Request.Cookies[FormsAuthentication.FormsCookieName] == null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
        var ASPSESSID = "<%=Session.SessionID %>";
        $(document).ready(function () {
            $("uploadify").uploadify({
                'swf': 'JS/uploadify-3.1/uploadify.swf',
                'uploader': 'UploadHandler.ashx',
                'auto': false,
                'buttonText': '选    择',
                'removeCompleted': true,
                'multi': true,
                'removeTimeout': 1,
                'fileSizeLimit': '50MB',
                'fileTypeDesc': '请选择 wav 格式文件',
                'fileTypeExts': '*.wav',
                'requeueErrors': true,
                'queueID': 'fileQueue',
                'formData': { 'ASPSESSID': ASPSESSID, 'AUTHID': auth },
                'onSelect': function (file) {
                    $('#uploadify').uploadifySettings('formData', { 'ASPSESSID': ASPSESSID, 'AUTHID': AUTH });
                    alert(formData);
                },
                //当队列中的所有文件全部完成上传时触发
                'onQueueComplete': function (states) {
                    alert('成功上传的文件数：' + states.uploadsSuccessful + '个；\n出错的文件数：' + states.uploadsErrored + '个。。。');
                },
                'onUploadSuccess': function (file, data, response) {
                    if (data.indexOf('提示') > -1) {
                        alert(data);
                        return false;
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-left: auto; margin-right: auto; margin-top: 28px; width: 1004px">
        <br />
        <div style="margin-left: auto; margin-right: auto" align="center">
            <asp:Label ID="Label1" Text="文件上传" ForeColor="Gray" Font-Size="32px" runat="server"></asp:Label>
        </div>
        <div style="margin-left: auto; margin-right: auto; margin-top: 28px; width: 60%">
            <div align="right" style="font-size: 16px">
                <a href="UpLoadData.aspx">重新上传</a>
                &nbsp;&nbsp;|&nbsp;&nbsp;
                <a href="Index.aspx">返回主页</a>
            </div>
            <div style="border-color: #C0C0C0; margin-top: 21px">
                <asp:Label ID="Label10" Text="Step-1" ForeColor="Blue" Font-Size="24px" runat="server"></asp:Label>
                <br />
                <br />
                <div align="center">
                    <asp:Label ID="Label11" Text="数据文件上传" ForeColor="Blue" Font-Size="Larger" runat="server"></asp:Label>
                </div>
                <br />
                <br />
                &nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload" runat="server" Height="28px" Width="500px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnUpload" BackColor="White" BorderColor="White" Height="28px" Width="70px" Text="上传" runat="server" OnClick="btnUpload_Click" />
                <br />
                &nbsp;&nbsp;
                <asp:Label ID="lblUploadInfo" Text="Label" ForeColor="Red" Visible="false" runat="server"></asp:Label>
            </div>
            <br />
            <div>
                <p>
                    <asp:Label ID="Label2" Text="注意：" ForeColor="Red" Font-Size="Large" runat="server"></asp:Label>
                </p>
                <p align="center">
                    <asp:Label ID="Label3" Text="1.只允许上传指定格式文件（*.txt）" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                </p>
                <p align="center">
                    <asp:Label ID="Label4" Text="2.只允许上传指定大小文件（4MB 以内）" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                </p>
                <p align="center">
                    <asp:Label ID="Label5" Text="3.请您尽量不要重复上传包含同一条数据的文件" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                </p>
                <br />
            </div>
        </div>
        <br />
        <div id="Div1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="divStep2" runat="server" style="margin-left: auto; margin-right: auto; width: 50%; margin-top: 28px">
                        <div style="margin-left: auto; margin-right: auto">
                            <div id="Div2" runat="server">
                                <asp:Label ID="Label6" Text="Step-2" ForeColor="Blue" Font-Size="24px" runat="server"></asp:Label>
                                <br />
                            </div>
                            <br />
                            <br />
                            <div align="center">
                                <asp:Label ID="Label7" Text="音频文件上传" ForeColor="Blue" Font-Size="Larger" runat="server"></asp:Label>
                            </div>
                            <br />
                            <br />
                            <div id="fileQueue"></div>
                            <div>
                                <input type="file" name="uploadify" id="uploadify" style="width: 412px; height: 28px" />
                                &nbsp;&nbsp;
                                <a href="javascript:$('#uploadify').uploadify('upload','*')">
                                    <button style="width: 70px; height: 20px">上传</button></a>
                            </div>
                            <br />
                            <div align="center" style="font-size:14pt">
                                <a href="javascript:$('#uploadify').uploadify('cancel','*')">取消上传</a>
                                &nbsp;&nbsp;|&nbsp;&nbsp;
                                <a href="Index.aspx">返回主页</a>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="divStatus" runat="server" style="margin-left: auto; margin-right: auto; width: 50%; margin-top: 14px">
                <p>
                    <asp:Label ID="strStatus" runat="server" Font-Names="宋体" Font-Bold="true" Font-Size="9pt" ForeColor="Red" Visible="false" BorderStyle="None" BackColor="White"></asp:Label>
                </p>
            </div>
        </div>
        <br />
    </div>
</asp:Content>

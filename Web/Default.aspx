<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<html>
<head id="Head1" runat="server">
<title>用户登录</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="Proficient Login Form Responsive, Login form web template,Flat Pricing tables,Flat Drop downs  Sign up Web Templates, Flat Web Templates, Login signup Responsive web template, Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyEricsson, Motorola web design" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<!-- fonts -->
<link href="http://fonts.googleapis.com/css?family=Poiret+One" rel="stylesheet"/>
<link href="http://fonts.googleapis.com/css?family=Raleway:100,200,300,400,500,600,700,800,900" rel="stylesheet"/>
<!-- /fonts -->
<!-- css -->
<link href="CSS/font-awesome.min.css" rel="stylesheet" type="text/css" media="all" />
<link href="CSS/style.css" rel="stylesheet" type='text/css' media="all" />
<!-- /css -->
</head>
<body>
    <form id="form" runat="server">
<div class="overlay">
	<h1 class="agileits">局 放 检 测 云 分 析 系 统</h1>
	<div class="content-w3ls agileits">
		<img src="Images/HB.png" alt="" class="user-wthree"/>
		<form action="#" method="post">
			<div class="form-group1-agile agile-info">
				<!-- <input  runat="server" type="text" name="userid" value="请输入用户名" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Username';}"/> -->
                <asp:TextBox ID="txtName" runat="server" Text="请输入用户名" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Username';}"></asp:TextBox>
			</div>
			<div class="form-group2-agile agile-info">
				<!-- <input   runat="server" type="password" name="psw" value="请输入密码" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}"/>	 -->
                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Text="请输入密码" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}"></asp:TextBox>
			</div>
            <asp:Button ID="btnLogin" runat="server" Text="登录" onclick="BtnLogin_Click" />
			
		</form>
	</div>
	<div class="footer-w3ls w3-agile">
		<p class="w3-agileits">© Copyright 2018 版权所有  <a href="http://www.miitbeian.gov.cn/" target="_blank">豫ICP备09018549号</a></p>
	</div>	
</div>
<video autoplay loop id="video-background" poster="Video/city.jpg" muted>
  <%-- <source src="Video/city.mp4" type="video/mp4">--%>
</video>	
<!-- js -->
<script src="JS/modernizr.min.js" type="text/javascript"></script>
<!-- /js -->
        </form>
</body>
</html>


      

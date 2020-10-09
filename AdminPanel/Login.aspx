<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AdminPanel.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<head>
    <title>ورود به پنل مدیریت سیستم</title>
    <link href="Bootstrap-3.2.0/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Bootstrap-3.2.0/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Bootstrap-3.2.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="Bootstrap-3.2.0/css/simple-line-icons.css" rel="stylesheet" />
    <link href="Bootstrap-3.2.0/css/animate.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/bootstrap-rtl/3.2.0-rc2/css/bootstrap-rtl.min.css" />--%>
    <link href="Bootstrap-3.2.0/css/adminPanel.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
   
    <div id="loginMain">
         <div class="row" >
            <div class="col-md-10 col-md-offset-1">
          <h4>ورود به پنل مدیریت سیستم</h4>
            </div>
             
        </div>
        <hr/>
        <div class="row" >
            <div class="col-md-2" align="left">
                <label>نام کاربری:</label>
            </div>
            <div class="col-md-10" >
                <asp:TextBox runat="server" ID="txtUser"></asp:TextBox>
            </div>
            
        </div>
        <div class="row">
            <div class="col-md-2" align="left">
                <label>کلمه عبور:</label>
            </div>
            <div class="col-md-10" >
                <asp:TextBox TextMode="Password" runat="server" ID="txtPassword"></asp:TextBox>
            </div>
            
        </div>
        
         <div class="row">
            <div class="col-md-2" align="left">
                <label>عبارت امنیتی:</label>
            </div>
            <div class="col-md-10" >
                <telerik:RadCaptcha ID="RadCaptcha1" runat="server" ErrorMessage="عبارت امنیتی صحیح نیست" CaptchaTextBoxLabel="" CaptchaImage-TextChars="Numbers"></telerik:RadCaptcha>
            </div>
            
        </div>

        <div class="row">
            <div class="col-md-10 col-md-offset-2" >
                <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_OnClick" Text="ورود" CssClass="btn btn-black"></asp:Button>
            </div>
           
        </div>
        <div class="row">
            <div class="col-md-10 col-md-offset-2" >
                <div class="label label-danger" runat="server" id="lblError"></div>
            </div>
        </div>
    </div>
        </form>

</body>
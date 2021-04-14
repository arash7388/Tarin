<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tashim.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<head>
    <title>ورود به سیستم</title>
    <link href="Content/css/bootstrap-rtl.css" rel="stylesheet" />
    <link href="Content/css/custom.css" rel="stylesheet" />
</head>
<body style="background-image: url(/Images/bg1.jpg); background-repeat: repeat">
    <form runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </scripts>
        </telerik:RadScriptManager>

        <div id="loginMain">
            <div class="row">
                 <div class="col-md-2" style="margin-right: 80px;margin-top: 10px;">
                    <%--<asp:Image runat="server" ID="logo" ImageUrl="Images/logo.png" CssClass="logo"/>--%>
                </div>

                <div class="col-md-9 " style="margin-right: 0px; margin-top: 35px">
                    <h4 style="font-family: BKoodak">ورود به سیستم </h4>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-2" align="left">
                    <label>نام کاربری:</label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtUser"></asp:TextBox>
                </div>

            </div>
            <div class="row">
                <div class="col-md-2" align="left">
                    <label>کلمه عبور:</label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox TextMode="Password" runat="server" ID="txtPassword"></asp:TextBox>
                </div>

            </div>

            <div class="row">
                <div class="col-md-2" align="left">
                    <label>عبارت امنیتی:</label>
                </div>
                <div class="col-md-10">
                    <telerik:RadCaptcha ID="RadCaptcha1" runat="server" ErrorMessage="عبارت امنیتی صحیح نیست" CaptchaTextBoxLabel="" CaptchaImage-TextChars="Numbers"></telerik:RadCaptcha>
                </div>

            </div>

            <div class="row">
                <div class="col-md-10 col-md-offset-2">
                    <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_OnClick" Text="ورود" CssClass="btn btn-info"></asp:Button>
                </div>

            </div>
            <br/>
            <br/>

            <div class="row">
                <div class="col-md-9 col-md-offset-2">
                    <asp:Label runat="server" ID="lblError" class="alert alert-danger" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </form>

</body>

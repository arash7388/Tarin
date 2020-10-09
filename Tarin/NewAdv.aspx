<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="NewAdv.aspx.cs" Inherits="Tarin.NewAdv" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/css/newAdv.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <div class="container">
        <%--<asp:UpdatePanel runat="server" ID="updPanelCats">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-6" runat="server" id="divCats">
                        <div class="row">
                            <div class="col-sm-12">
                                برای ثبت آگهی جدید ابتدا باید موضوع مورد نظرتان را انتخاب نمایید
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                موضوع:
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <asp:DropDownList runat="server" ID="drpMainCat" OnSelectedIndexChanged="drpCat_OnSelectedIndexChanged" AutoPostBack="True" />
                            </div>
                        </div>

                    </div>
                    <div class="col-sm-6">
                    </div>

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="drpMainCat" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>--%>

        <div class="row">
            <div class="col-sm-12">
                <h4>برای ثبت آگهی جدید ابتدا باید موضوع مورد نظرتان را انتخاب نمایید
                </h4>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12" runat="server" id="divCats">
                <div runat="server" id="divMain"></div>
            </div>
        </div>

        <br />
        <br />

        <div class="container" runat="server" visible="False" id="rowProps">
            <div class="row">
                <div class="col-sm-12">
                    <h4>تعیین مشخصات</h4>
                    <hr class="hrBlue" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox runat="server" ID="t1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="t1" ErrorMessage="***" ValidationGroup="saveValidation"></asp:RequiredFieldValidator>
                            <div runat="server" id="divPropsContainer">
                                <div runat="server" id="divProps"></div>
                            </div>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-xs-3 col-sm-3" align="left" style="padding-left: 25px">
                                تصویر:
                            </div>
                            <div class="col-xs-9 col-sm-6">
                                <telerik:RadAsyncUpload runat="server" ID="asyncUploadPic"
                                    HideFileInput="true"
                                    EnableViewState="True"
                                    MultipleFileSelection="Disabled"
                                    AllowedFileExtensions=".jpeg,.jpg,.png" />
                                <span class="" runat="server" id="allowedExts">انواع مجاز (<%= GetUploadAllowedFileExtensions() %>)
                                </span>
                            </div>

                        </div>
                        
                    </div>
                    <br/>
                    <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left" style="padding-left: 25px">
                                
                            </div>
                            <div class="col-xs-9 col-sm-6">
                                <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info btn100" Text="ذخیره" OnClick="btnSave_OnClick" ValidationGroup="saveValidation" CausesValidation="True"/>
                            </div>
                    </div>

                </div>

                <div class="col-sm-6">
                    <asp:Panel runat="server" ID="panelCommonProps">
                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                محله:
                            </div>
                            <div class="col-xs-9 col-sm-6">
                                <asp:TextBox runat="server" ID="txtArea" CssClass="form-control "></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="الزامی" CssClass="propReqValidator" ControlToValidate="txtArea" ValidationGroup="saveValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                عنوان:
                            </div>
                            <div class="col-xs-9 col-sm-6">
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control "></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                ایمیل:
                            </div>
                            <div class="col-xs-9 col-sm-6">
                                <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control "></asp:TextBox>
                                <asp:CheckBox runat="server" ID="chkHideEmail" Text="آدرس ایمیل مخفی بماند"></asp:CheckBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                شماره تماس:
                            </div>
                            <div class="col-xs-9 col-sm-6">
                                <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control "></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                توضیحات:
                            </div>
                            <div class="col-xs-9 col-sm-9">
                                <asp:TextBox runat="server" ID="TextBox4" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

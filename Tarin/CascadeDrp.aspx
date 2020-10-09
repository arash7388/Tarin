<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CascadeDrp.aspx.cs" Inherits="Tarin.CascadeDrp" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
    <link href="Content/css/newAdv.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <input type="hidden" name="catIdHF" id="catIdHF" runat="server" />
    <input type="hidden" name="SelectedCatHF" id="SelectedCatHF" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container">

        <div class="row">
            <div class="col-sm-12">
                <h4>برای ثبت آگهی یا خدمت جدید ابتدا باید دسته مرتبط با آگهی یا خدمت را انتخاب نمایید
                </h4>
            </div>
        </div>

        <div runat="server" id="divAllCats">

            <div class="row paddingTop5">
                <div class="col-sm-6">
                    <asp:DropDownList ID="drpCat1" runat="server" Width="150" onchange="VisibleDDL('2');" CssClass="drpCat">
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="casDrp1" TargetControlID="drpCat1" PromptText="انتخاب کنید..."
                        PromptValue="" ServicePath="WebService1.asmx" ServiceMethod="GetMainCats" runat="server"
                        Category="Id" LoadingText="در حال بارگذاری..." />
                </div>
            </div>

            <div class="row paddingTop5">
                <div class="col-sm-6">
                    <asp:DropDownList ID="drpCat2" runat="server" Width="150" CssClass="drpCatStyle" onchange="VisibleDDL('3');">
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="casDrp2" TargetControlID="drpCat2" PromptText="انتخاب کنید..."
                        PromptValue="" ServicePath="WebService1.asmx" ServiceMethod="GetSubCats" runat="server"
                        Category="Id" ParentControlID="drpCat1" LoadingText="در حال بارگذاری..." />
                </div>
            </div>

            <div class="row paddingTop5">
                <div class="col-sm-6">

                    <asp:DropDownList ID="drpCat3" runat="server" Width="150" CssClass="drpCatStyle" onchange="VisibleDDL('4');">
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="casDrp3" TargetControlID="drpCat3" PromptText="انتخاب کنید..."
                        PromptValue="" ServicePath="WebService1.asmx" ServiceMethod="GetSubCats" runat="server"
                        Category="Id" ParentControlID="drpCat2" LoadingText="در حال بارگذاری..." />
                </div>
            </div>

            <div class="row paddingTop5">
                <div class="col-sm-6">
                    <asp:DropDownList ID="drpCat4" runat="server" Width="150" CssClass="drpCatStyle" onchange="VisibleDDL('5');">
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="casDrp4" TargetControlID="drpCat4" PromptText="انتخاب کنید..."
                        PromptValue="" ServicePath="WebService1.asmx" ServiceMethod="GetSubCats" runat="server"
                        Category="Id" ParentControlID="drpCat3" LoadingText="در حال بارگذاری..." />
                </div>
            </div>

            <div class="row paddingTop5">
                <div class="col-sm-6">
                    <asp:DropDownList ID="drpCat5" runat="server" Width="150" CssClass="drpCatStyle" onchange="VisibleDDL('6');">
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="casDrp5" TargetControlID="drpCat5" PromptText="انتخاب کنید..."
                        PromptValue="" ServicePath="WebService1.asmx" ServiceMethod="GetSubCats" runat="server"
                        Category="Id" ParentControlID="drpCat4" LoadingText="در حال بارگذاری..." />
                </div>
            </div>

        </div>

        <div class="row" runat="server" id="divTravercedDats" visible="False">
            <div class="col-sm-12">
                <asp:Label runat="server" ID="lblTraversedCats"></asp:Label>
            </div>
        </div>
        <div class="container" runat="server" id="rowProps" style="visibility: hidden">
            <div class="row">
                <div class="col-sm-12">
                    <h4>تعیین مشخصات</h4>
                    <hr class="hrBlue" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div runat="server" id="divProps"></div>
                    <br />
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
                                    AllowedFileExtensions=".jpeg,.jpg,.png" 
                                    MaxFileSize="512000"/>
                                <span class="" runat="server" id="allowedExts">انواع مجاز (<%= GetUploadAllowedFileExtensions() %>)
                                </span>
                            </div>

                        </div>

                    </div>
                    <br />


                </div>

                <div class="col-sm-6">
                    <asp:Panel runat="server" ID="panelCommonProps">
                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                محله:
                            </div>
                            <div class="col-xs-7 col-sm-6">
                                <asp:DropDownList runat="server" ID="drpArea" CssClass="form-control padding0" />

                            </div>
                            <div class="col-xs-2 col-sm-3">
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="الزامی" CssClass="propReqValidator" ControlToValidate="drpArea" ValidationGroup="saveValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                عنوان:
                            </div>
                            <div class="col-xs-7 col-sm-6">
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control "></asp:TextBox>
                            </div>
                            <div class="col-xs-2 col-sm-3">
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="الزامی" CssClass="propReqValidator" ControlToValidate="txtTitle" ValidationGroup="saveValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                ایمیل:
                            </div>
                            <div class="col-xs-7 col-sm-6">
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control "></asp:TextBox>
                                <asp:CheckBox runat="server" ID="chkHideEmail" Text="آدرس ایمیل مخفی بماند"></asp:CheckBox>
                            </div>
                            <div class="col-xs-2 col-sm-3">
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="الزامی" CssClass="propReqValidator" ControlToValidate="txtEmail" ValidationGroup="saveValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                شماره تماس:
                            </div>
                            <div class="col-xs-7 col-sm-6">
                                <asp:TextBox runat="server" ID="txtTel" CssClass="form-control "></asp:TextBox>
                            </div>
                            <div class="col-xs-2 col-sm-3">
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="الزامی" CssClass="propReqValidator" ControlToValidate="txtTel" ValidationGroup="saveValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-3 col-sm-3" align="left">
                                توضیحات:
                            </div>
                            <div class="col-xs-7 col-sm-7">
                                <asp:TextBox runat="server" ID="txtDesc" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-xs-2 col-sm-2">
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="الزامی" CssClass="propReqValidator" ControlToValidate="txtDesc" ValidationGroup="saveValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-6">
                    <div class="col-xs-3 col-sm-3" align="left" style="padding-left: 25px">
                    </div>
                    <div class="col-xs-9 col-sm-6">
                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info btn100" Text="ذخیره" OnClick="btnSave_OnClick" ValidationGroup="saveValidation" CausesValidation="True" />
                    </div>
                </div>
                <div class="col-sm-6">
                </div>

            </div>
        </div>

        <script type="text/javascript">
            function VisibleDDL(nextIndex) {
                debugger;

                if (<%=(Page.IsPostBack).ToString().ToLower()%>)
                return;

            if (nextIndex == 2) {
                    currentddl = $("#<%= drpCat1.ClientID %>");
                    id = currentddl[0].options[currentddl[0].selectedIndex].value;
                    ddl = $("#<%= drpCat2.ClientID %>");
                }
                else
                    if (nextIndex == 3) {
                        currentddl = $("#<%= drpCat2.ClientID %>");
                        id = currentddl[0].options[currentddl[0].selectedIndex].value;
                        ddl = $("#<%= drpCat3.ClientID %>");
                    }
                    else
                        if (nextIndex == 4) {
                            currentddl = $("#<%= drpCat3.ClientID %>");
                            id = currentddl[0].options[currentddl[0].selectedIndex].value;
                            ddl = $("#<%= drpCat4.ClientID %>");
                        }
                        else
                            if (nextIndex == 5) {
                                currentddl = $("#<%= drpCat4.ClientID %>");
                                id = currentddl[0].options[currentddl[0].selectedIndex].value;
                                ddl = $("#<%= drpCat5.ClientID %>");
                        }


            $.ajax({
                type: "POST",
                url: "WebService1.asmx/CatHasSubCat",
                data: "{'catId':'" + id + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                error: function (jqXHR, textStatus, errorThrown) {
                    alert('خطا در سیستم ');
                },
                success: function (msg) {
                    debugger;
                    if (msg.d == true) {
                        ddl[0].style.visibility = "visible";
                        $("#<%= rowProps.ClientID %>")[0].style.visibility = "hidden";
                    }
                    else {
                        debugger;
                        ddl[0].style.visibility = "hidden";
                        
                        var cId = document.getElementById('<%= catIdHF.ClientID%>');

                        if (cId)
                            cId.value = id;
                        
                        __doPostBack('<%= this.UniqueID %>', '');
                    }
                }
            });
        }
        </script>

    </div>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
</asp:Content>

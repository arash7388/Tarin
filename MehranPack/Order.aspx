<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="MehranPack.Order" %>
<%@ Import Namespace="Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:ScriptManager runat="server" ID="srcManager"></asp:ScriptManager>
    <div class="container" id="orderContainer">
        <div class="row">
            <div class="col-sm-12">
                <h3 runat="server" ID="h3Title"></h3>
                <hr class="hrBlue" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-2" align="left">
                مشتری:
            </div>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="drpCustomer" CssClass="form-control drpNormal "></asp:DropDownList>
            </div>

            <div class="col-sm-2" align="left">
                نام کار:
            </div>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="txtWorkTitle" CssClass="form-control txtNormal"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2" align="left">
                نوع جنس:
            </div>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="drpProductType" CssClass=" form-control drpNormal"></asp:DropDownList>
            </div>

            <div class="col-sm-2">
            </div>

            <div class="col-sm-4">
                <div class="panel panel-default">
                    <div class="panel-body-custom">
                        <div class="row">
                            <div class="col-sm-6">
                                <asp:CheckBox runat="server" ID="chkIsForSent" CssClass="txtNormal" Text="ارسالی"></asp:CheckBox>
                            </div>
                            <div class="col-sm-6">
                                <asp:CheckBox runat="server" ID="chkIsExisted" CssClass="txtNormal" Text="موجود"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2" align="left">
                گرماژ:
            </div>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="txtGrammage" CssClass="form-control txtNormal"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2" align="left">
                مقدار مقوا:
            </div>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="txtCartonCount" CssClass="form-control txtNormal"></asp:TextBox>
            </div>

            <div class="col-sm-2" align="left">
                ابعاد مقوا:
            </div>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="txtCartonSize" CssClass="form-control txtNormal"></asp:TextBox>
            </div>

            <div class="col-sm-2" align="left">
                ابعاد برش:
            </div>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="txtCutSize" CssClass="form-control txtNormal"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2" align="left">
                تیراژ:
            </div>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="txtTiraj" CssClass="form-control txtNormal"></asp:TextBox>
            </div>

        </div>
        <div class="row">
            <div class="col-sm-2">
            </div>

            <div class="col-sm-4">
                <div class="panel panel-default">
                    <div class="panel-body-custom">
                        <div class="row">
                            <div class="col-sm-2">
                                زینک
                            </div>

                            <div class="col-sm-5">
                                <asp:CheckBox runat="server" ID="chkZincExisted" CssClass="" Text="موجود"></asp:CheckBox>
                            </div>
                            <div class="col-sm-5">
                                <asp:CheckBox runat="server" ID="chkZincSent" CssClass="" Text="ارسالی"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-sm-2" align="left">
                توضیحات زینک:
            </div>
            <div class="col-sm-10">
                <asp:TextBox runat="server" ID="txtZincDesc" CssClass="form-control txtNormal"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2" align="left">
                تعداد رنگ:
            </div>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="txtColorCount" CssClass="form-control txtNormal"></asp:TextBox>
            </div>

            <div class="col-sm-2" align="left">
                نوع رنگ:
            </div>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="txtColorType" CssClass="form-control txtNormal"></asp:TextBox>
            </div>

            <div class="col-sm-2" align="left">
                تعداد دستگاه:
            </div>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="txtMachineCount" CssClass="form-control txtNormal"></asp:TextBox>
            </div>

        </div>

        <div class="row">
            <div class="col-sm-2" align="left">
                توضیحات دستگاه:
            </div>
            <div class="col-sm-10">
                <asp:TextBox runat="server" ID="txtMachineDesc" CssClass="form-control txtNormal"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2">
            </div>

            <div class="col-sm-4">
                <div class="panel panel-default">
                    <div class="panel-body-custom">
                        <div class="row">
                            <div class="col-sm-2">
                                ورنی
                            </div>

                            <div class="col-sm-5">
                                <asp:CheckBox runat="server" ID="chkVernyDark" Text="مات" CssClass=""></asp:CheckBox>
                            </div>
                            <div class="col-sm-5">
                                <asp:CheckBox runat="server" ID="chkVernyClear" Text="براق" CssClass=""></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-2">
            </div>

            <div class="col-sm-4">
                <div class="panel panel-default">
                    <div class="panel-body-custom">
                        <div class="row">
                            <div class="col-sm-2">
                                یووی
                            </div>

                            <div class="col-sm-5">
                                <asp:CheckBox runat="server" ID="chkUVDark" Text="مات" CssClass=""></asp:CheckBox>
                            </div>
                            <div class="col-sm-5">
                                <asp:CheckBox runat="server" ID="chkUVClear" Text="براق" CssClass=""></asp:CheckBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row">

            <div class="col-sm-2">
            </div>

            <div class="col-sm-4">
                <div class="panel panel-default">
                    <div class="panel-body-custom">
                        <div class="row">
                            <div class="col-sm-2" align="left">
                                سلفون
                            </div>
                            <div class="col-sm-5">
                                <asp:CheckBox runat="server" ID="chkCelefonDark" Text="مات" CssClass=""></asp:CheckBox>
                            </div>
                            <div class="col-sm-5">
                                <asp:CheckBox runat="server" ID="chkCelefonClear" Text="براق" CssClass=""></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-2">
            </div>

            <div class="col-sm-2">
                <asp:CheckBox runat="server" ID="chkLabChasb" Text="لب چسب" CssClass=""></asp:CheckBox>
            </div>
        </div>


        <div class="row">
            <div class="col-sm-2">
            </div>

            <div class="col-sm-10">
                <div class="panel panel-default">
                    <div class="panel-body-custom">
                        <div class="row">
                            <div class="col-sm-3">
                                <asp:CheckBox runat="server" Text="قالب تیغ" ID="chkGhalebTigh" />
                            </div>

                            <div class="col-sm-3">
                                <asp:CheckBox runat="server" Text="کلیشه برجسته" ID="chkKlisheBarjesteh" />
                            </div>

                            <div class="col-sm-3">
                                <asp:CheckBox runat="server" Text="نیم تیغ" ID="chkNimTigh" />
                            </div>

                            <div class="col-sm-3">
                                <asp:CheckBox runat="server" Text="خط تا" ID="chkCutLine" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-sm-2" align="left">
                کد قالب:
            </div>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="txtGhalebCode" CssClass="form-control txtNormal"></asp:TextBox>
            </div>

            <div class="col-sm-2" align="left">
                کناره کار:
            </div>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="txtKenareh" CssClass="form-control txtNormal"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2" align="left">
                توضیحات:
            </div>
            <div class="col-sm-10">
                <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control txtNormal"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2" align="left">
                عکس برش:
            </div>
            <div class="col-sm-6">
                <telerik:RadUpload ID="radUploalCutImage" runat="server" ControlObjectsVisibility="None">
                </telerik:RadUpload>
            </div>
            <div class="col-sm-4">
                <asp:CheckBox runat="server" ID="chkIsFactored" Text="فاکتور شد"/>
            </div>
        </div>

        <hr />

        <div class="row">
            <div class="col-sm-10 col-sm-offset-2">
                <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="btn btn-info btn-standard" OnClick="btnSave_Click"></asp:Button>
                <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="btn btn-info btn-standard" OnClick="btnCancel_Click"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>


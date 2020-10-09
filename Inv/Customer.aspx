<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="Inv.Customer" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <h3>مشتری</h3>
                <hr class="hrBlue"/>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-3 col-sm-2" align="left">
                کد:
            </div>
            <div class="col-xs-9 col-sm-4">
                <asp:TextBox runat="server" ID="txtCode" CssClass="form-control "></asp:TextBox>
            </div>

        </div>

        <div class="row">
            <div class="col-xs-3 col-sm-2" align="left">
                نام :
            </div>
            <div class="col-xs-9 col-sm-4">
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control "></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-3 col-sm-2" align="left">
                تلفن :
            </div>
            <div class="col-xs-9 col-sm-4">
                <asp:TextBox runat="server" ID="txtTel" CssClass="form-control "></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-3 col-sm-2" align="left">
                موبایل :
            </div>
            <div class="col-xs-9 col-sm-4">
                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control "></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-3 col-sm-2" align="left">
                ایمیل :
            </div>
            <div class="col-xs-9 col-sm-4">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control "></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-3 col-sm-2" align="left">
                آدرس :
            </div>
            <div class="col-xs-9 col-sm-8">
                <asp:TextBox runat="server" ID="txtAdr" CssClass="form-control "></asp:TextBox>
            </div>
        </div>
        
        <hr />

        <div class="row">
            <div class="col-xs-2 visible-xs"></div>
            <div class="col-xs-10 col-sm-offset-2">
                <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="btn btn-info btn-standard" OnClick="btnSave_Click"></asp:Button>
                <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="btn btn-info btn-standard" OnClick="btnCancel_Click"></asp:Button>
            </div>
        </div>
        
        <div class="row">
        <div class="col-md-10 col-md-offset-2">
            <div class="label label-danger" runat="server" id="lblResult"></div>
        </div>
    </div>
    </div>
</asp:Content>


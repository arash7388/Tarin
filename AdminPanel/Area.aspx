<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="Area.aspx.cs" Inherits="AdminPanel.Area" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    
    <div class="row">
        <div class="col-md-12">
            <h3>منطقه ها</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2" align="left">
            شهر:
        </div>
        <div class="col-md-10">
            <asp:DropDownList runat="server" ID="drpCity" CssClass="dropdown "></asp:DropDownList>
        </div>

    </div>

    <div class="row">
        <div class="col-md-2" align="left">
            نام :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
        </div>
    </div>

    <hr />

    <div class="row">
        <div class="col-md-10 col-md-offset-2">
            <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="btn btn-black" OnClick="btnSave_Click"></asp:Button>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10 col-md-offset-2">
            <div class="label label-danger" runat="server" id="lblResult"></div>
        </div>
    </div>

</asp:Content>

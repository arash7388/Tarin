<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="~/Rework.aspx.cs" Inherits="MehranPack.Rework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3 runat="server" id="h3Title">دوباره کاری </h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-1 col-sm-2">
            ACode:
        </div>
        <div class="col-md-3 col-sm-4">
            <asp:DropDownList runat="server" ID="drpACode" Height="23" Width="150"></asp:DropDownList>
        </div>
        <div class="col-md-8 col-sm-6">
            <asp:Label runat="server" ID="lblProductName" Height="23" Width="150"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="col-md-1 col-sm-2">
            اوپراتور:
        </div>
        <div class="col-md-3 col-sm-4">
            <asp:DropDownList runat="server" ID="drpOp" Height="23" Width="150"></asp:DropDownList>
        </div>
    </div>

    <div class="row">
        <div class="col-md-1 col-sm-2">
            علت:
        </div>
        <div class="col-md-3 col-sm-4">
            <asp:DropDownList runat="server" ID="drpReason" Height="23" Width="250"></asp:DropDownList>
        </div>
    </div>

    <div class="row">
        <div class="col-md-1 col-sm-2">
            توضیحات:
        </div>
        <div class="col-md-7 col-sm-7">
            <asp:TextBox runat="server" ID="txtDesc" Height="50" Width="500"></asp:TextBox>
        </div>
    </div>


    <hr />

    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="btn btn-black" OnClick="btnSave_Click"></asp:Button>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="label label-info lblResult" runat="server" id="lblResult"></div>
        </div>
    </div>
</asp:Content>


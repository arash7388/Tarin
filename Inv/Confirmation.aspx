﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirmation.aspx.cs" Inherits="Inv.Confirmation" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div ID="confirmArea" runat="server">
        <div class="row" style="margin-bottom: 200px"></div>
        <div class="row">
            <div class="col-sm-10">
                <asp:Label runat="server" ID="lblMessage" CssClass="lblMedium"></asp:Label>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-sm-10">
                <asp:Button runat="server" ID="btnAccept" Text="بله" CssClass="btn btn-info" OnClick="btnAccept_Click" />
                <asp:Button runat="server" ID="btnCancel" Text="خیر" CssClass="btn btn-info" OnClick="btnCancel_OnClick" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-10">
            </div>
        </div>
    </div>
</asp:Content>
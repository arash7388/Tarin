<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="Confirmation.aspx.cs" Inherits="AdminPanel.Confirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <link href="Bootstrap-3.2.0/css/adminPanel.css" rel="stylesheet" />
    <div class="row">
       <div class="col-md-10">
           <asp:Label runat="server" ID="lblMessage" CssClass="lblMedium"></asp:Label>
       </div> 
    </div>
    <hr/>
    <div class="row">
        <div class="col-md-10">
            <asp:Button runat="server" ID="btnAccept" Text="بله" CssClass="btn btn-danger btn-standard" OnClick="btnAccept_Click"/>
            <asp:Button runat="server" ID="btnCancel" Text="خیر" CssClass="btn btn-info btn-standard" OnClientClick="JavaScript:window.history.back(1); return false;"/>
        </div>
    </div>
    <br/>
    <div class="row">
        <div class="col-md-10">
            <asp:Label runat="server" ID="lblInfo" CssClass="lblMedium"></asp:Label>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AdminPanelMain.aspx.cs" Inherits="AdminPanel.AdminPanelMain" %>
<%@ Import Namespace="Repository.Entity.Domain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <div class="col-md-10">
            <h4><%=((User)Session["Username"]).FriendlyName%> عزیز</h4>
            <h4>به پنل مدیریت سیستم نوین طراحان خوش آمدید</h4>
        </div>
    </div>
    </asp:Content>

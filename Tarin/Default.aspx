<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tarin.Default" MasterPageFile="~/Main.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row un-pad">
        <div ng-view>
        </div>
    </div>
  
</asp:Content>

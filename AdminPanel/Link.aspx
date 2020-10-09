<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="Link.aspx.cs" Inherits="AdminPanel.Link" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
     
    <div class="row" >
         <div class="col-md-2" align="left">
            عنوان لینک:
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
        </div>
       
    </div>
    
     <div class="row" >
         <div class="col-md-2" align="left">
            آدرس لینک :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtHref"></asp:TextBox>
        </div>
        
    </div>
    
    <hr/>
     <div class="row" >
        <div class="col-md-10 col-md-offset-2">
         <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="btn btn-black" OnClick="btnSave_Click"></asp:Button>
        </div>
    </div>
    <div class="row" >
        <div class="col-md-10 col-md-offset-2">
         <div class="label label-danger" runat="server" id="lblResult"></div>
        </div>
    </div>
</asp:Content>

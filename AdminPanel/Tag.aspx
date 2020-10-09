<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="Tag.aspx.cs" Inherits="AdminPanel.Tag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>تگ ها</h3>
        </div>
    </div>
     
    <div class="row" >
         <div class="col-md-2" align="left">
            کد :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtCode"></asp:TextBox>
        </div>
        
    </div>
    
    <div class="row" >
          <div class="col-md-2" align="left">
            نام :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
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

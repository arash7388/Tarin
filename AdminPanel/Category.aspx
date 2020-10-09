<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="AdminPanel.Category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
     <div class="row">
        <div class="col-md-12">
            <h3>گروه ها</h3>
        </div>
    </div>

     <div class="row" >
         <div class="col-md-2" align="left">
            کد گروه:
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtCode" Height="23" Width="303"></asp:TextBox>
        </div>
       
    </div>

    <div class="row" >
         <div class="col-md-2" align="left">
            نام گروه:
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtName" Height="23" Width="303"></asp:TextBox>
        </div>
       
    </div>
    
     <div class="row" >
         <div class="col-md-2" align="left">
            تصویر :
        </div>
        <div class="col-md-10">
            <asp:Image runat="server" ID="selectedImage"/>
            <asp:FileUpload ID="fileUploadControl" runat="server" BorderStyle="Solid" EnableTheming="True" Width="303px" CssClass="fileUploadclass"/>
            <asp:Label runat="server" ID="statusLabel" Text="" />
        </div>
        
    </div>
    

    <div class="row" >
         <div class="col-md-2" align="left">
            گروه والد:
        </div>
        <div class="col-md-10">
            <asp:DropDownList runat="server" ID="drpParentCat" CssClass="dropdown" Width="303"></asp:DropDownList>
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
         <div class="label label-info lblResult" runat="server" id="lblResult"></div>
        </div>
    </div>
</asp:Content>

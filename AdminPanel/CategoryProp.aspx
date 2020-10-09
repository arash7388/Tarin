<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="CategoryProp.aspx.cs" Inherits="AdminPanel.CategoryProp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>ویژگی های گروه</h3>
        </div>
        
    </div>
    
    <div class="row" >
         <div class="col-md-2" align="left">
            گروه :
        </div>
        <div class="col-md-10">
            <asp:DropDownList runat="server" ID="drpCategory" CssClass="dropdown" Width="303"></asp:DropDownList>
            <p>فقط گروههایی لیست می شوند که در آخرین سطح هستند یعنی گروه هایی که زیر گروه ندارند</p>
        </div>
       
    </div>

    <div class="row" >
         <div class="col-md-2" align="left">
            نام :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtName"  Width="303"></asp:TextBox>
        </div>
       
    </div>
    
     <div class="row" >
         <div class="col-md-2" align="left">
            برچسب فارسی :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtCaption"  Width="303"></asp:TextBox>
        </div>
       
    </div>
    
     <div class="row" >
         <div class="col-md-2" align="left">
            نوع :
        </div>
        <div class="col-md-10">
            <asp:DropDownList runat="server" ID="drpType" CssClass="dropdown" Width="303"></asp:DropDownList>
        </div>
       
    </div>
    
     <div class="row" >
         <div class="col-md-2" align="left">
            دارای لیست مقادیر 
        </div>
        <div class="col-md-10">
            <asp:CheckBox ID="chkHasDataSource" runat="server"/>
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

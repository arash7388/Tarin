<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="CategoryPropValue.aspx.cs" Inherits="AdminPanel.CategoryPropValue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>مقادیر ویژگی های گروه</h3>
        </div>
    </div>
    
    <div class="row" >
         <div class="col-md-2" align="left">
            گروه :
        </div>
        <div class="col-md-10">
            <asp:DropDownList runat="server" ID="drpCategory" CssClass="dropdown" Width="303" OnSelectedIndexChanged="drpCategory_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            &nbsp;
            <p>فقط گروه های سطح آخر قابل انتخاب هستند یعنی گروه هایی که زیر گروه ندارند</p>
        </div>
       
    </div>

    <div class="row" >
         <div class="col-md-2" align="left">
            ویژگی :
        </div>
        <div class="col-md-10">
            <asp:DropDownList runat="server" ID="drpCategoryProp" CssClass="dropdown" Width="303"></asp:DropDownList>
            <p>فقط ویژگی هایی که دارای لیست مقادیر هستند قابل انتخابند</p>
        </div>
       
    </div>
    
     <div class="row" >
         <div class="col-md-2" align="left">
            مقدار :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtValue"  Width="303"></asp:TextBox>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="~/ReworkReason.aspx.cs" Inherits="MehranPack.ReworkReason" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
     <div class="row">
        <div class="col-md-12">
            <h3>علت دوباره کاری</h3>
        </div>
    </div>

     <div class="row" >
         <div class="col-md-2" align="left">
            نام:
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtName" Height="23" Width="303"></asp:TextBox>
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


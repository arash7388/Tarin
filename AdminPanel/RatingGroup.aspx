<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true" CodeBehind="RatingGroup.aspx.cs" Inherits="AdminPanel.RatingGroup" %>
<%@ Register Src="RatingUC.ascx" TagName="RatingUC" TagPrefix="RUC" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <telerik:RadScriptManager runat="server">
    </telerik:RadScriptManager>
    
    <div class="row">
        <div class="col-md-12">
            <h3>آیتم رای گیری</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10">
            <div class="col-md-3" align="left">
                نام آیتم رای گیری:
            </div>
            <div class="col-md-9">
                <asp:TextBox runat="server" ID="txtName" Width="686px"></asp:TextBox>
            </div>
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
    
    <div class="row">
        <div class="col-md-10">
<RUC:RatingUC runat="server" ID="r1"/>          
        </div>
    </div>
</asp:Content>

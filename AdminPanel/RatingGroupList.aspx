<%@ Page Title="" Language="C#" MasterPageFile="AdminPanel.Master" AutoEventWireup="true" CodeBehind="RatingGroupList.aspx.cs" Inherits="AdminPanel.RatingGroupList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="RatingUC.ascx" TagPrefix="RatingUC" TagName="RatingUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="aj">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ucRating"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

     <div class="row">
        <div class="col-md-12">
            <h3>آیتم های رای گیری</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10">
            <telerik:RadGrid ID="gridRatingGroups" runat="server" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Id" HeaderText="شناسه" UniqueName="columnId" Visible="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter column Name" HeaderText="نام" UniqueName="columnName">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-black" OnClick="btnAdd_Click" />
        </div>

    </div>

    <telerik:RadRating ID="RadRating1" runat="server" ItemCount="5"
        SelectionMode="Continuous" Precision="Half" Orientation="Horizontal">
    </telerik:RadRating>
</asp:Content>

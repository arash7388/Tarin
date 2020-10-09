<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GoodsSupply.aspx.cs" Inherits="MehranPack.GoodsSupply" %>

<%@ Import Namespace="Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="UserControls/PersianCalender.ascx" TagName="PersianCalender" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>گزارش موجودی کالا</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" UpdateInitiatorPanelsOnly="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridReport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridReport" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1">
    </telerik:RadAjaxLoadingPanel>

    <div class="row">
        <div class="col-sm-12">
            <h3 runat="server" id="h3Header">گزارش موجودی کالا</h3>
            <hr class="hrBlue" />
        </div>
    </div>

   <%-- <div class="row">
        <div class="col-xs-3 col-sm-1" align="left">
            مشتری:
        </div>
        <div class="col-xs-6 col-sm-3">
            <asp:DropDownList runat="server" ID="drpCustomer" CssClass="form-control paddingTop0" Height="29"></asp:DropDownList>
        </div>
        <div class="col-xs-6 col-sm-3 col-sm-offset-5" align="left">
            <asp:Button runat="server" ID="btnExportToExcel" OnClick="btnExportToExcel_OnClick" CssClass="btn btn-info btn-standard" Height="33" Text="ارسال به اکسل" />
        </div>
    </div>--%>
    
    <div class="row">
        <div class="col-xs-3 col-sm-1" align="left">
            کالا:
        </div>
        <div class="col-xs-6 col-sm-3">
            <asp:DropDownList runat="server" ID="drpProducts" CssClass="form-control paddingTop0" Height="29"></asp:DropDownList>
        </div>
        
          <div class="col-sm-2 col-sm-offset-6" align="left">
            <asp:Button runat="server" ID="btnRun" CssClass="btn btn-info btn-standard" OnClick="btnRun_OnClick" Text="اجرا" />
        </div>
    </div>

   <%-- <div class="row">
        <div class="col-sm-1" align="left">
            از تاریخ:
        </div>
        <div class="col-sm-3">
            <uc:PersianCalender runat="server" ID="dtFrom" AllowEmpty="True" TimePanel-Visible="False" />
        </div>

        <div class="col-sm-1" align="left">
            تا تاریخ:
        </div>
        <div class="col-sm-3">
            <uc:PersianCalender runat="server" ID="dtTo" AllowEmpty="True" TimePanel-Visible="False" />
        </div>
        <div class="col-sm-2 col-sm-offset-2" align="left">
            <asp:Button runat="server" ID="btnRun" CssClass="btn btn-info btn-standard" OnClick="btnRun_OnClick" Text="اجرا" />
        </div>
    </div>--%>

    <br />
    <div class="row">
        <div class="col-sm-12">
            <telerik:RadGrid ID="RadGridReport" runat="server"
                AllowFilteringByColumn="False" AllowPaging="True" AllowSorting="False" AutoGenerateColumns="False"
                CellSpacing="-1" GridLines="Both" PageSize="15" Height="550px" ShowFooter="True" OnItemCommand="RadGrid1_ItemCommand">
                <ClientSettings AllowColumnsReorder="True">
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
                <MasterTableView>
                    <Columns>
                      
                         <telerik:GridBoundColumn DataField="Code" FilterControlAltText="Filter column column" HeaderText="کد کالا" ReadOnly="True" SortExpression="Product.Code" UniqueName="column01" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        
                          <telerik:GridBoundColumn DataField="ProductCategoryName" FilterControlAltText="Filter column column" HeaderText="گروه" 
                            ReadOnly="True" SortExpression="ProductCategoryName"  AllowFiltering="False" ShowFilterIcon="False" UniqueName="column00111" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter column column" HeaderText="نام کالا" ReadOnly="True" SortExpression="Name" UniqueName="column00159" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="350px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                     
                        
                         <%--
                        <telerik:GridBoundColumn DataField="Product.ProductCategory.Name" FilterControlAltText="Filter column column" HeaderText= "گروه سطح آخر" ReadOnly="True" SortExpression="Product.ProductCategory.Name" UniqueName="column0010" AutoPostBackOnFilter="True" 
                            AllowFiltering="False" ShowFilterIcon="False" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>--%>

                       
                        
                       <telerik:GridBoundColumn DataField="Supply"  HeaderText="موجودی" ReadOnly="True"  
                            UniqueName="column222"  ShowFilterIcon="False" AllowFiltering="False">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                       
                    </Columns>
                </MasterTableView>
                <PagerStyle PageSizes="20,30,50" PagerTextFormat="{4}<strong>{5}</strong> مورد"
                    PageSizeLabelText="تعداد ردیف ها:" />
            </telerik:RadGrid>
            
          

            
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Cardex.aspx.cs" Inherits="MehranPack.Cardex" %>

<%@ Import Namespace="Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="UserControls/PersianCalender.ascx" TagName="PersianCalender" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>گزارش کاردکس کالا</title>
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
            <h3 runat="server" id="h3Header">گزارش کاردکس کالا</h3>
            <hr class="hrBlue" />
        </div>
    </div>

    <div class="row">
        <div class="col-xs-3 col-sm-1" align="left">
            مشتری:
        </div>
        <div class="col-xs-6 col-sm-3">
            <asp:DropDownList runat="server" ID="drpCustomer" CssClass="form-control paddingTop0" Height="29"></asp:DropDownList>
        </div>
        <div class="col-xs-6 col-sm-3 col-sm-offset-5" align="left">
            <asp:Button runat="server" ID="btnExportToExcel" OnClick="btnExportToExcel_OnClick" CssClass="btn btn-info btn-standard" Height="33" Text="ارسال به اکسل" />
        </div>
    </div>
    
    <div class="row">
        <div class="col-xs-3 col-sm-1" align="left">
            کالا:
        </div>
        <div class="col-xs-6 col-sm-3">
            <asp:DropDownList runat="server" ID="drpProducts" CssClass="form-control paddingTop0" Height="29"></asp:DropDownList>
        </div>
    </div>

    <div class="row">
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
    </div>

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
                        <telerik:GridTemplateColumn HeaderText="نوع رسید" ReadOnly="True" HeaderStyle-Width="70px" Visible="true" AllowFiltering="False" ShowFilterIcon="False" UniqueName="pquality" AutoPostBackOnFilter="True" >
                           
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                            <ItemTemplate>
                                <%# Eval("InOutType").ToSafeInt()==(int)InOutType.In ? "ورود" : "خروج"%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                       <%-- ForeColor='<%= Eval("InOutType").ToSafeInt()==(int)InOutType.In ? "green" : "red"%>'--%>

                        <telerik:GridBoundColumn DataField="InputOutputId" FilterControlAltText="Filter column column" HeaderText="شناسه رسید" 
                            ReadOnly="True" SortExpression="InputOutputId" UniqueName="column0" AutoPostBackOnFilter="True" 
                             AllowFiltering="False" ShowFilterIcon="False" CurrentFilterFunction="EqualTo" DataType="System.Int32" FilterDelay="1200">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="60px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        
                      <%--  <telerik:GridBoundColumn DataField="ReceiptId" FilterControlAltText="Filter column column" HeaderText="ش رسید ورود" ReadOnly="True" 
                             AllowFiltering="False" ShowFilterIcon="False" SortExpression="ReceiptId" UniqueName="columnRInId" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" DataType="System.Int32" FilterDelay="1200">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="60px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>--%>
                        
                        <telerik:GridBoundColumn DataField="Product.ProductCategory.Parent.Name" FilterControlAltText="Filter column column" HeaderText="گروه" 
                            ReadOnly="True" SortExpression="Product.ProductCategory.Parent.Name"  AllowFiltering="False" ShowFilterIcon="False" UniqueName="column00111" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Product.ProductCategory.Name" FilterControlAltText="Filter column column" HeaderText= "گروه سطح آخر" ReadOnly="True" SortExpression="Product.ProductCategory.Name" UniqueName="column0010" AutoPostBackOnFilter="True" 
                            AllowFiltering="False" ShowFilterIcon="False" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                      <%--  <telerik:GridBoundColumn DataField="Product.Code" FilterControlAltText="Filter column column" HeaderText="کد کالا" ReadOnly="True" SortExpression="Product.Code" UniqueName="column01" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Product.Name" FilterControlAltText="Filter column column" HeaderText="نام کالا" ReadOnly="True" SortExpression="Product.Name" UniqueName="column001" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>--%>
                        
                        <telerik:GridTemplateColumn HeaderText="تاریخ رسید" ReadOnly="True" HeaderStyle-Width="100px" Visible="true" SortExpression="InsertDateTimeMaster" AllowSorting="False"
                            AllowFiltering="False" ShowFilterIcon="False" UniqueName="insdt" CurrentFilterFunction="Contains" FilterDelay="1100">
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                            <ItemTemplate>
                                <%#DateTime.Parse(Eval("InsertDateTimeMaster").ToSafeString()).ToFaDate() %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <%--<telerik:GridTemplateColumn HeaderText="تاریخ رسید کالا" ReadOnly="True" HeaderStyle-Width="110px" Visible="true" SortExpression="InsertDateTimeDetail" AllowSorting="False"
                            AllowFiltering="False" ShowFilterIcon="False" UniqueName="insdtdetail" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" FilterDelay="1100">
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                            <ItemTemplate>
                                <%#DateTime.Parse(Eval("InsertDateTimeDetail").ToSafeString()).ToFaDate() %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>

                        <%--<telerik:GridBoundColumn DataField="InsertDateTimeMasterFa" FilterControlAltText="Filter column1 column" HeaderText="تاریخ رسید" ReadOnly="True" UniqueName="columnMFA" AllowFiltering="False" DataType="System.String" Visible="True">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>--%>
                        
                      <%--  <telerik:GridBoundColumn DataField="InsertDateTimeDetailFa" FilterControlAltText="Filter column1 column" HeaderText="تاریخ رسید کالا" ReadOnly="True" UniqueName="columnDFA" AllowFiltering="False" DataType="System.String" Visible="True">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>--%>

                        <telerik:GridBoundColumn DataField="Customer.Name"  FilterControlAltText="Filter column1 column" HeaderText="مشتری" ReadOnly="True" UniqueName="column1" AllowFiltering="False" >
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Count"  HeaderText="تعداد" ReadOnly="True" 
                            UniqueName="column2"  ShowFilterIcon="False" AllowFiltering="False" Aggregate="Sum" FooterAggregateFormatString="{0:###,##0}">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        
                         <telerik:GridBoundColumn DataField="RunningSupply"  HeaderText="موجودی" ReadOnly="True"  
                            UniqueName="column222"  ShowFilterIcon="False" AllowFiltering="False">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        
                        

                       <%-- <telerik:GridBoundColumn DataField="ProductionQuality" FilterControlAltText="Filter column2 column" HeaderText="کیفیت" ReadOnly="True" SortExpression="ProductionQuality" 
                            UniqueName="columnPQId" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" FilterDelay="1500" Visible="True">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>--%>

                        
                      
                        
                        <telerik:GridTemplateColumn HeaderText="کیفیت" ReadOnly="True" HeaderStyle-Width="70px" Visible="true" SortExpression="ProductionQuality" AllowSorting="True"
                            AllowFiltering="False" ShowFilterIcon="False" UniqueName="pquality" AutoPostBackOnFilter="True" 
                            CurrentFilterFunction="Contains" FilterDelay="1000" >
                           <FilterTemplate>
                            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxPQ"  Width="60px" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("pquality").CurrentFilterValue %>'
                                runat="server" OnClientSelectedIndexChanged="qchanged" >   
                                <Items>
                                    <telerik:RadComboBoxItem Text="بد" Value="1"/>
                                    <telerik:RadComboBoxItem Text="متوسط" Value="2" />
                                    <telerik:RadComboBoxItem Text="خوب"  Value="3"/>
                                </Items>
                            </telerik:RadComboBox>

                            <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                                <script type="text/javascript">
                                    function qchanged(sender, args) {

                                        debugger;
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        tableView.filter("columnPQId", sender.get_value(), "EqualTo"); //args.get_item().get_value()
                                }
                                </script>
                            </telerik:RadScriptBlock>
                        </FilterTemplate>


                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                            <ItemTemplate>
                                <%#GetProductionQualityTextByValue(Eval("ProductionQuality").ToSafeInt()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <%-- <telerik:GridCheckBoxColumn CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="True" DataField="IsFactored" DataType="System.Boolean" FilterControlAltText="Filter column5 column" HeaderText="فاکتور شد" ReadOnly="True" UniqueName="column5" SortExpression="IsFactored">
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="70px" />
                            <ItemStyle HorizontalAlign="Center" Font-Size="10"></ItemStyle>
                        </telerik:GridCheckBoxColumn>--%>
                    </Columns>
                </MasterTableView>
                <PagerStyle PageSizes="20,30,50" PagerTextFormat="{4}<strong>{5}</strong> مورد"
                    PageSizeLabelText="تعداد ردیف ها:" />
            </telerik:RadGrid>
            
          

            
        </div>
    </div>
</asp:Content>

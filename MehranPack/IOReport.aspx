<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="IOReport.aspx.cs" Inherits="MehranPack.OrdersReport" %>

<%@ Import Namespace="Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="UserControls/PersianCalender.ascx" TagName="PersianCalender" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>گزارش ورود و خروج کالا</title>
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
            <h3 runat="server" id="h3Header">گزارش ورود و خروج کالا</h3>
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
                AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                CellSpacing="-1" GridLines="Both" PageSize="15" Height="550px" ShowFooter="True" OnItemCommand="RadGrid1_ItemCommand">
                <ClientSettings AllowColumnsReorder="True">
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="InputOutputId" FilterControlAltText="Filter column column" HeaderText="شناسه رسید" ReadOnly="True" SortExpression="InputOutputId" UniqueName="column0" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" DataType="System.Int32" FilterDelay="1200">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="60px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="ReceiptId" FilterControlAltText="Filter column column" HeaderText="ش رسید ورود" ReadOnly="True" SortExpression="ReceiptId" UniqueName="columnRInId" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" DataType="System.Int32" FilterDelay="1200">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="60px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="Product.ProductCategory.Parent.Name" FilterControlAltText="Filter column column" HeaderText="گروه" ReadOnly="True" SortExpression="Product.ProductCategory.Parent.Name" UniqueName="column00111" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Product.ProductCategory.Name" FilterControlAltText="Filter column column" HeaderText= "گروه سطح آخر" ReadOnly="True" SortExpression="Product.ProductCategory.Name" UniqueName="column0010" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Product.Code" FilterControlAltText="Filter column column" HeaderText="کد کالا" ReadOnly="True" SortExpression="Product.Code" UniqueName="column01" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.String" FilterDelay="1100">
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
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridTemplateColumn HeaderText="تاریخ رسید" ReadOnly="True" HeaderStyle-Width="100px" Visible="true" SortExpression="InsertDateTimeMaster" AllowSorting="True"
                            AllowFiltering="False" ShowFilterIcon="True" UniqueName="insdt" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" FilterDelay="1100">
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                            <ItemTemplate>
                                <%#DateTime.Parse(Eval("InsertDateTimeMaster").ToSafeString()).ToFaDate() %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridTemplateColumn HeaderText="تاریخ رسید کالا" ReadOnly="True" HeaderStyle-Width="110px" Visible="true" SortExpression="InsertDateTimeDetail" AllowSorting="True"
                            AllowFiltering="False" ShowFilterIcon="True" UniqueName="insdtdetail" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" FilterDelay="1100">
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                            <ItemTemplate>
                                <%#DateTime.Parse(Eval("InsertDateTimeDetail").ToSafeString()).ToFaDate() %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <%--<telerik:GridBoundColumn DataField="InsertDateTimeMasterFa" FilterControlAltText="Filter column1 column" HeaderText="تاریخ رسید" ReadOnly="True" UniqueName="columnMFA" AllowFiltering="False" DataType="System.String" Visible="True">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>--%>
                        
                        <telerik:GridBoundColumn DataField="InsertDateTimeDetailFa" FilterControlAltText="Filter column1 column" HeaderText="تاریخ رسید کالا" ReadOnly="True" UniqueName="columnDFA" AllowFiltering="False" DataType="System.String" Visible="False">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Customer.Name" FilterControlAltText="Filter column1 column" HeaderText="مشتری" ReadOnly="True" UniqueName="column1" AllowFiltering="False">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Count" FilterControlAltText="Filter column2 column" HeaderText="تعداد" ReadOnly="True" SortExpression="Count" 
                            UniqueName="column2" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" FilterDelay="1500" Aggregate="Sum" FooterAggregateFormatString="{0:###,##0}">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="ProductionQuality" FilterControlAltText="Filter column2 column" HeaderText="کیفیت" ReadOnly="True" SortExpression="ProductionQuality" 
                            UniqueName="columnPQId" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" FilterDelay="1500" Visible="True">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        
                        <%-- <telerik:GridBoundColumn DataField="PersianDate" FilterControlAltText="Filter column3 column" HeaderText="تاریخ" ReadOnly="True" SortExpression="PersianDate" UniqueName="column3" AllowFiltering="False">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="110px" />
                            <ItemStyle HorizontalAlign="Center" Font-Names="bkoodak" Font-Size="10"></ItemStyle>
                        </telerik:GridBoundColumn>--%>

                          <%-- <telerik:GridBoundColumn DataField="IsFactored" FilterControlAltText="Filter column4 column" HeaderText="فاکتور شد" ReadOnly="True" SortExpression="IsFactored" UniqueName="column4" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" DataType="System.Boolean">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text="" />
                        </ColumnValidationSettings>
                        <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                        </telerik:GridBoundColumn>--%>
                        
                        <telerik:GridTemplateColumn HeaderText="کیفیت" ReadOnly="True" HeaderStyle-Width="70px" Visible="true" SortExpression="ProductionQuality" AllowSorting="True"
                            AllowFiltering="True" ShowFilterIcon="True" UniqueName="pquality" AutoPostBackOnFilter="True" 
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
            
          <%--  <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:Tarin %>"
        ProviderName="System.Data.SqlClient" SelectCommand="SELECT 1 as PQID, 'بد' as PQ union SELECT 2,'متوسط' union SELECT 3,'خوب' "
        runat="server"></asp:SqlDataSource>--%>

            <%--<telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AllowSorting="True"
                    OnNeedDataSource="RadGrid1_NeedDataSource" AllowFilteringByColumn="True"
                    OnItemCommand="RadGrid1_ItemCommand" CellSpacing="0" GridLines="None">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AutoGenerateColumns="false" TableLayout="Fixed">
                        <ColumnGroups>
                            <telerik:GridColumnGroup Name="GeneralInformation" HeaderText="General Information"
                                HeaderStyle-HorizontalAlign="Center" />
                            <telerik:GridColumnGroup Name="SpecificInformation" HeaderText="Specific Information"
                                HeaderStyle-HorizontalAlign="Center" />
                            <telerik:GridColumnGroup Name="BookingInformation" HeaderText="Booking Information"
                                HeaderStyle-HorizontalAlign="Center" />
                        </ColumnGroups>
                        <HeaderStyle Width="102px" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="BrandName" HeaderText="Brand Name" UniqueName="BrandName"
                                ColumnGroupName="GeneralInformation">
                                <HeaderStyle Width="170px" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="BrandNameCombo" DataSourceID="EntityDataSource1" DataTextField="BrandName"
                                        OnDataBound="BrandNameCombo_DataBound" DataValueField="BrandName"
                                        Height="200px" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("BrandName").CurrentFilterValue %>'
                                        runat="server" OnClientSelectedIndexChanged="BrandNameComboIndexChanged">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Select a Brand" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                                        <script type="text/javascript">
                                            function BrandNameComboIndexChanged(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                            tableView.filter("BrandName", args.get_item().get_value(), "EqualTo");
                                        }
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Model" HeaderText="Model" UniqueName="Model"
                                ColumnGroupName="GeneralInformation" FilterControlWidth="60px">
                                <HeaderStyle Width="115px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Classification" HeaderText="Classification" UniqueName="Classification"
                                ColumnGroupName="GeneralInformation">
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="ClassificationCombo" Width="90px" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Classification").CurrentFilterValue %>'
                                        runat="server" OnClientSelectedIndexChanged="ClassificationComboIndexChanged">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" />
                                            <telerik:RadComboBoxItem Text="Hatchback" Value="Hatchback" />
                                            <telerik:RadComboBoxItem Text="Sedan" Value="Sedan" />
                                            <telerik:RadComboBoxItem Text="SUV" Value="SUV" />
                                            <telerik:RadComboBoxItem Text="MPV" Value="MPV" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                        <script type="text/javascript">
                                            function ClassificationComboIndexChanged(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                            tableView.filter("Classification", args.get_item().get_value(), "EqualTo");
                                        }
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Transmission" HeaderText="Transmission" UniqueName="Transmission"
                                ColumnGroupName="GeneralInformation">
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="TransmissionCombo" Width="90px" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Transmission").CurrentFilterValue %>'
                                        runat="server" OnClientSelectedIndexChanged="TransmissionComboIndexChanged">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" />
                                            <telerik:RadComboBoxItem Text="Manual" Value="Manual" />
                                            <telerik:RadComboBoxItem Text="Automatic" Value="Automatic" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                        <script type="text/javascript">
                                            function TransmissionComboIndexChanged(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                            tableView.filter("Transmission", args.get_item().get_value(), "EqualTo");
                                        }
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Year" HeaderText="Year" UniqueName="Year" ColumnGroupName="SpecificInformation" FilterControlWidth="55px">
                                <HeaderStyle Width="110px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Fuel" HeaderText="Fuel" UniqueName="Fuel" ColumnGroupName="SpecificInformation">
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="FuelCombo" Width="90px" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Fuel").CurrentFilterValue %>'
                                        runat="server" OnClientSelectedIndexChanged="FuelComboIndexChanged">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" />
                                            <telerik:RadComboBoxItem Text="Diesel" Value="Diesel" />
                                            <telerik:RadComboBoxItem Text="Gasoline" Value="Gasoline" />
                                            <telerik:RadComboBoxItem Text="Hybrid" Value="Hybrid" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock4" runat="server">
                                        <script type="text/javascript">
                                            function FuelComboIndexChanged(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                            tableView.filter("Fuel", args.get_item().get_value(), "EqualTo");
                                        }
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn DataField="Price" HeaderText="Price" UniqueName="Price"
                                ColumnGroupName="BookingInformation" DataFormatString="<strong>&#8364; {0}</strong>"
                                AllowFiltering="false" HeaderStyle-Width="80px" />
                            <telerik:GridTemplateColumn HeaderText="Book" ColumnGroupName="BookingInformation"
                                AllowFiltering="false">
                                <HeaderStyle Width="102px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="BookButton" runat="server" Text="Book Now" OnClientClick='<%# String.Format("openConfirmationWindow({0}); return false;", Eval("CarID")) %>'
                                        CssClass="bookNowLink" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <NestedViewTemplate>
                            <div class="carBackground" style='<%# NormalizeValue(String.Format("background-image: url(Images/LargeLogos/{0}.png);", Eval("BrandName"))) %>'>
                                <div style="float: left;">
                                    <asp:Image ID="CarImage" runat="server" AlternateText="Car Image" ImageUrl='<%# GetCarImageUrl(Container)%>' />
                                </div>
                                <div style="float: right; width: 50%">
                                    <div class="carTitle">
                                        <%# Eval("BrandName") %>
                                        <%# Eval("Model") %>
                                        <span style="color: #555555; float: right; font-size: 14px; font-weight: normal;">Rented
                                        <%# Eval("RentedCount") %>
                                        times</span>
                                    </div>
                                    <hr class="lineSeparator" />
                                    <table width="100%" class="carInfo">
                                        <tr>
                                            <td>
                                                <strong>Year:</strong>
                                                <%# Eval("Year") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Classification:</strong>
                                                <%# Eval("Classification") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Transmission:</strong>
                                                <%# Eval("Transmission") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Fuel Type:</strong>
                                                <%# Eval("Fuel") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Price:</strong> &#8364;<%# Eval("Price") %>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="clear: both">
                                </div>
                            </div>
                        </NestedViewTemplate>
                        <PagerStyle PageSizes="5,10" PagerTextFormat="{4}<strong>{5}</strong> cars matching your search criteria"
                            PageSizeLabelText="Cars per page:" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="false" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="" />
                    </ClientSettings>
                </telerik:RadGrid>--%>
        </div>
    </div>
</asp:Content>

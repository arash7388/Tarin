<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FactorsReport.aspx.cs" Inherits="MehranPack.FactorsReport" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register TagPrefix="uc" TagName="PersianCalender" Src="~/UserControls/PersianCalender.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>گزارش فاکتورها</title>
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
            <h3>گزارش فاکتورها</h3>
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
            <span class="input-group-btn">
                <asp:ImageButton runat="server" ID="btnExportToExcel" CssClass="btn btn-default btn50" ImageUrl="Images/excel16.png" OnClick="btnExportToExcel_OnClick"></asp:ImageButton>
                <asp:ImageButton runat="server" ID="btnExportToPdf" CssClass="btn btn-default btn50" ImageUrl="Images/pdf16.png" OnClick="btnExportToPdf_OnClick"></asp:ImageButton>
            </span>

            <%--<asp:Button  runat="server" ID="btnExportToExcel" OnClick="btnExportToExcel_OnClick"  CssClass="btn btn-info btn-standard" Height="33" Text="ارسال به اکسل"/>--%>
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
                CellSpacing="-1" GridLines="Both" PageSize="20" Height="605px"
                OnItemCreated="RadGridReport_OnItemCreated">
                <ClientSettings AllowColumnsReorder="True">
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="FactorNo" FilterControlAltText="Filter column column" HeaderText="شماره فاکتور" ReadOnly="True" SortExpression="FactorNo" UniqueName="column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" DataType="System.Int32" FilterDelay="1200" FilterImageToolTip="فیلتر" MaxLength="50">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter column1 column" HeaderText="مشتری" ReadOnly="True" UniqueName="column1" AllowFiltering="False" FilterImageToolTip="فیلتر" MaxLength="200">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TotalPrice" FilterControlAltText="Filter column2 column" HeaderText="مبلغ" ReadOnly="True" SortExpression="TotalPrice" UniqueName="column2" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" FilterDelay="1500" DataType="System.Decimal" FilterImageToolTip="فیلتر" MaxLength="150" DataFormatString="{0:###,###,###,###}">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PersianDate" FilterControlAltText="Filter column3 column" HeaderText="تاریخ" ReadOnly="True" SortExpression="PersianDate" UniqueName="column3" AllowFiltering="False" FilterImageToolTip="فیلتر" MaxLength="150">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="110px" />
                            <ItemStyle HorizontalAlign="Center" Font-Names="bkoodak" Font-Size="10"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowSorting="False" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataField="Description" FilterControlAltText="Filter column4 column" FilterDelay="1200" FilterImageToolTip="فیلتر" HeaderText="توضیحات" ReadOnly="True" UniqueName="column4">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <PagerStyle PageSizes="20,30,50" PagerTextFormat="{4}<strong>{5}</strong> مورد"
                    PageSizeLabelText="تعداد ردیف ها:" />
            </telerik:RadGrid>

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

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PaymentsReport.aspx.cs" Inherits="MehranPack.PaymentsReport" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register TagPrefix="uc" TagName="persiancalender" Src="~/UserControls/PersianCalender.ascx" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <h3>گزارش مبالغ دریافتی</h3>
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
        </div>
    </div>

    <div class="row">
        <div class="col-sm-1" align="left">
            از تاریخ:
        </div>
        <div class="col-sm-3">
            <uc:persiancalender runat="server" id="dtFrom" allowempty="True" timepanel-visible="False" />
        </div>

        <div class="col-sm-1" align="left">
            تا تاریخ:
        </div>
        <div class="col-sm-3">
            <uc:persiancalender runat="server" id="dtTo" allowempty="True" timepanel-visible="False" />
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
                CellSpacing="-1" GridLines="Both" PageSize="20" Height="605px">
                <ClientSettings AllowColumnsReorder="True">
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
                <MasterTableView>
                    <ColumnGroups>
                        <telerik:GridColumnGroup Name="Remain" HeaderText="مانده" HeaderStyle-HorizontalAlign="Center"  />
                        
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridBoundColumn DataField="PersianPaymentDate" HeaderText="تاریخ" ReadOnly="True" SortExpression="PersianPaymentDate" UniqueName="column1" MaxLength="90">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="90px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="FactorNo" FilterControlAltText="Filter column column" HeaderText="شماره فاکتور" ReadOnly="True" SortExpression="FactorNo" UniqueName="column2" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" DataType="System.Int32" FilterDelay="1200" FilterImageToolTip="فیلتر" MaxLength="80">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="80px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter column column" HeaderText="شرح" ReadOnly="True" SortExpression="Description" UniqueName="column3" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" DataType="System.string" FilterDelay="1500" FilterImageToolTip="فیلتر" >
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="FactorTotalPrice" FilterControlAltText="Filter column2 column" HeaderText="مبلغ فاکتور" ReadOnly="True" SortExpression="FactorTotalPrice" UniqueName="column4" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" FilterDelay="1500" DataType="System.Decimal" FilterImageToolTip="فیلتر" MaxLength="100" DataFormatString="{0:###,###,###,###}">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="PaymentTotalPrice" FilterControlAltText="Filter column2 column" HeaderText="مبلغ  دریافتی" ReadOnly="True" SortExpression="PaymentTotalPrice" UniqueName="column5" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" FilterDelay="1500" DataType="System.Decimal" FilterImageToolTip="فیلتر" MaxLength="110" DataFormatString="{0:###,###,###,###}">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn ColumnGroupName="Remain" DataField="RemainBed" FilterControlAltText="Filter column2 column" HeaderText="بدهکار" ReadOnly="True" SortExpression="RemainBed" UniqueName="column6" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" FilterDelay="1500" DataType="System.Decimal" FilterImageToolTip="فیلتر" MaxLength="100" DataFormatString="{0:###,###,###,###}">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn ColumnGroupName="Remain" DataField="RemainBes" FilterControlAltText="Filter column2 column" HeaderText="بستانکار" ReadOnly="True" SortExpression="RemainBes" UniqueName="column7" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" FilterDelay="1500" DataType="System.Decimal" FilterImageToolTip="فیلتر" MaxLength="100" DataFormatString="{0:###,###,###,###}">
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WorkLineSummaryReport.aspx.cs" Inherits="MehranPack.WorkLineSummaryReport" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register TagPrefix="uc" TagName="PersianCalender" Src="~/UserControls/PersianCalender.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>خلاصه گزارش کارهای تولیدی</title>
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
            <h3>گزارش خلاصه کارهای تولیدی</h3>
            <hr class="hrBlue" />
        </div>
    </div>
    <div class="row">
        <div class="col-xs-3 col-sm-1" align="left">
            نوع :
        </div>
        <div class="col-xs-6 col-sm-3">
            <asp:DropDownList runat="server" ID="drpReportType" CssClass="paddingTop0" Height="29" Width="180" OnSelectedIndexChanged="drpReportType_SelectedIndexChanged"></asp:DropDownList>
<%--            &nbsp;<asp:Label runat="server" ID="lblHint" Font-Size="Small" ForeColor="Red" Visible="true" CssClass="alert-danger">توجه:جهت صحیح بودن خروجی گزارش باید تاریخ ثبت کاربرگ و تاریخ تولید اوپراتور یکسان باشند</asp:Label>--%>
        </div>
        <div class="col-xs-3 col-sm-1" align="left">
            پارت :
        </div>
        <div class="col-xs-6 col-sm-3">
            <asp:TextBox runat="server" ID="txtPartNo" Width="80"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-3 col-sm-1" align="left">
            اوپراتور:
        </div>
        <div class="col-xs-6 col-sm-3">
            <asp:DropDownList runat="server" ID="drpOperator" CssClass="form-control paddingTop0" Width="180" Height="29"></asp:DropDownList>
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
<label>جهت دسته بندی جدول ، ستون مد نظر را به پنل بالای جدول کشیده و رها کنید</label>
            <br />
            <telerik:RadGrid ID="RadGridReport" runat="server"
                AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                CellSpacing="-1" GridLines="Both" PageSize="20" Height="605px"
                OnItemCreated="RadGridReport_OnItemCreated" ShowGroupPanel="true" ShowFooter="true">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="true">
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    <ClientMessages DragToGroupOrReorder="ستون را جهت گروه بندی به پنل بالا کشیده و رها کنید" />
                    <ClientMessages DragToResize="جهت تغییر سایز بکشید" />
                </ClientSettings>
                <MasterTableView ShowGroupFooter="true" AllowMultiColumnSorting="true">

                    <%--                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias="اوپراتور" FieldName="OperatorName" FormatString=""
                                    HeaderValueSeparator=" : "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="OperatorName" SortOrder="Descending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>--%>

                    <Columns>


                        <telerik:GridBoundColumn DataField="FriendlyName" FilterControlAltText="Filter column2 column" HeaderText="اوپراتور" ReadOnly="True" UniqueName="column234" AllowFiltering="true" AutoPostBackOnFilter="True" FilterImageToolTip="فیلتر" CurrentFilterFunction="Contains" MaxLength="200" DataType="System.string">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column2 column" HeaderText="محصول" ReadOnly="True" UniqueName="columnProductName" AllowFiltering="true" AutoPostBackOnFilter="True" FilterImageToolTip="فیلتر" CurrentFilterFunction="Contains" MaxLength="200" DataType="System.string">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="ProcessName" FilterControlAltText="Filter column2 column" HeaderText="فرآیند" ReadOnly="True" UniqueName="columnProcessName" AllowFiltering="true" AutoPostBackOnFilter="True" FilterImageToolTip="فیلتر" CurrentFilterFunction="Contains" MaxLength="200" DataType="System.string">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="PersianDate" FilterControlAltText="Filter column column" HeaderText="تاریخ" ReadOnly="True" SortExpression="PersianDate" UniqueName="column22" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.string" FilterDelay="1200" FilterImageToolTip="فیلتر" MaxLength="50">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Year" FilterControlAltText="Filter column column" HeaderText="سال" ReadOnly="True" SortExpression="Count" UniqueName="columnYear" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.Int32" FilterDelay="1200" FilterImageToolTip="فیلتر" MaxLength="50">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Month" FilterControlAltText="Filter column column" HeaderText="ماه" ReadOnly="True" SortExpression="Count" UniqueName="columnMonth" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.Int32" FilterDelay="1200" FilterImageToolTip="فیلتر" MaxLength="50">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Day" FilterControlAltText="Filter column column" HeaderText="روز" ReadOnly="True" SortExpression="Count" UniqueName="columnDay" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.Int32" FilterDelay="1200" FilterImageToolTip="فیلتر" MaxLength="50">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridCalculatedColumn HeaderText="تعداد فرآیند" UniqueName="columnTotalItems" DataType="System.Int32"
                            DataFields="Count" Expression="{0}" FooterText="جمع : "
                            Aggregate="Sum">
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridCalculatedColumn>
                        
                        <telerik:GridBoundColumn DataField="ProcessTime" FilterControlAltText="Filter column column" HeaderText="زمان مجاز" ReadOnly="True" SortExpression="Count" UniqueName="column23391" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.Int32" FilterDelay="1200" FilterImageToolTip="فیلتر" MaxLength="50">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="ProcessDuration" FilterControlAltText="Filter column column" HeaderText="زمان طی شده" ReadOnly="True" SortExpression="Count" UniqueName="column233912" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.Int32" FilterDelay="1200" FilterImageToolTip="فیلتر" MaxLength="50">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
                            <ItemStyle Font-Names="bkoodak" Font-Size="10" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="DiffTime" FilterControlAltText="Filter column column" HeaderText="تاخیر/تعجیل" ReadOnly="True" SortExpression="DiffTime" UniqueName="column2339132" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" DataType="System.Int32" FilterDelay="1200" FilterImageToolTip="فیلتر" MaxLength="50">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="خطایی رخ داد" />
                            </ColumnValidationSettings>
                            <HeaderStyle Font-Names="bkoodak" Font-Bold="True" Font-Size="Medium" Width="120px" />
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

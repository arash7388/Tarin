<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="MehranPack.Payment" %>

<%@ Import Namespace="Common" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc" TagName="PersianCalender" Src="~/UserControls/PersianCalender.ascx" %>

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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <div class="container" id="paymentContainer"></div>

    <div class="row">
        <div class="col-sm-12">
            <h3>مبالغ دریافتی</h3>
            <hr class="hrBlue" />
        </div>
    </div>

    <div class="row">
        <div class="col-sm-1">
            شماره:
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtPaymentNo" CssClass="form-control txtNormal"></asp:TextBox>
        </div>
        
        <div class="col-sm-1 col-sm-offset-1">
            تاریخ:
        </div>
        <div class="col-sm-3">
            <uc:PersianCalender runat="server" ID="ucDate" CssClass="form-control txtNormal"></uc:PersianCalender>
        </div>
    </div>

<div class="row">
    
        <div class="col-sm-1 " align="left">
            توضیحات:
        </div>
        <div class="col-sm-11">
            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control txtNormal"></asp:TextBox>
        </div>
    
</div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-1" align="left">
                    فاکتور:
                </div>
                <div class="col-sm-3">
                    <telerik:RadComboBox ID="drpFactor" runat="server" AutoPostBack="True" AllowCustomText="True" Filter="Contains" MarkFirstMatch="True" Width="100%" OnSelectedIndexChanged="drpFactor_OnSelectedIndexChanged">
                    </telerik:RadComboBox>
                </div>

                <div class="col-sm-1" align="left">
                    <asp:Label runat="server" ID="lblPriceCaption" Visible="False">مبلغ:</asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:Label runat="server" ID="lblFactorPrice" CssClass="" Visible="False"></asp:Label>
                </div>

                <div class="col-sm-1" align="left">
                    <asp:Label runat="server" ID="lblCustomerCaption" Visible="False">مشتری:</asp:Label>

                </div>
                <div class="col-sm-2">
                    <asp:Label runat="server" ID="lblFactorCustomer" CssClass="" Visible="False"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="drpFactor" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <br />
    <div class="row">
        <div class="col-sm-12">

            <asp:GridView runat="server" AutoGenerateColumns="False" Width="98%" ID="gridPayment" CssClass="table table-bordered table-striped"
                DataKeyNames="RowNumber" AllowPaging="True" ShowFooter="True" EmptyDataText="اطلاعاتی جهت نمایش وجود ندارد"
                OnRowCommand="gridFactor_OnRowCommand"
                OnPageIndexChanging="gridFactor_OnPageIndexChanging"
                OnRowEditing="gridFactor_OnRowEditing"
                OnRowUpdating="gridFactor_OnRowUpdating"
                OnRowCancelingEdit="gridFactor_OnRowCancelingEdit"
                OnRowDeleting="gridFactor_OnRowDeleting"
                OnRowDeleted="gridFactor_OnRowDeleted"
                OnRowDataBound="gridFactor_OnRowDataBound"
                ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:TemplateField HeaderText="ردیف">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("RowNumber") %>'></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:ImageButton ID="btnAddFooter" runat="server" ImageUrl="Images/plus16.png" OnClick="btnAddFooter_OnClick"></asp:ImageButton>
                        </FooterTemplate>

                        <HeaderStyle HorizontalAlign="Left" Width="60" />
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                        <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center"></FooterStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="تاریخ">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# ((DateTime)Eval("DetailDate")).ToFaDate() %>'></asp:Label>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <uc:PersianCalender runat="server" Date='<%# ((DateTime)Eval("DetailDate")).ToFaDate() %>' ID="ucDetailDate"></uc:PersianCalender>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <uc:PersianCalender runat="server" />
                        </FooterTemplate>

                        <HeaderStyle Width="100"></HeaderStyle>
                        <ItemStyle Width="100" CssClass="dateUC"></ItemStyle>
                        <FooterStyle Width="100" CssClass="dateUC"></FooterStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="مبلغ">
                        <ItemTemplate>
                            <asp:Label runat="server" Text=' <%# Eval("Price").ToSafeDecimal().ToString("N0") %>'></asp:Label>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text=' <%# Eval("Price").ToSafeDecimal().ToString("N0") %>' ID="txtPriceEdit" autocomplete="off"></asp:TextBox>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <asp:TextBox ID="txtPriceFooter" runat="server" autocomplete="off"></asp:TextBox>
                        </FooterTemplate>

                        <HeaderStyle Width="100"></HeaderStyle>
                        <ItemStyle Width="100"></ItemStyle>
                        <FooterStyle Width="100"></FooterStyle>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="نوع">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="drpPaymentType" Text='<%#GetPaymentTypeName(Eval("PaymentType").ToSafeInt()) %>' />
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:DropDownList ID="drpPaymentType" runat="server" DataSourceID="ObjectDSPaymentType" DataValueField="Id" DataTextField="Name">
                            </asp:DropDownList>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <asp:DropDownList ID="drpPaymentTypeInsert" runat="server" DataSourceID="ObjectDSPaymentType" DataValueField="Id" DataTextField="Name">
                            </asp:DropDownList>
                        </FooterTemplate>

                        <HeaderStyle Width="80"></HeaderStyle>
                        <ItemStyle Width="80"></ItemStyle>
                        <FooterStyle Width="80"></FooterStyle>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="شرح">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Eval("Description") %>' ID="txtDescription" autocomplete="off"></asp:TextBox>
                        </EditItemTemplate>

                        <FooterTemplate>
                            <asp:TextBox runat="server" Text="" autocomplete="off"></asp:TextBox>
                        </FooterTemplate>

                        <HeaderStyle Width="300"></HeaderStyle>
                        <ItemStyle Width="300"></ItemStyle>
                        <FooterStyle Width="300"></FooterStyle>

                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" EditImageUrl="Images/Edit16.png" CancelText="انصراف" UpdateText="ذخیره" EditText="ویرایش" />
                    <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="Images/Delete16.png" DeleteText="حذف" />

                </Columns>
                <EmptyDataTemplate>
                    <asp:ImageButton ID="btnAddFirstRow" runat="server" ImageUrl="Images/plus16.png" CausesValidation="False" OnClick="btnAddFirstRow_OnClick" />
                </EmptyDataTemplate>

            </asp:GridView>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-2 col-sm-offset-8" style="text-align: left; padding-left: 0">
            <asp:Label runat="server" Text="جمع کل"></asp:Label>
        </div>
        <div class="col-sm-2" style="text-align: left;">
            <asp:TextBox runat="server" ReadOnly="True" ID="txtTotalPrice"></asp:TextBox>
        </div>


    </div>

    <hr />
    <div class="row">
        <div class="col-sm-4 ">
            <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="btn btn-info btn-standard" OnClick="btnSave_Click" AccessKey="s" ToolTip="Alt + S"></asp:Button>
            <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="btn btn-info btn-standard" OnClick="btnCancel_Click" AccessKey="q" ToolTip="Alt + Q"></asp:Button>
        </div>
    </div>

    <asp:ObjectDataSource ID="ObjectDSPaymentType" runat="server" TypeName="Repository.DAL.PaymentTypeRepository"
        SelectMethod="GetAll"></asp:ObjectDataSource>

</asp:Content>

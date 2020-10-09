<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Factor.aspx.cs" Inherits="MehranPack.Factor" ClientIDMode="Static" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="Repository.Entity.Domain" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>فاکتور</title>
    <script type="text/javascript" src="/Scripts/jquery-1.10.2.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:ScriptManager runat="server" ID="srcManager"></asp:ScriptManager>
    <div class="container" id="orderContainer">
        <div class="row">
            <div class="col-sm-12">
                <h3>فاکتور</h3>
                <hr class="hrBlue" />
            </div>
        </div>

        <div class="row">
            <div class="col-sm-1" align="left">
                شماره:
            </div>
            <div class="col-sm-3">
                <asp:TextBox runat="server" ID="txtFactorNumber" TextMode="Number" CssClass="form-control txtNormal paddingTop0"></asp:TextBox>
            </div>

            <div class="col-sm-1 col-sm-offset-1" align="left">
                تاریخ:
            </div>
            <div class="col-sm-2">
                <asp:TextBox runat="server" ID="txtDate" CssClass="form-control txtNormal"></asp:TextBox>
            </div>
        </div>


        <div class="row">
            <div class="col-sm-1" align="left">
                مشتری:
            </div>
            <div class="col-sm-3">
                <asp:DropDownList runat="server" ID="drpCustomer" CssClass="form-control drpNormal "></asp:DropDownList>
            </div>

            <div class="col-sm-1 col-sm-offset-1" align="left">
                توضیحات:
            </div>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control txtNormal"></asp:TextBox>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-sm-12">
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="98%" ID="gridFactor" CssClass="table table-bordered table-striped"
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

                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="40px"  />
                            <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center"></FooterStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="شرح">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("Description") %>' ID="txtDescription" autocomplete="off"></asp:TextBox>
                            </EditItemTemplate>

                            <FooterTemplate>
                                <asp:TextBox runat="server" Text=""></asp:TextBox>
                            </FooterTemplate>

                            <ItemStyle Width="150"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="واحد">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="drpGoodsUnit" Text='<%#GetGoodsUnitName(Eval("GoodsUnitId").ToSafeInt()) %>' />
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:DropDownList ID="drpGoodsUnit" runat="server" DataSourceID="ObjectDSGoodsUnits" DataValueField="Id" DataTextField="Name">
                                </asp:DropDownList>
                            </EditItemTemplate>

                            <ItemStyle Width="100"></ItemStyle>

                            <FooterTemplate>
                                <asp:DropDownList ID="drpGoodsUnitInsert" runat="server" DataSourceID="ObjectDSGoodsUnits" DataValueField="Id" DataTextField="Name">
                                </asp:DropDownList>
                            </FooterTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="تعداد">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Count").ToSafeDecimal().ToString("N0") %>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("Count") %>' autocomplete="off"></asp:TextBox>
                            </EditItemTemplate>

                            <FooterTemplate>
                                <asp:TextBox runat="server"></asp:TextBox>
                            </FooterTemplate>

                            <ItemStyle Width="40"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="وزن" Visible="False">
                            <ItemTemplate>
                                <asp:Label runat="server" Text=' <%# Eval("Weight").ToSafeDecimal().ToString("N0") %>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text=' <%# Eval("Weight") %>' autocomplete="off" ></asp:TextBox>
                            </EditItemTemplate>

                            <FooterTemplate>
                                <asp:TextBox runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemStyle Width="40"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="فی">
                            <ItemTemplate>
                                <asp:Label runat="server" Text=' <%# Eval("Price").ToSafeDecimal().ToString("N0") %>'></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text=' <%# Eval("Price") %>' autocomplete="off"></asp:TextBox>
                            </EditItemTemplate>

                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtPriceFooter"></asp:TextBox>
                            </FooterTemplate>

                            <ItemStyle Width="40"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="مبلغ">
                            <ItemTemplate>
                                <asp:Label runat="server"></asp:Label>
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:Label runat="server" Text=' <%# (Eval("Price").ToSafeDecimal() * Eval("Count").ToSafeDecimal()).ToString("N0") %>'></asp:Label>
                            </EditItemTemplate>

                           <%-- <FooterTemplate>
                                <asp:Label ID="lblTotalPriceFooter" runat="server"></asp:Label>
                            </FooterTemplate>--%>

                            <ItemStyle Width="80"></ItemStyle>
                          
                        </asp:TemplateField>

                        <%-- <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="btnInsert" runat="server" CausesValidation="false" UseSubmitBehavior="False" CommandName="Insert"
                                CommandArgument='<%# Eval("Id") %>' />
                            <asp:Image runat="server" ImageUrl="Images/plus16.png" />
                        </ItemTemplate>
                        <ItemStyle Width="80"></ItemStyle>
                    </asp:TemplateField>--%>

                        <%--  <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="" CommandArgument='<%# Eval("Id") %>'  ImageUrl="Images/Edit16.png"/>
                            <asp:Image runat="server" ImageUrl="Images/Edit16.png" />
                        </ItemTemplate>
                        <ItemStyle Width="80"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                Text="" CommandArgument='<%# Eval("Id") %>' ImageUrl="Images/Delete16.png" />
                            <asp:Image runat="server" ImageUrl="Images/Delete16.png" />
                        </ItemTemplate>
                        <ItemStyle Width="70"></ItemStyle>
                    </asp:TemplateField>--%>

                        <asp:CommandField ShowEditButton="True" EditImageUrl="Images/Edit16.png" CancelText="انصراف" UpdateText="ذخیره" EditText="ویرایش" />
                        <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="Images/Delete16.png" DeleteText="حذف" />

                    </Columns>
                    <EmptyDataTemplate>
                        <%--<asp:Label ID="Label1" runat="server" Text="1"></asp:Label>
                     <asp:TextBox runat="server" Text=""></asp:TextBox>
                     <asp:DropDownList ID="drpGoodsUnitEmpty" runat="server"></asp:DropDownList>
                    <asp:TextBox runat="server"></asp:TextBox>
                     <asp:TextBox runat="server"></asp:TextBox>
                     <asp:TextBox runat="server"></asp:TextBox>
                    <asp:Label runat="server"></asp:Label>--%>
                        <asp:ImageButton ID="btnAddFirstRow" runat="server" ImageUrl="Images/plus16.png" CausesValidation="False" OnClick="btnAddFirstRow_OnClick" />

                    </EmptyDataTemplate>

                </asp:GridView>
            </div>
        </div>

        <div class="row">
           <div class="col-sm-2 col-sm-offset-8" style="text-align: left;padding-left: 0">
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


        <asp:ObjectDataSource ID="ObjectDSGoodsUnits" runat="server" SelectMethod="GetAll"
            TypeName="Repository.DAL.GoodsUnitRepository"></asp:ObjectDataSource>
    </div>

    <script type="text/javascript">
        $("#<%=gridFactor.ClientID %>").keydown(function (e) {
            debugger;
            var focused = $(document.activeElement);
            if (focused[0].id == 'txtPriceFooter' && e.keyCode == 9) {

                //var g = $("#<%=gridFactor.ClientID %>");
                $("btnAddFooter").click();
                //.click();
            }
            if (e.keyCode == 50 || e.keyCode == 98) {
                //$("#<%=gridFactor.ClientID %>").click();
        }

        });
    </script>

</asp:Content>


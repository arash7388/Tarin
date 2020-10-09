<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PaymentList.aspx.cs" Inherits="MehranPack.PaymentList" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="Repository.Entity.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:ScriptManager runat="server" ID="sc1"></asp:ScriptManager>
    <div class="container" style="">
        <div class="row">
            <div class="col-md-12">
                <h3>لیست مبالغ دریافتی</h3>
                <hr class="hrBlue" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView runat="server" AutoGenerateColumns="False" Width="98%" ID="gridList" CssClass="table table-bordered table-striped"
                            OnRowCommand="gridList_OnRowCommand" DataKeyNames="Id"
                            OnPageIndexChanging="gridList_OnPageIndexChanging"
                            OnSorting="gridList_Sorting"
                            AllowPaging="True"
                            AllowSorting="True">
                            <Columns>
                                <asp:BoundField DataField="PaymentNo" HeaderText="شماره" SortExpression="PaymentNo">
                                    <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                </asp:BoundField>
                                
                                 <asp:TemplateField HeaderText="فاکتور" SortExpression="FactorNo">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#((Factor)Eval("Factor")).FactorNo.ToSafeInt() %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="160"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="مشتری" SortExpression="CustomerName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#GetCustomerName(((Factor)Eval("Factor")).CustomerId.ToSafeInt()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="160"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="تاریخ">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Convert.ToDateTime(Eval("PaymentDate")).ToFaDate() %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="مبلغ">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("TotalPrice").ToString().ToFaGString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Description" HeaderText="توضیحات">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="150" />
                                </asp:BoundField>

                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            Text="ویرایش" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Image runat="server" ImageUrl="Images/Edit16.png" />
                                    </ItemTemplate>
                                    <ItemStyle Width="80"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                            Text="حذف" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Image runat="server" ImageUrl="Images/Delete16.png" />
                                    </ItemTemplate>
                                    <ItemStyle Width="70"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print"
                                            Text="چاپ" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Image runat="server" ImageUrl="Images/print16.png" />
                                    </ItemTemplate>
                                    <ItemStyle Width="70"></ItemStyle>
                                </asp:TemplateField>

                            </Columns>
                            
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gridList" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="gridList" EventName="Sorting" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-info btn-standard" OnClick="btnAdd_Click" />
            </div>
        </div>
    </div>

    <script type='text/javascript' src='<%= ResolveUrl("~/Scripts/jquery-1.10.2.js") %>'></script>
    <script type='text/javascript' src='<%= ResolveUrl("~/Scripts/bootstrap-rtl.js") %>'></script>
</asp:Content>


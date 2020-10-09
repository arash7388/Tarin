<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="MehranPack.OrderList" %>

<%@ Import Namespace="Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:ScriptManager runat="server" ID="sc1"></asp:ScriptManager>
    <div class="container" style="">
        <div class="row">
            <div class="col-md-12">
                <h3>سفارشات</h3>
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
                            OnDataBound="gridList_OnDataBound"
                            OnSorting="gridList_Sorting"
                            AllowPaging="True"
                            AllowSorting="True">
                            <Columns>
                                <asp:BoundField DataField="OrderNo" HeaderText="شماره" SortExpression="OrderNo">
                                    <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="مشتری" SortExpression="CustomerName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#GetCustomerName(Eval("CustomerId").ToSafeInt()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="160"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="تاریخ">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Convert.ToDateTime(Eval("InsertDateTime")).ToFaDate() %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="WorkTitle" HeaderText="نام کار">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="120"></ItemStyle>
                                </asp:BoundField>


                                <asp:TemplateField HeaderText="نوع جنس">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#GetProductTypeName(Eval("ProductTypeId").ToSafeInt()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Grammage" HeaderText="گرماژ">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="30" />
                                </asp:BoundField>

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

                            <%--  <PagerTemplate>

                        <table width="100%">
                            <tr>
                                <td style="width: 70%">

                                    <asp:Label ID="MessageLabel"
                                        ForeColor="Blue"
                                        Text="Select a page:"
                                        runat="server" />
                                    <asp:DropDownList ID="PageDropDownList"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                        runat="server" />

                                </td>

                                <td style="width: 70%; text-align: right">

                                    <asp:Label ID="CurrentPageLabel"
                                        ForeColor="Blue"
                                        runat="server" />

                                </td>

                            </tr>
                        </table>

                    </PagerTemplate>--%>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gridList" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="gridList" EventName="Sorting" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-info btn-standard" OnClick="btnAdd_Click" />
                <%--<asp:Button runat="server" Text="حذف" data-toggle="modal" data-target="#myModal" ID="btnDelete" CssClass="btn btn-info btn-standard"></asp:Button>--%>
            </div>
        </div>

        <!-- Modal -->

        <%--<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel" style="font-family: BKoodak">توجه</h4>

                    </div>
                    <div class="modal-body">
                        آیا از حذف اطمینان دارید؟
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-info btn-sm" data-dismiss="modal">خیر</button>
                        <asp:Button runat="server" Text="بله" ID="btnConfirmDelete" OnClick="btnDelete_OnClick" CssClass="btn btn-info btn-sm"></asp:Button>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>

    <script type='text/javascript' src='<%= ResolveUrl("~/Scripts/jquery-1.10.2.js") %>'></script>
    <script type='text/javascript' src='<%= ResolveUrl("~/Scripts/bootstrap-rtl.js") %>'></script>
</asp:Content>

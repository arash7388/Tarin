<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WorksheetList.aspx.cs" Inherits="MehranPack.WorksheetList" %>
<%@ Import Namespace="Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="container" >
        <div class="row">
            <div class="col-md-12">
                <h3 runat="server" id="h3Header">لیست کاربرگ ها</h3>
                <hr class="hrBlue"/>
            </div>
        </div>

        <div class="row">
            <div class="col-md-11">
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridList" CssClass="table table-bordered table-striped"
                    OnRowCommand="gridList_OnRowCommand" DataKeyNames="Id" OnPageIndexChanging="gridList_OnPageIndexChanging" AllowPaging="True" EmptyDataText="اطلاعاتی جهت نمایش یافت نشد">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="شناسه">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="30" />
                        </asp:BoundField>

                       
                        <asp:TemplateField HeaderText="اوپراتور ">
                            <ItemTemplate>
                                <%# GetOperatorName(Eval("OperatorId").ToSafeInt()) %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="PartNo" HeaderText="پارت">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="30" />
                        </asp:BoundField>

                        <%--<asp:BoundField DataField="InsertDateTime" HeaderText="تاریخ ایجاد">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="200" />
                        </asp:BoundField>--%>
                        
                         <asp:TemplateField HeaderText="تاریخ ">
                            <ItemTemplate>
                                <%# ((DateTime)Eval("Date")).Date.ToFaDate() %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80" />
                        </asp:TemplateField>

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
                <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-info btn-standard" OnClick="btnAdd_Click" />
            </div>
        </div>
    </div>
</asp:Content>

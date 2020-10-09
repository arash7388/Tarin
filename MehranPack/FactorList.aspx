<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FactorList.aspx.cs" Inherits="MehranPack.FactorList" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Repository.Entity.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="container" >
        <div class="row">
            <div class="col-md-12">
                <h3>فاکتور ها</h3>
                <hr class="hrBlue"/>
            </div>
        </div>

        <div class="row" >
            <div class="col-sm-12">
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="98%" ID="gridList" CssClass="table table-bordered table-striped"
                    OnRowCommand="gridList_OnRowCommand" DataKeyNames="Id" 
                    OnPageIndexChanging="gridList_OnPageIndexChanging" 
                    AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="FactorNo" HeaderText="شماره">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="50" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="مشتری">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#(((Customer)Eval("Customer")).Name) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="160"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="تاریخ">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#Convert.ToDateTime(Eval("FactorDate")).ToFaDate() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80"></ItemStyle>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="مبلغ فاکتور">
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
                <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-info btn-standard" OnClick="btnAdd_Click" />
                <%--<asp:Button runat="server" Text="حذف" data-toggle="modal" data-target="#myModal" ID="btnDelete" CssClass="btn btn-info btn-standard"></asp:Button>--%>
            </div>
        </div>
    </div>
</asp:Content>

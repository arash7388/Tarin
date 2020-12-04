<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReworkList.aspx.cs" Inherits="MehranPack.ReworkList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="container" >
        <div class="row">
            <div class="col-md-12">
                <h3 runat="server" id="h3Title">لیست دوباره کاری ها</h3>
                <hr class="hrBlue"/>
            </div>
        </div>

        <div class="row">
            <div class="col-md-11">
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridList" CssClass="table table-bordered table-striped"
                    OnRowCommand="gridList_OnRowCommand" DataKeyNames="Id" OnPageIndexChanging="gridList_OnPageIndexChanging" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="شناسه">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="30" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ACode" HeaderText="ACode">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="80" />
                        </asp:BoundField>

                        <asp:BoundField DataField="OpName" HeaderText="اوپراتور">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="100" />
                        </asp:BoundField>

                         <asp:BoundField DataField="ReasonName" HeaderText="علت">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="100" />
                        </asp:BoundField>

                         <asp:BoundField DataField="Desc" HeaderText="توضیحات">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="150" />
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

                    </Columns>
                </asp:GridView>
                <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-info btn-standard" OnClick="btnAdd_Click" />
            </div>
        </div>
    </div>
</asp:Content>

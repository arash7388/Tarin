<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="MehranPack.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>محصولات</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10">
            <hr class="hrBlue" />
            <asp:GridView runat="server" AutoGenerateColumns="False" Width="512px" ID="gridProduct" CssClass="table table-bordered table-striped"
                OnRowCommand="gridProduct_OnRowCommand" DataKeyNames="Id" AllowPaging="true" PageSize="10" OnPageIndexChanging="gridProduct_OnPageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="شناسه">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Code" HeaderText="کد">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="نام">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CategoryName" HeaderText="گروه محصول">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="EditProduct"
                                Text="ویرایش" CommandArgument='<%# Eval("Id") %>' />
                            <asp:Image runat="server" ImageUrl="Images/Edit16.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="DeleteProduct"
                                Text="حذف" CommandArgument='<%# Eval("Id") %>' />
                            <asp:Image runat="server" ImageUrl="Images/Delete16.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" ID="btnAddProduct" Text="جدید" CssClass="btn btn-black" OnClick="btnAddProduct_Click" />
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="CategoryPropList.aspx.cs" Inherits="AdminPanel.CategoryPropList" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="Repository.Entity.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>ویژگی های گروه</h3>
            <p>
            در این قسمت گروه های سطح آخر یعنی آنهایی که زیرگروه ندارند انتخاب شده و برای آنها ویژگی تعیین می شود. به طور مثال 
            گروه آپارتمان می تواند دارای ویژگی متراژ باشد
        </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <asp:GridView runat="server" AutoGenerateColumns="False" Width="512px" ID="gridCategoryProp" CssClass="table table-bordered table-striped" DataKeyNames="Id"
                OnRowCommand="gridCategoryProp_RowCommand" AllowPaging="True" PageSize="10" OnPageIndexChanging="gridCategoryProp_OnPageIndexChanging">
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Wrap="False" />

                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="شناسه">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="نام">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Caption" HeaderText="برچسب فارسی">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="گروه">
                        <ItemTemplate>
                            <asp:Label runat="server" CausesValidation="false"
                                Text='<%#CategoryList.SingleOrDefault(a => a.Key==Eval("CategoryId").ToSafeInt()).Value %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="نوع">
                        <ItemTemplate>
                            <asp:Label runat="server" CausesValidation="false"
                                Text='<%#Enum.GetName(typeof(CategoryPropType),Eval("Type"))%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false"
                                CommandName="Edit"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="ویرایش" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false"
                                CommandName="Delete"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="حذف" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-black" OnClick="btnAddCat_Click" />
        </div>
    </div>
</asp:Content>

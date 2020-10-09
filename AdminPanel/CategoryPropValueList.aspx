<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="CategoryPropValueList.aspx.cs" Inherits="AdminPanel.CategoryPropValueList" %>
<%@ Import Namespace="AdminPanel" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Repository.Entity.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    
    <div class="row">
        <div class="col-md-12">
            <h3>مقادیر ویژگی های گروه</h3>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-12">
           <p>ویژگی هایی که در آنها "دارای لیست مقادیر" تیک خورده باشد در این قسمت قابل انتخاب هستند و مقادیر آنها در این قسمت تعیین می شوند به طور 
               مثال ویژگی سایز می تواند دارای مقادیر کوچک ، متوسط و بزرگ باشد که در این قسمت تعیین میشوند.
           </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10">
            <asp:GridView runat="server" AutoGenerateColumns="False" Width="512px" ID="gridCategoryPropValue" CssClass="table table-bordered table-striped" DataKeyNames="Id"
                OnRowCommand="gridCategoryPropValue_RowCommand" AllowPaging="True" PageSize="10" OnPageIndexChanging="gridCategoryPropValue_OnPageIndexChanging">
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Wrap="False" />

                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="شناسه">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                     <asp:TemplateField HeaderText="گروه">
                        <ItemTemplate>
                            <asp:Label runat="server" CausesValidation="false"
                                Text='<%#((Repository.Entity.Domain.Category)Eval("Category")).Name%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="ویژگی">
                        <ItemTemplate>
                            <asp:Label runat="server" CausesValidation="false"
                                Text='<%#((Repository.Entity.Domain.CategoryProp)Eval("CategoryProp")).Caption%>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Value" HeaderText="مقدار">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                  
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

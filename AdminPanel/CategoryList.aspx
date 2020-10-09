<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="AdminPanel.CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>گروه ها</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="512px" ID="gridCategory" CssClass="table table-bordered table-striped" DataKeyNames="Id"
                OnRowCommand="gridCategory_RowCommand" AllowPaging="True" PageSize="10" OnPageIndexChanging="gridCategory_OnPageIndexChanging">
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Wrap="False" />
                    
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="شناسه">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                     <asp:BoundField DataField="Code" HeaderText="کد">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Name" HeaderText="عنوان">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ParentName" HeaderText="گروه والد">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false"
                                CommandName="EditCat"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="ویرایش" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false"
                                CommandName="DeleteCat"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="حذف" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <%--<asp:UpdatePanel runat="server">
                <ContentTemplate>
        
                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gridCategory" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>--%>
            <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-black" OnClick="btnAddCat_Click" />
        </div>
    </div>
</asp:Content>

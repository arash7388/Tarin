<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProcessCategoryList.aspx.cs" Inherits="MehranPack.ProcessCategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="container" >
        <div class="row">
            <div class="col-md-12">
                <h3>لیست فرآیندهای گروه محصولات
                &nbsp;
                &nbsp;
                <asp:Button runat="server" Text="لیست تایمینگ های گروه/فرآیند" CssClass="btn btn-default" ID="btnPrTiming" OnClientClick="location.href = 'ProcessCategoryTiming.aspx'; return false;"/>
                    </h3>
                <hr class="hrBlue"/>
            </div>
        </div>

        <div class="row">
            <div class="col-md-11">
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridList" CssClass="table table-bordered table-striped"
                    OnRowCommand="gridList_OnRowCommand" DataKeyNames="CategoryId" OnPageIndexChanging="gridList_OnPageIndexChanging" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="CategoryId" HeaderText="شناسه گروه" Visible="false">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="30" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CategoryName" HeaderText="گروه محصول">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="150" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ProcessNames" HeaderText="فرآیندها">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="150" />
                        </asp:BoundField>               

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                    Text="ویرایش" CommandArgument='<%# Eval("CategoryId") %>' />
                                <asp:Image runat="server" ImageUrl="Images/Edit16.png" />
                            </ItemTemplate>
                            <ItemStyle Width="80"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                    Text="حذف" CommandArgument='<%# Eval("CategoryId") %>' />
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

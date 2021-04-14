<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MemberList.aspx.cs" Inherits="Tashim.MemberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>اعضا</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <hr class="hrBlue" />
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="512px" ID="gridList" CssClass="table table-bordered table-striped" DataKeyNames="Id"
                OnRowCommand="gridList_RowCommand" AllowPaging="True" PageSize="50" OnPageIndexChanging="gridList_OnPageIndexChanging">
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

                    <asp:BoundField DataField="Name" HeaderText="نام">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ShareAmount" HeaderText="مبلغ سهام">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ShareCount" HeaderText="تعداد سهام">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false"
                                CommandName="Edit"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="ویرایش" />
                            <asp:Image runat="server" ImageUrl="Images/Edit16.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false"
                                CommandName="Delete"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="حذف" />
                            <asp:Image runat="server" ImageUrl="Images/Delete16.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-black" OnClick="btnAdd_Click" />
        </div>
    </div>
    <hr class="hrBlue" />
    <div class="row">
        <div class="col-md-1 text-left">
            <label>مجموع:</label>
        </div>
        <div class="col-md-6">
            <asp:TextBox runat="server" ID="txtSum"></asp:TextBox>
        </div>
    </div>
</asp:Content>


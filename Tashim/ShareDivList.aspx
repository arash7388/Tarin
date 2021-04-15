<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ShareDivList.aspx.cs" Inherits="Tashim.ShareDivList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>لیست تقسیم سود ها</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <hr class="hrBlue" />
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="512px" ID="gridList" CssClass="table table-bordered table-striped" DataKeyNames="Id"
                OnRowCommand="gridList_RowCommand" AllowPaging="True" PageSize="10" OnPageIndexChanging="gridList_OnPageIndexChanging" OnRowDataBound="gridList_RowDataBound">
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Wrap="False" />
                    
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="شناسه">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                     <asp:TemplateField ShowHeader="true" HeaderText="تاریخ">
                        <ItemTemplate>
                            <asp:Label ID="txtInsertDateTime" runat="server" CausesValidation="false"
                                Text='<%# Eval("PersianInsertDateTime") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ShareAmount" HeaderText="مبلغ سود">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                                     
                    <asp:BoundField DataField="TypeDesc" HeaderText="نوع سهامدار">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>


                    <%--<asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false"
                                CommandName="Edit"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="ویرایش" />
                            <asp:Image runat="server" ImageUrl="Images/Edit16.png" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>

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
        
</asp:Content>


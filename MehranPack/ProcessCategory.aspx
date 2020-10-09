<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="~/ProcessCategory.aspx.cs" Inherits="MehranPack.ProcessCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>فرآیندهای گروه محصول</h3>
        </div>
    </div>
    <hr class="hrBlue" />
    <div class="row">
        <div class="col-md-2" align="left">
            گروه محصول:
        </div>
        <div class="col-md-10">
            <asp:DropDownList runat="server" ID="drpCat" CssClass="dropdown" Width="350"></asp:DropDownList>
        </div>

    </div>
    <br />

    <div class="row">
        <div class="col-md-10">
            <asp:GridView runat="server" AutoGenerateColumns="False" Width="512px" ID="gridInput" CssClass="table table-bordered table-striped" DataKeyNames="Id"
                OnRowCommand="gridSource_RowCommand" AllowPaging="True" PageSize="14"
                EmptyDataText="اطلاعاتی جهت نمایش وجود ندارد"
                OnRowDeleting="gridInput_OnRowDeleting" OnRowDeleted="gridInput_OnRowDeleted">

                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Wrap="False" />

                <Columns>
                     <asp:BoundField DataField="ProcessId" HeaderText="فرآیند" Visible="false">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ProcessName" HeaderText="فرآیند" Visible="true">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Order" HeaderText="ترتیب" Visible="true">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                     <asp:BoundField DataField="ProcessTime" HeaderText="زمان" Visible="true">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <%--   <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false"
                                        CommandName="EditCat"
                                        CommandArgument='<%# Eval("Id") %>'
                                        Text="ویرایش" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                Text="حذف" CommandArgument='<%# Eval("ProcessId") %>' />
                            <asp:Image runat="server" ImageUrl="Images/Delete16.png" />
                        </ItemTemplate>
                        <ItemStyle Width="70"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


        </div>
    </div>
    <hr class="hrGray"/>

    <div class="panel" id="panelAddNewPC">
        <div class="panel-heading">اضافه کردن آیتم</div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-2" align="left">
                    فرآیند:
                </div>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="drpProcesses" CssClass="dropdown" Width="300"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2 " align="left">
                    ترتیب:
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtOrder" CssClass="form-control" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2 " align="left">
                    زمان فرآیند:
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtProcessTime" CssClass="form-control" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-10 col-md-offset-2">
                    <asp:Button runat="server" ID="Button1" Text="اضافه" CssClass="btn btn-info btn-standard" OnClick="btnAdd_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div class="row" id="PrCatSaveRow">
        <div class="col-md-2">
            <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="btn btn-black btn-standard" OnClick="btnSave_Click" style="min-width:120px !important"></asp:Button>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10 col-md-offset-2">
            <div class="label label-info lblResult" runat="server" id="lblResult"></div>
        </div>
    </div>
</asp:Content>


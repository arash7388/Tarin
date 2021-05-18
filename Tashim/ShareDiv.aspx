<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ShareDiv.aspx.cs" Inherits="Tashim.ShareDiv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h3>تقسیم سود</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2" align="left">
            مبلغ سود :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtShareAmount" Height="23" Width="303"></asp:TextBox>
        </div>

    </div>

    <div class="row">
        <div class="col-md-2" align="left">
            درصد سهام :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtSharePercent" Height="23" Width="303"></asp:TextBox>
        </div>

    </div>

    <div class="row">
        <div class="col-md-2" align="left">
            درصد مساوی :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtEqualPercent" Height="23" Width="303"></asp:TextBox>
        </div>

    </div>

    <div class="row">
        <div class="col-md-2" align="left">
            درصد اولویت دار :
        </div>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtPrPercent" Height="23" Width="303"></asp:TextBox>
        </div>

    </div>

    <div class="row">
        <div class="col-md-2" align="left">
            نوع سهامدار:
        </div>
        <div class="col-md-10">
            <asp:DropDownList runat="server" ID="drpType" CssClass="dropdown" Width="303"></asp:DropDownList>
        </div>

    </div>
    <hr />

    <asp:GridView runat="server" AutoGenerateColumns="False" Width="512px" ID="gridList" CssClass="table table-bordered table-striped" DataKeyNames="Id"
                OnRowCommand="gridList_RowCommand" AllowPaging="True" PageSize="10" OnPageIndexChanging="gridList_OnPageIndexChanging">
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Wrap="False" />
                    
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="شناسه">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                    

                    <asp:BoundField DataField="ShareAmount" HeaderText="مبلغ سود">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                                     
                    <asp:BoundField DataField="TypeDesc" HeaderText="نوع سهامدار">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

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

    <div class="row">
        <div class="col-md-10 col-md-offset-2">
            <asp:Button runat="server" ID="btnSave" Text="اجرا" CssClass="btn btn-black" OnClick="btnSave_Click"></asp:Button>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10 col-md-offset-2">
            <div class="label label-info lblResult" runat="server" id="lblResult"></div>
        </div>
    </div>
</asp:Content>

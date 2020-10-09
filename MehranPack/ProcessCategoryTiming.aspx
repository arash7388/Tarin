<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProcessCategoryTiming.aspx.cs" Inherits="MehranPack.ProcessCategoryTiming" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="container" >
        <div class="row">
            <div class="col-md-12">
                <h3>لیست تایمینگ فرآیندهای گروه محصولات</h3>
                <hr class="hrBlue"/>
            </div>
        </div>

        <div class="row">
            <div class="col-md-11">
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridList" CssClass="table table-bordered table-striped"
                     DataKeyNames="Id" OnPageIndexChanging="gridList_OnPageIndexChanging" AllowPaging="True">
                    <Columns>
                         <asp:BoundField DataField="Id" HeaderText="شناسه">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CategoryName" HeaderText="گروه محصول">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ProcessName" HeaderText="فرآیند">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ProcessTime" HeaderText="زمان">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="50" />
                        </asp:BoundField> 
                    </Columns>
                </asp:GridView>
                
            </div>
        </div>
    </div>
</asp:Content>

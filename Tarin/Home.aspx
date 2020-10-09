<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Tarin.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <link href="Content/css/bootstrap-rtl.css" rel="stylesheet" />
    <link href="Content/css/icons.css" rel="stylesheet" />
    <link href="Content/css/page-design.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script>
        var isLoading = false;
        var pageSize = 10;
        var pageIndex = 0;
        function InfiniteScroll() {

            $('#divPostsLoader').html('<img src="Content/images/loader.gif">');

            //send a query to server side to present new content
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetData",
                data: '{pageIndex: ' + pageIndex + '}',
                //data: '{pageIndex: ' + pageIndex + ',cat: ' + </%= menuId.Text %> +'}',

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    if (data != "") {
                        $('.divLoadData:last').after(data.d);
                    }
                    $('#divPostsLoader').empty();
                }
            });
        };

        $(window).scroll(function () {
            var scrolloffset = 0;

            if (($(window).scrollTop() >= $(document).height() - $(window).height() - scrolloffset) && isLoading == false) {
                isLoading = true;
                InfiniteScroll();
                isLoading = false;
                pageIndex += 5;
            }
        });
    </script>
    <div class="un-pad">

        <div class="pge-body un-pad">
            <div class="row un-pad">
                <div class="col-sm-2 un-pad">
                    <div class="menu-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <div class="button-menu">
                                    <asp:ListView ID="listViewCats" runat="server" ItemType="Repository.Entity.Domain.Category">

                                        <LayoutTemplate>
                                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                        </LayoutTemplate>

                                        <ItemTemplate>
                                            <div>

                                                <div class="unstyled">
                                                    <asp:LinkButton runat="server" Text="<%#Item.Name %>"
                                                        CommandArgument="<%#Item.Id%>"
                                                        OnCommand="MyButtonHandler" />
                                                </div>
                                            </div>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
                <div class="col-sm-10 un-pad ">
                    <div class="col-sm-11 un-pad items-body">
                        <div class="first-menu-item">
                            جدیدترین آگهی ها
                        </div>
                        <div style="padding: 1%">

                            <div class="divLoadData row">
                            </div>
                            <div class="row">
                                <div class="col-sm-2 col-sm-offset-5" id="divPostsLoader">
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


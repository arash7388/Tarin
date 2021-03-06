﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ManualWorkLine.aspx.cs" Inherits="MehranPack.WorkLine" ClientIDMode="Static" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register TagPrefix="uc" TagName="PersianCalender" Src="~/UserControls/PersianCalender.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>ورود دستی تایمینگ تولید</title>
    <meta content="Automation" />
</asp:Content>button

<asp:Content runat="server" ContentPlaceHolderID="main">
    <div class="row">
        <div class="col-md-10" style="padding-right: 75px;">
            <h3 style="margin-top: 35px;">ورود دستی تایمینگ تولید &nbsp; 
            
                 <asp:Label runat="server" ID="lblCurrentDate"></asp:Label>
                -
                 <asp:Label runat="server" ID="lblCurrentTime"></asp:Label>
            </h3>
        </div>
        <div class="col-md-2 text-left">
            <img src="Images/logo.png" class="img-responsive" style="width: 110px; height: 70px; margin-top: 10px" />
        </div>
    </div>

    
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="Panel1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Panel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
 
<asp:Panel ID="Panel1" runat="server">
    <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="false" PageSize="10"
        AllowMultiRowEdit="true"
        OnNeedDataSource="RadGrid1_NeedDataSource"
        OnItemCommand="RadGrid1_ItemCommand"
        OnInsertCommand="RadGrid1_InsertCommand"
        OnUpdateCommand="RadGrid1_UpdateCommand"
        OnDeleteCommand="RadGrid1_DeleteCommand">
        <MasterTableView
            CommandItemDisplay="Top"
            DataKeyNames="OrderID"
            InsertItemDisplay="Top"
            InsertItemPageIndexAction="ShowItemOnLastPage"
            EditMode="InPlace"
            RetainExpandStateOnRebind="true">
            <CommandItemTemplate>
                <telerik:RadButton ID="insertbtn" runat="server" Text="Add New Record" CommandName="InitInsert"></telerik:RadButton>
                <telerik:RadButton ID="updateBtn" runat="server" Text="Update Edited" CommandName="UpdateEdited"></telerik:RadButton>
                <telerik:RadButton ID="cancelBtn" runat="server" Text="Cancel All" CommandName="CancelAll"></telerik:RadButton>
            </CommandItemTemplate>
            <Columns>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <telerik:RadButton ID="RadButton6" runat="server" Text="Edit" CommandName="Edit"></telerik:RadButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--<telerik:RadButton ID="RadButton7" runat="server" Text='<%# Container.OwnerTableView.IsItemInserted?"Insert":"Update" %>'
                            CommandName='<%# Container.OwnerTableView.IsItemInserted?"PerformInsert":"Update" %>'>
                        </telerik:RadButton>--%>
                        <telerik:RadButton ID="RadButton8" runat="server" Text="Cancel" CommandName="Cancel"></telerik:RadButton>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
 
                <telerik:GridBoundColumn DataField="OrderID" DataType="System.Int32"
                    FilterControlAltText="Filter OrderID column" HeaderText="OrderID"
                    ReadOnly="True" SortExpression="OrderID" UniqueName="OrderID">
                </telerik:GridBoundColumn>
 
                <telerik:GridTemplateColumn DataField="Freight" DataType="System.Decimal" ReadOnly="True"
                    FilterControlAltText="Filter Freight column" HeaderText="Freight"
                    SortExpression="Freight" UniqueName="Freight">
                    <HeaderTemplate>
                        <asp:Label ID="LabelFreight" runat="server" Text="Freight"></asp:Label>
                        <br />
                        <br />
                        <telerik:RadButton ID="RadButton3" runat="server" Text="Edit" CommandName="EditColumn" CommandArgument="Freight"></telerik:RadButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("Freight") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" DbValue='<%# Bind("Freight") %>'></telerik:RadNumericTextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
 
                <telerik:GridTemplateColumn DataField="ShipName"
                    FilterControlAltText="Filter ShipName column" HeaderText="ShipName" ReadOnly="True"
                    SortExpression="ShipName" UniqueName="ShipName">
                    <HeaderTemplate>
                        <asp:Label ID="LabelShipName" runat="server" Text="ShipName"></asp:Label>
                        <br />
                        <br />
                        <telerik:RadButton ID="RadButton4" runat="server" Text="Edit" CommandName="EditColumn" CommandArgument="ShipName"></telerik:RadButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("ShipName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadTextBox ID="RadTextBox1" runat="server" Text='<%# Bind("ShipName") %>'></telerik:RadTextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
 
                <telerik:GridTemplateColumn DataField="ShipCountry"
                    FilterControlAltText="Filter ShipCountry column" HeaderText="ShipCountry" ReadOnly="True"
                    SortExpression="ShipCountry" UniqueName="ShipCountry">
                    <HeaderTemplate>
                        <asp:Label ID="LabelShipCountry" runat="server" Text="ShipCountry"></asp:Label>
                        <br />
                        <br />
                        <telerik:RadButton ID="RadButton5" runat="server" Text="Edit" CommandName="EditColumn" CommandArgument="ShipCountry"></telerik:RadButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("ShipCountry") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadTextBox ID="RadTextBox2" runat="server" Text='<%# Bind("ShipCountry") %>'></telerik:RadTextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn Text="Delete" CommandName="Delete"></telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Action:" Visible="false"></asp:Label>
</asp:Panel>



    <script type="text/javascript">

        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }


        $(document).ready(function () {
            if ($("#gridWorkLine") != undefined) {
                var gridWidth = $("#gridWorkLine").width();
                var gridOffset = $("#gridWorkLine").offset();

                if (gridWidth == null)
                    $("#txtBarcodeInput").width("95%")
                else
                    $("#txtBarcodeInput").width(gridWidth);

                if (gridOffset != undefined)
                    $("#txtBarcodeInput").offset({ left: gridOffset.left })
            };

            $("#txtBarcodeInput").on("keypress", function (e) {

                if (e.keyCode == 35) {
                    var inputTxt = $("#txtBarcodeInput").val();
                    var inputTxt1 = '<%#Session["InputBarcode"] != null ? Session["InputBarcode"].ToString() : "" %>';
                    var paramss = '{input:"' + inputTxt + '"}'
                    $.ajax({
                        url: '<%= ResolveUrl("~/workline.aspx/AddRow") %>',
                        type: "POST",
                        data: paramss,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            debugger;
                            if (data.d == "OK") {
                                $("#txtBarcodeInput").val('');
                                toastr["success"]("ردیف با موفقیت اضافه شد")
                                $("#txtBarcodeInput").val('');
                                setTimeout(function () { window.location.href = window.location.origin + "/workline.aspx"; }, 1200);
                            }
                            else {
                                beep();
                                toastr["error"](data.d);
                                setTimeout(() => { console.log("خطا در سیستم " + data.d); }, 1000);
                                $("#txtBarcodeInput").val('');
                            };
                        },
                        error: function (data) {
                            beep();
                            toastr["error"]("خطا در سیستم " + data.Message);
                            setTimeout(() => { console.log("خطا در سیستم " + data.Message); }, 1000);
                        }
                    });
                }
            });


            var myVar = setInterval(timer, 1000);

            function timer() {

                var d = new Date();
                var time = d.toLocaleTimeString().replace(" ", " ").replace("AM", "").replace("PM", "")
                $("#lblCurrentTime").text(time.toFaDigit());
            }

            ConvertNumberToPersion();

            String.prototype.toEnDigit = function () {
                return this.replace(/[\u06F0-\u06F9]+/g, function (digit) {
                    var ret = '';
                    for (var i = 0, len = digit.length; i < len; i++) {
                        ret += String.fromCharCode(digit.charCodeAt(i) - 1728);
                    }

                    return ret;
                });
            };

            String.prototype.toFaDigit = function () {
                return this.replace(/\d+/g, function (digit) {
                    var ret = '';
                    for (var i = 0, len = digit.length; i < len; i++) {
                        ret += String.fromCharCode(digit.charCodeAt(i) + 1728);
                    }

                    return ret;
                });
            };

            function ConvertNumberToPersion() {
                persian = { 0: '۰', 1: '۱', 2: '۲', 3: '۳', 4: '۴', 5: '۵', 6: '۶', 7: '۷', 8: '۸', 9: '۹' };
                function traverse(el) {
                    if (el.nodeType == 3) {
                        var list = el.data.match(/[0-9]/g);
                        if (list != null && list.length != 0) {
                            for (var i = 0; i < list.length; i++)
                                el.data = el.data.replace(list[i], persian[list[i]]);
                        }
                    }
                    for (var i = 0; i < el.childNodes.length; i++) {
                        traverse(el.childNodes[i]);
                    }
                }
                traverse(document.body);
            }


        });


        function beep() {
            var snd = new Audio("data:audio/wav;base64,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qJrx4KOQjahZxWbcZgztj2c49nKmkId44S71j0c8eV9yDK6uPRzx5X18eDvjvQ6yKo9ZSS6l//8elePK/Lf//IInrOF/FvDoADYAGBMGb7FtErm5MXMlmPAJQVgWta7Zx2go+8xJ0UiCb8LHHdftWyLJE0QIAIsI+UbXu67dZMjmgDGCGl1H+vpF4NSDckSIkk7Vd+sxEhBQMRU8j/12UIRhzSaUdQ+rQU5kGeFxm+hb1oh6pWWmv3uvmReDl0UnvtapVaIzo1jZbf/pD6ElLqSX+rUmOQNpJFa/r+sa4e/pBlAABoAAAAA3CUgShLdGIxsY7AUABPRrgCABdDuQ5GC7DqPQCgbbJUAoRSUj+NIEig0YfyWUho1VBBBA//uQZB4ABZx5zfMakeAAAAmwAAAAF5F3P0w9GtAAACfAAAAAwLhMDmAYWMgVEG1U0FIGCBgXBXAtfMH10000EEEEEECUBYln03TTTdNBDZopopYvrTTdNa325mImNg3TTPV9q3pmY0xoO6bv3r00y+IDGid/9aaaZTGMuj9mpu9Mpio1dXrr5HERTZSmqU36A3CumzN/9Robv/Xx4v9ijkSRSNLQhAWumap82WRSBUqXStV/YcS+XVLnSS+WLDroqArFkMEsAS+eWmrUzrO0oEmE40RlMZ5+ODIkAyKAGUwZ3mVKmcamcJnMW26MRPgUw6j+LkhyHGVGYjSUUKNpuJUQoOIAyDvEyG8S5yfK6dhZc0Tx1KI/gviKL6qvvFs1+bWtaz58uUNnryq6kt5RzOCkPWlVqVX2a/EEBUdU1KrXLf40GoiiFXK///qpoiDXrOgqDR38JB0bw7SoL+ZB9o1RCkQjQ2CBYZKd/+VJxZRRZlqSkKiws0WFxUyCwsKiMy7hUVFhIaCrNQsKkTIsLivwKKigsj8XYlwt/WKi2N4d//uQRCSAAjURNIHpMZBGYiaQPSYyAAABLAAAAAAAACWAAAAApUF/Mg+0aohSIRobBAsMlO//Kk4soosy1JSFRYWaLC4qZBYWFRGZdwqKiwkNBVmoWFSJkWFxX4FFRQWR+LsS4W/rFRb/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////VEFHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAU291bmRib3kuZGUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAwNGh0dHA6Ly93d3cuc291bmRib3kuZGUAAAAAAAAAACU=");
            snd.play();
        }
        
    </script>
</asp:Content>

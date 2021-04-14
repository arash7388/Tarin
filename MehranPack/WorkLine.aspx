<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="WorkLine.aspx.cs" Inherits="MehranPack.WorkLine" ClientIDMode="Static" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>صف کارهای در حال تولید</title>
    <meta content="Automation" />
    <link href="Content/css/select2.css" rel="stylesheet" />
    <script src="Content/js/select2.js"></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="main">

    <asp:HiddenField runat="server" ID="reworkEsghatMode" />

    <div class="row">
        <div class="col-md-10" style="padding-right: 75px;">
            <h3 style="margin-top: 35px;">صف کارهای در حال تولید &nbsp; 
            
                 <asp:Label runat="server" ID="lblCurrentDate"></asp:Label>
                -
                 <asp:Label runat="server" ID="lblCurrentTime"></asp:Label>
            </h3>
        </div>

        <div class="col-md-2 text-left">
            <img src="Images/logo.png" class="img-responsive" style="width: 110px; height: 70px; margin-top: 10px" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-10" style="padding-right: 80px;">
            <asp:CheckBox runat="server" ID="chkShowAll" Text="نمایش کلیه تایمینگ ها(100 ردیف آخر)" AutoPostBack="true" />
        </div>
    </div>

    <div class="row">

        <div class="col-md-12 text-center" style="padding-right: 45px;">
            <hr class="hrBlue" style="width: 95%; margin-top: 5px !important" />
            <br />
            <asp:GridView runat="server" AutoGenerateColumns="False" Width="95%" ID="gridWorkLine" CssClass="table table-bordered table-striped" DataKeyNames="Id"
                OnRowCommand="gridWorkLine_RowCommand" AllowPaging="True"
                PageSize="10" OnPageIndexChanging="gridWorkLine_OnPageIndexChanging">
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Wrap="False" />

                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="شناسه">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="WorksheetId" HeaderText="شناسه کاربرگ">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="OperatorName" HeaderText="اوپراتور">
                        <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ProcessName" HeaderText="فرآیند">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PersianDateTime" HeaderText="زمان ایجاد">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="دستی" InsertVisible="false">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Visible="true" Enabled="false" Checked='<%# Eval("Manual") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

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

            <asp:Button runat="server" ID="btnAdd" Text="جدید" CssClass="btn btn-black btn-standard" OnClick="btnAdd_Click" Visible="false" />
            <asp:TextBox runat="server" ID="txtBarcodeInput" placeholder="بارکد را اسکن کنید ..." AutoCompleteType="None" autocomplete="off"></asp:TextBox>
        </div>
    </div>

    <!-- Modal -->
    <div id="reworkModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" onclick="cancel();">&times;</button>
                    <h4 class="modal-title">ورود رمز</h4>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10">
                            <label>رمز</label>&nbsp;&nbsp;
                            <asp:TextBox runat="server" ID="txtReworkPassword" placeholder="" Width="60" TextMode="Password"></asp:TextBox>
                            <br />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-standard btn-success" onclick="checkReworkPassword();">تایید</button>
                    <button type="button" class="btn btn-default btn-standard btn-danger" onclick="cancel();">انصراف</button>
                </div>
            </div>

        </div>
    </div>

    <!-- Modal -->
    <div id="esghatModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" onclick="cancel();">&times;</button>
                    <h4 class="modal-title">ورود رمز</h4>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10">
                            <label>رمز</label>&nbsp;&nbsp;
                            <asp:TextBox runat="server" ID="txtEsghatPassword" placeholder="" Width="60" TextMode="Password"></asp:TextBox>
                            <br />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-standard btn-success" onclick="checkEsghatPassword();">تایید</button>
                    <button type="button" class="btn btn-default btn-standard btn-danger" onclick="cancel();">انصراف</button>
                </div>
            </div>

        </div>
    </div>

    <%--Modal--%>
    <div id="esghatReworkInputModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <h3 runat="server" id="h3ReworkEsghat"></h3>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-1 col-sm-2">
                            فرآیند قبلی:
                        </div>
                        <div class="col-md-3 col-sm-4">
                            <asp:DropDownList runat="server" ID="drpPrevProcess" Height="23" Width="150" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>

                    <div id="rowsContainer">
                        <div class="row" id="row0">
                            <div class="col-md-1 col-sm-2">
                                ACode:
                            </div>
                            <div class="col-md-3 col-sm-4">
                                <asp:DropDownList runat="server" ID="multiAcodeDD0" class="multiDropDown" Height="23" Width="150"></asp:DropDownList>

                                <%--<select id="#multiAcodeDD0" class="multiDropDown" name="acodes[]" multiple="multiple" style="min-width: 120px">
                                </select>--%>
                            </div>

                            <div class="col-md-1 col-sm-2 col-md-offset-1 col-sm-offset-1">
                                علت:
                            </div>

                            <div class="col-md-3 col-sm-4">
                                <asp:DropDownList runat="server" ID="drpReworkReason0" CssClass="drpRR" Height="23" Width="126"></asp:DropDownList>
                            </div>

                            <%--<div class="col-md-8 col-sm-6">
                            <asp:Label runat="server" ID="lblProductName" Height="23" Width="150"></asp:Label>
                        </div>--%>
                        </div>

                    </div>


                    <button type="button" id="btn0" onclick="addNewAcodeReasonRow()">+ </button>

                    <div class="row">
                        <div class="col-md-1 col-sm-2">
                            اوپراتور:
                        </div>
                        <div class="col-md-3 col-sm-4">
                            <asp:DropDownList runat="server" ID="drpOp" Height="23" Width="150" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>

                    <%--<div class="row">
                        <div class="col-md-1 col-sm-2">
                            علت:
                        </div>
                        <div class="col-md-3 col-sm-4">
                            <asp:DropDownList runat="server" ID="drpReworkReason" Height="23" Width="250"></asp:DropDownList>
                        </div>
                    </div>--%>

                    <div class="row">
                        <div class="col-md-1 col-sm-2">
                            شرح:
                        </div>
                        <div class="col-md-7 col-sm-7">
                            <asp:TextBox runat="server" ID="txtReworkDesc" Height="50" Width="500"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-standard btn-success" onclick="addReworkEsghat();">تایید</button>
                    <button type="button" class="btn btn-default btn-standard btn-danger" onclick="$('#esghatReworkInputModal').modal('hide');$('#txtBarcodeInput').val('') ">انصراف</button>
                </div>
            </div>

        </div>
    </div>



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
                    debugger;
                    var inputTxt = $("#txtBarcodeInput").val();
                    inputTxt = inputTxt.replace('و', ',');
                    var inputTxt1 = '<%#Session["InputBarcode"] != null ? Session["InputBarcode"].ToString() : "" %>';
                    var paramss = '{input:"' + inputTxt + '",reworkACodes:"",reworkReasons:"",reworkDesc:"",reworkEsghatMode:"",reworkEsghatPrevProcessId:""}';

                    var parts = inputTxt.split(",");

                    if (parts[2] == "1000") { //rework
                        $("#txtReworkPassword").val('');
                        $("#txtEsghatPassword").val('');
                        $('#reworkModal').modal('show');
                    }
                    else if (parts[2] == "1001") { //esghat
                        $("#txtReworkPassword").val('');
                        $("#txtEsghatPassword").val('');
                        $('#esghatModal').modal('show');
                    }
                    else
                        addRow(paramss);
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

        function addRow(paramss) {
            $.ajax({
                url: '<%= ResolveUrl("~/workline.aspx/AddRow") %>',
                type: "POST",
                data: paramss,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d == "OK") {
                        $("#txtBarcodeInput").val('');
                        toastr["success"]("ردیف با موفقیت اضافه شد")
                        $("#txtBarcodeInput").val('');
                        setTimeout(function () { window.location.href = window.location.origin + "/workline.aspx"; }, 1200);
                    }
                    else {
                        beep();
                        toastr["error"](data.d).css("width", "500px");
                        setTimeout(() => { console.log("خطا در سیستم " + data.d); }, 1000);
                        $("#txtBarcodeInput").val('');
                        $('#esghatReworkInputModal').modal('hide');
                    };
                },
                error: function (data) {
                    beep();
                    debugger;
                    toastr["error"]("خطا در سیستم " + data.Message).css("width", "500px");
                    setTimeout(() => { console.log("خطا در سیستم " + data.Message); }, 1000);
                    $('#esghatReworkInputModal').modal('hide');
                }
            });
        }

        function cancel() {
            $('#reworkModal').modal('hide');
            $('#esghatModal').modal('hide');
        }

        function checkReworkPassword() {
            var paramss = '{input:"' + $("#txtReworkPassword").val() + '"}'
            $.ajax({
                url: '<%= ResolveUrl("~/workline.aspx/CheckReworkEsghatPassword") %>',
                type: "POST",
                data: paramss,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d == "OK") {
                        $('#reworkModal').modal('hide');
                        $('#reworkEsghatMode').val('Rework');
                        $('#esghatReworkInputModal').modal('show');
                    }
                    else {
                        beep();
                        toastr["error"]("رمز اشتباه است").css("width", "500px");
                    };
                },
                error: function (data) {
                    beep();
                    toastr["error"]("خطا در سیستم " + data.Message).css("width", "500px");
                    setTimeout(() => { console.log("خطا در سیستم " + data.Message); }, 1000);
                    return 0;
                }
            });
        }

        function checkEsghatPassword() {
            var paramss = '{input:"' + $("#txtEsghatPassword").val() + '"}'
            $.ajax({
                url: '<%= ResolveUrl("~/workline.aspx/CheckReworkEsghatPassword") %>',
                type: "POST",
                data: paramss,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d == "OK") {
                        $('#esghatModal').modal('hide');
                        $('#reworkEsghatMode').val('Esghat');
                        $('#esghatReworkInputModal').modal('show');
                    }
                    else {
                        beep();
                        toastr["error"]("رمز اشتباه است").css("width", "500px");
                    };
                },
                error: function (data) {
                    beep();
                    toastr["error"]("خطا در سیستم " + data.Message).css("width", "500px");
                    setTimeout(() => { console.log("خطا در سیستم " + data.Message); }, 1000);
                    return 0;
                }
            });
        }

        $('#esghatReworkInputModal')
            .on('show.bs.modal', function () {
                cloneCount = 0;

                try {$("#row1").remove();} catch (e) {}
                try {$("#row2").remove();} catch (e) {}
                try {$("#row3").remove();} catch (e) {}
                try {$("#row4").remove();} catch (e) {}
                try {$("#row5").remove();} catch (e) {}
                                
                var inputText = $("#txtBarcodeInput").val().replace('و', ',').replace('#', '');
                var reworkEsghatMode = $('#reworkEsghatMode').val();

                if (reworkEsghatMode == 'Rework')
                    $('#h3ReworkEsghat').html('ثبت دوباره کاری');
                else
                    $('#h3ReworkEsghat').html('ثبت اسقاط');

                $("#drpOp").val(inputText.split(",")[1]);
                $("#txtReworkDesc").val('');
                
                var paramsss = '{worksheetId:' + inputText.split(",")[0] + ',operatorId:' + inputText.split(",")[1] + '}'
                $.ajax({
                    url: '<%= ResolveUrl("~/workline.aspx/GetLastProcessOfWorksheet") %>',
                    type: "POST",
                    data: paramsss,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        debugger;
                        $("#drpPrevProcess").val(data.d);
                        loadACodes(inputText.split(",")[0]);
                    },
                    error: function (data) {
                        debugger;
                        beep();
                        toastr["error"]("خطا در سیستم " + data.Message).css("width", "500px");
                        setTimeout(() => { console.log("خطا در سیستم " + data.Message); }, 1000);
                        return 0;
                    }
                });
            });

        function addReworkEsghat() {
            debugger;
            var inputText = $("#txtBarcodeInput").val().replace('و', ',').replace('#', '');


            var reworkACodes = $('.multiDropDown').find(":selected");

            var selectedCodes = "";
            for (var i = 0; i < reworkACodes.length; i++) {
                selectedCodes += "," + reworkACodes[i].value;
            }

            if (selectedCodes == "" || selectedCodes == ",") {
                toastr["error"]("هیچ کد کالایی انتخاب نشده ");
                return;
            }

            selectedCodes = selectedCodes.substring(1, selectedCodes.length);


            var reworkReasons = $('.drpRR').find(":selected");

            var selectedReasons = "";
            for (var i = 0; i < reworkReasons.length; i++) {
                selectedReasons += "," + reworkReasons[i].value;
            }

            selectedReasons = selectedReasons.substring(1, selectedReasons.length);

            if (selectedReasons == "" || selectedReasons == ",") {
                toastr["error"]("هیچ علتی انتخاب نشده ");
                return;
            }

            var reworkDesc = $("#txtReworkDesc").val();
            var reworkEsghatMode = $('#reworkEsghatMode').val();
            var reworkEsghatPrevProcessId = $('#drpPrevProcess').find(":selected").val();

            var paramss = '{input:"' + inputText + '",reworkACodes:"' + selectedCodes + '",reworkReasons:"' + selectedReasons + '",reworkDesc:"' + reworkDesc + '",reworkEsghatMode:"' + reworkEsghatMode + '","reworkEsghatPrevProcessId":"' + reworkEsghatPrevProcessId + '"}';

            addRow(paramss);
        }


        $(document).ready(function () {
            //$('#multiAcodeDD0').select2({
            //    width: '120px'
            //});
        });

        function loadACodes(worksheetId, acodeSelectId) {
            debugger;
            if (acodeSelectId ==undefined || acodeSelectId == '')
                acodeSelectId = 'multiAcodeDD0';

            var paramss = '{worksheetId:' + worksheetId + '}';

            $.ajax({
                url: '<%= ResolveUrl("~/workline.aspx/GetRelatedACodes") %>',
                type: "POST",
                data: paramss,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger;
                    var res = data.d.replace(/\"/g, '').replace('[', '').replace(']', '').split(",");

                    if (acodeSelectId == 'multiAcodeDD0') {
                        var element = $('.multiDropDown');

                        if (element != undefined) {
                            element.empty();
                            //element.select2('destroy');
                        }

                        //element.select2({
                        //    width: '150px'
                        //});

                        element.val(null).trigger("change");

                        for (var i = 0; i < res.length; i++) {
                            var data = {
                                id: res[i],
                                text: res[i]
                            };

                            var newOption = new Option(data.text, data.id, false, false);
                            element.append(newOption).trigger('change');
                        }
                    }
                    else {



                        $('#' + acodeSelectId).empty();
                        //$('#' + acodeSelectId).select2('destroy');

                        //$('#' + acodeSelectId).select2({
                        //    width: '120px'
                        //});

                        //$('#' + acodeSelectId).val(null).trigger("change");

                        for (var i = 0; i < res.length; i++) {
                            var data = {
                                id: res[i],
                                text: res[i]
                            };

                            var newOption = new Option(data.text, data.id, false, false);
                            $('#' + acodeSelectId).append(newOption).trigger('change');
                        }
                    }
                },
                error: function (data) {
                    debugger;
                    beep();
                    toastr["error"]("خطا در سیستم " + data.Message).css("width", "500px");
                    setTimeout(() => { console.log("خطا در سیستم " + data.Message); }, 1000);
                    return 0;
                }
            });
        }

        function addNewAcodeReasonRow() {
            debugger;
            cloneCount++;
            var newRow = $("#row0").clone(true).attr('id', 'row' + cloneCount);

            newRow.appendTo("#rowsContainer");

            $("#row" + cloneCount).append('<button type="button" onclick="deleteRow(this);">-</button>');

            var newMultiACode = newRow.find('.multiDropDown')[0];
            newMultiACode.id = 'multiAcodeDD' + cloneCount;

            //$('#' + newMultiACode.id).select2({
            //    width: '150px'
            //});

            loadACodes($("#txtBarcodeInput").val().replace('و', ',').replace('#', '').split(',')[0], newMultiACode.id);

            var newReason = newRow.find('.drpRR')[0];
            newReason.id = 'drpReworkReason' + cloneCount;


        }

        function deleteRow(item) {
            debugger;
            item.parentElement.remove();
        }
    </script>
</asp:Content>

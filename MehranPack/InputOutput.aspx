<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InputOutput.aspx.cs" Inherits="MehranPack.InputOutput" %>

<%@ Register Src="UserControls/PersianCalender.ascx" TagName="PersianCalender" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <h3 runat="server" id="h3Header">ثبت اطلاعات ورود و خروج</h3>
                <hr class="hrBlue" />
            </div>
        </div>


        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


        <div class="row">
            <div class="col-sm-1" align="left">
                تاریخ:
            </div>

            <div class="col-sm-4">
                <uc:PersianCalender runat="server" ID="dtInputOutput" AllowEmpty="False" TimePanel-Visible="False" />
            </div>

            <div runat="server" id="divReceiptId">
                <div class="col-sm-2 col-sm-offset-1" align="left">
                    شناسه رسید:
                </div>

                <div class="col-sm-4">
                    <asp:TextBox runat="server" ID="txtInReceiptId"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-10">
                <asp:GridView runat="server" AutoGenerateColumns="False" Width="512px" ID="gridInput" CssClass="table table-bordered table-striped" DataKeyNames="Id"
                    OnRowCommand="gridSource_RowCommand" AllowPaging="True" PageSize="10"
                    OnPageIndexChanging="gridSource_OnPageIndexChanging" EmptyDataText="اطلاعاتی جهت نمایش وجود ندارد"
                    OnRowDeleting="gridInput_OnRowDeleting" OnRowDeleted="gridInput_OnRowDeleted">

                    <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Wrap="False" />

                    <Columns>
                        <asp:BoundField DataField="ProductId" HeaderText="شناسه کالا" Visible="False">
                            <ControlStyle BorderColor="#FFFF99" BorderStyle="Solid" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Code" HeaderText="کد کالا">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Name" HeaderText="عنوان کالا">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CategoryName" HeaderText="گروه کالا">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="InsertDateTime" HeaderText="تاریخ">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CustomerName" HeaderText="مشتری">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Count" HeaderText="تعداد">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ProductionQuality" HeaderText="کیفیت">
                            <HeaderStyle HorizontalAlign="Center" />
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
                                    Text="حذف" CommandArgument='<%# Eval("ProductId") %>' />
                                <asp:Image runat="server" ImageUrl="Images/Delete16.png" />
                            </ItemTemplate>
                            <ItemStyle Width="70"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


            </div>
        </div>

        <div class="row">
            <div class="col-md-11">

                <asp:HiddenField runat="server" ID="hfProductId" />

                <div class="row">
                    <div class="col-md-1 col-sm2 margin-top-7" align="left">
                        کد کالا
                    </div>
                    <div class="col-md-2 col-sm2">

                        <asp:TextBox runat="server" ID="txtProductCode" CssClass="form-control form-control-custom" ClientIDMode="Static"></asp:TextBox>
                        <button type="button" class="btn btn-default btn-xs" data-toggle="modal" data-target="#myModal">+</button>

                    </div>

                    <div class="col-md-1 col-sm2 margin-top-7" align="left">
                        نام کالا
                    </div>
                    <div class="col-md-3 col-sm3">
                        <asp:TextBox runat="server" ID="txtProductName" ReadOnly="True" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="col-md-1 col-sm-2 margin-top-7" align="left">
                        گروه کالا
                    </div>
                    <div class="col-md-3 col-sm-3">
                        <asp:TextBox runat="server" ID="txtCatName" ReadOnly="True" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-1 col-sm2 margin-top-7" align="left">
                        تاریخ
                    </div>
                    <div class="col-md-2 col-sm2" id="divDate">
                        <uc:PersianCalender runat="server" ID="dtInputOutputDetail" AllowEmpty="False" TimePanel-Visible="False" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-1 col-sm2 margin-top-7" align="left">
                        تعداد
                    </div>
                    <div class="col-md-2 col-sm2">
                        <asp:TextBox runat="server" ID="txtCount" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-1 col-sm2 margin-top-7" align="left">
                        کیفیت
                    </div>
                    <div class="col-md-2 col-sm2">
                        <asp:RadioButtonList runat="server" ID="rbQuality" DataValueField="ProductionQuality" BorderWidth="1" BorderStyle="Solid" RepeatColumns="3">
                            <asp:ListItem Value="1">بد</asp:ListItem>
                            <asp:ListItem Value="2">متوسط</asp:ListItem>
                            <asp:ListItem Value="3">خوب</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-1 col-sm2 margin-top-7" align="left">
                        مشتری
                    </div>
                    <div class="col-md-2 col-sm2">
                        <asp:DropDownList runat="server" ID="drpCustomer" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-1 col-sm2" align="left">
                    </div>
                    <div class="col-md-2 col-sm2">
                        <asp:Button runat="server" ID="btnAdd" Text="  +  " OnClick="btnAdd_OnClick" CssClass="btn btn-default btn-info" />
                    </div>
                </div>

            </div>
        </div>
        <hr class="hrGray" />
        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info btn-standard" Text="ثبت" OnClick="btnSave_OnClick" />

        <div class="row">
            <div class="col-md-10 col-md-offset-2">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </div>
        </div>
        <!-- Modal -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">انتخاب کالا</h4>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label>جستجو</label>
                                <asp:TextBox runat="server" ID="txtSearchTree" placeholder="نام کالا"></asp:TextBox>
                                <asp:Button runat="server" ID="b1" OnClick="b1_OnClick" Text="جستجو" />
                                <%--<asp:Button runat="server" ID="btnClearSearch" OnClick="btnClearSearch_OnClick" Text="جستجو" />--%>
                                <br />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TreeView runat="server" ID="tv1" OnSelectedNodeChanged="tv1_OnSelectedNodeChanged"></asp:TreeView>
                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="b1" EventName="Click" />
                                    </Triggers>

                                </asp:UpdatePanel>

                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">انصراف</button>
                    </div>
                </div>

            </div>
        </div>

    </div>

    <script type="text/javascript">
        function onNodeClicked(productId, catId) {
            $('#myModal').modal('hide');
            $('#hfProductId').val(productId);

            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/InputOutput.aspx/GetProductCodeAndName") %>',
                data: '{productId:' + productId + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onSuccess,
                failure: function (response) {
                    alert('error in GetProductName!');
                }
            });

            function onSuccess(response) {
                $('#txtProductCode').val(response.d.split(',')[0]);
                $('#txtProductName').val(response.d.split(',')[1]);
            };

            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/InputOutput.aspx/GetCategoryName") %>',
                data: '{catId:' + catId + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onSuccess2,
                failure: function (response) {
                    alert('error in GetCatName!');
                }
            });

            function onSuccess2(response) {
                $('#txtCatName').val(response.d);
            };
        }

    </script>

</asp:Content>

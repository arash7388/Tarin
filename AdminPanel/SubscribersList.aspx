<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="SubscribersList.aspx.cs" Inherits="AdminPanel.SubscribersList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <%--<script type="text/javascript">
            var deleteWindow = null;
            var gridSubscribers = null;
            var panelStep1 = null;
            var panelStep2 = null;

            function pageLoad() {
                gridSubscribers = $find("<%= gridSubscribers.ClientID %>");
                deleteWindow = $find("<%= RadWindow1.ClientID %>");
                panelStep1 = $get("<%= FirstStepPanel.ClientID %>");
                panelStep2 = $get("<%= SecondStepPanel.ClientID %>");
            }
        </script>--%>
    </telerik:RadCodeBlock>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
     <h3>مشترکین</h3>
    <div class="row">
        <div class="col-md-10">
            <telerik:RadGrid ID="gridSubscribers" runat="server" AllowFilteringByColumn="True" 
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                OnDeleteCommand="gridSubscribers_OnDeleteCommand" CellSpacing="-1" GridLines="Both">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
                <MasterTableView DataKeyNames="Id">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="column" Visible="False">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" HeaderText="آدرس ایمیل">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>

                            <HeaderStyle Width="200px"></HeaderStyle>
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn
                            AllowFiltering="false">
                            <HeaderStyle Width="100px" />
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="btnDelete" runat="server" Text="حذف" OnClientClick='<%# String.Format("openConfirmationWindow(\"{0}\",\"{1}\"); return false;", Eval("Id"), Eval("Email")) %>'/>--%>
                                <asp:LinkButton ID="btnDelete" runat="server" Text="حذف" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
    <%--<telerik:RadWindow ID="RadWindow1" runat="server" VisibleTitlebar="false" Modal="true" AutoSize="False"
                Behaviors="None" VisibleStatusbar="false">
                <ContentTemplate>
                    <asp:Panel ID="FirstStepPanel" runat="server">
                        <div class="bookNowFrame">
                            <div class="bookNowTitle">
                                آیا از حذف مشترک زیر اطمینان دارید؟
                            </div>
                            <br/>
                            <span id="selectedSubscriber"></span>
                            <hr class="lineSeparator" style="margin: 12px 0 12px 0" />
                      
                            <telerik:RadButton ID="btnDelete" runat="server" Text="بله"
                                Width="100px"  UseSubmitBehavior="false" OnClick="btnDelete_OnClick" />

                            <telerik:RadButton ID="btnCancel" runat="server" Text="خیر"
                                Width="100px" OnClientClicking=" btnCancelClick " UseSubmitBehavior="false" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="SecondStepPanel" runat="server" Style="display: none; padding: 120px 20px 0 30px; width: 480px;">
                        <div style="float: left;">
                            
                        </div>
                        <div style="float: left; padding: 10px 0 0 20px;">
                            <div class="bookNowComplete">
                                مشترک مورد نظر با موفقیت حذف شد
                            </div>
                            <hr class="lineSeparator" style="margin: 10px 10px 20px 0" />
                            <telerik:RadButton ID="BookNowCloseButton" runat="server" Text="بستن"
                                Width="100px" OnClientClicking=" deleteWindowCloseClick " UseSubmitBehavior="True" />
                        </div>
                        <div style="clear: both">
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadWindow>--%>
    <%-- <script type="text/javascript">

        function openConfirmationWindow(id, email) {
            debugger;
            $id = id;
            document.getElementById("selectedSubscriber").innerHTML = email;
            //$("#selectedSubscriber").innerHTML = "2222222222222 ";
            //deleteWindow.set_title(email);
            deleteWindow.show();
        }

        function deleteWindowCloseClick(sender, args) {
            deleteWindow.close();
            togglePanels();
            gridSubscribers.get_masterTableView().fireCommand("UpdateCount", deleteWindow.get_title());
            args.set_cancel(true);
        }

        function btnDeleteClick(sender, args) {
            //togglePanels();
            //args.set_cancel(true);
            debugger;
            $.ajax({
                type: "POST",
                url: "SubscribersList.aspx/DeleteSubscriber",
                data: "{'id':'" + $id + "' }", //Pass the parameter names and values
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("Error- Status: " + textStatus + " jqXHR Status: " + jqXHR.status + " jqXHR Response Text:" + jqXHR.responseText);
                    alert('خطا در حذف- لطفا مجددا تلاش کنید');
                },
                success: function (msg) {
                    if (msg.d == true) {
                        togglePanels();
                        //alert('ایمیل شما با موفقیت ارسال شد');
                        //document.getElementById('lblMailInfo').InnerHTML = 'label text';
                        //document.getElementById("lblMailInfo").value = "با موفقیت ارسال شد";

                    }
                    else {
                        //show error
                        alert('خطا در حذف- لطفا مجددا تلاش کنید');
                    }
                }
            });
        }

        function btnCancelClick(sender, args) {
            deleteWindow.close();
            args.set_cancel(true);
        }

        function togglePanels() {
            var step1Visible = panelStep1.style.display != "none";
            panelStep1.style.display = step1Visible ? "none" : "";
            panelStep2.style.display = step1Visible ? "" : "none";
        }
    </script>--%>
</asp:Content>

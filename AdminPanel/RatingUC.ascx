<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RatingUC.ascx.cs" Inherits="MTO.Admin.RatingUC" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="aj">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadRating1"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>

    </AjaxSettings>
</telerik:RadAjaxManager>--%>

<%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
</telerik:RadAjaxLoadingPanel>--%>

<%--<telerik:RadAjaxPanel runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
    <asp:Label runat="server" ID="lblRatingText"></asp:Label>
    <div id="div_ratingItems">
        <telerik:RadRating ID="RadRating1" runat="server" ItemCount="5"
            Value="3" SelectionMode="Continuous" Precision="Half" Orientation="Horizontal" OnRate="RadRating1_OnRate">
        </telerik:RadRating>
    </div>
    <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
</telerik:RadAjaxPanel>--%>


<div class="divRatings">
        <asp:Panel ID="RadRating_wrapper" runat="server">
            <div style="padding: 15px; padding-left: 20px;">
                The Godfather (1972)<telerik:RadRating ID="RadRating2" runat="server" ItemCount="5"
                    Value="3" SelectionMode="Continuous" Precision="Half" Orientation="Horizontal">
                </telerik:RadRating>
            </div>
        </asp:Panel>
    </div>


<%--<telerik:RadAjaxLoadingPanel runat="server" ID="LoadingPanel"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LoadingPanel">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rblNumberStars" EventName="SelectedIndexChanged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadRating_wrapper"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rblDirection" EventName="SelectedIndexChanged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadRating_wrapper"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rblSelectionMode" EventName="SelectedIndexChanged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadRating_wrapper"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rblChoosePrecision" EventName="SelectedIndexChanged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadRating_wrapper"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rblOrientation" EventName="SelectedIndexChanged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadRating_wrapper"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>--%>

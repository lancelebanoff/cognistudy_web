<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnalyticsPanel.ascx.cs" Inherits="CogniTutor.UserControls.AnalyticsPanel" %>

<%@ Register TagPrefix="COG" TagName="DoughnutChart" Src="~/UserControls/DoughnutChart.ascx" %>
<%@ Register TagPrefix="COG" TagName="SingleBarChart" Src="~/UserControls/SingleBarChart.ascx" %>

<asp:UpdatePanel ID="AllChartsUpdatePanel" runat="server">
    <ContentTemplate>

                                            
        <asp:Panel runat="server" ID="pnlStudentDropdown" class="row">
            <div class="col-lg-6">
                <asp:Button runat="server" ID="btnUpdatePanels" OnClick="btnUpdatePanels_Click" CssClass="hidden"/>
                <p class="medium-text">Student</p>
                <asp:DropDownList CssClass="form-control" ID="ddlFilterStudent" runat="server" AutoPostBack="true"
                    DataTextField="studentName" DataValueField="objectId" OnSelectedIndexChanged="btnUpdatePanels_Click"></asp:DropDownList>
                <br/>
            </div>
            <div class="col-lg-6">
            </div>
        </asp:Panel>
                                            
        <div class="row">
            <div class="col-lg-6">
                <p class="medium-text">Subject</p>
                <asp:DropDownList CssClass="form-control" ID="ddlFilterSubject" runat="server"  AutoPostBack="true"
                        OnSelectedIndexChanged="btnUpdatePanels_Click"></asp:DropDownList>
                <br />
            </div>
            <div class="col-lg-6">
                <p class="medium-text">Category</p>
                <asp:DropDownList CssClass="form-control" ID="ddlFilterCategory" runat="server"  AutoPostBack="true"
                        OnSelectedIndexChanged="btnUpdatePanels_Click" Enabled="false"></asp:DropDownList>
                <br />
            </div>
        </div>
                                            
        <div class="row">
            <div class="col-lg-6">
                <p class="medium-text">Time</p>
                <asp:DropDownList CssClass="form-control" ID="ddlFilterTime" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="btnUpdatePanels_Click">
                    <asp:ListItem Text="Past Week" Value="PastWeek"></asp:ListItem>
                    <asp:ListItem Text="Past Month" Value="PastMonth"></asp:ListItem>
                    <asp:ListItem Text="All Time" Value="AllTime"></asp:ListItem>
                </asp:DropDownList>
                <hr />
            </div>
        </div>
                                            
        <div class="row">
            <div class="col-lg-6">
                <asp:Label runat="server" ID="lbNoResults" Text="No analytics to show" Visible="false" CssClass="medium-text"></asp:Label>
                <asp:Panel ID="pnlTest" runat="server"></asp:Panel>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server"></asp:UpdatePanel>
            </div>
            <div class="col-lg-6">
                <asp:UpdatePanel ID="catUpdatePanel1" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="catUpdatePanel2" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="catUpdatePanel3" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="catUpdatePanel4" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="catUpdatePanel5" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="catUpdatePanel6" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="catUpdatePanel7" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="catUpdatePanel8" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="catUpdatePanel9" runat="server"></asp:UpdatePanel>
                <asp:UpdatePanel ID="catUpdatePanel10" runat="server"></asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="AllChartsUpdatePanel" DisplayAfter="0">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </ContentTemplate>
</asp:UpdatePanel>
                                    
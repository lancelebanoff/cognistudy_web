<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Analytics.aspx.cs" Inherits="CogniTutor.Analytics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>
<%@ Register TagPrefix="COG" TagName="DoughnutChart" Src="~/UserControls/DoughnutChart.ascx" %>
<%@ Register TagPrefix="COG" TagName="SingleBarChart" Src="~/UserControls/SingleBarChart.ascx" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Analytics</title>

    <link rel="shortcut icon" type="image/x-icon" href="Images/CogniTutor5.jpg" />

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/modern-business.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <!-- jQuery -->
    <script src="js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>
    
    <!-- Our custom javascript -->
    <script src="js/Custom.js"></script>

    <!-- Morris Charts CSS -->
    <link href="css/plugins/morris.css" rel="stylesheet">

</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <!--Login modal-->
        <COG:LoginWindow runat="server" />
        <!-- Navigation bar -->
        <COG:NavigationBar runat="server"/>

    <!-- Page Content -->
    <div class="container">

        <div class="row">
            <div class="col-lg-12">
                <div id="wrapper">

                    <div id="page-wrapper">

                        <div class="container-fluid">

                            <!-- Page Heading -->
                            <div class="row">
                                <div class="col-lg-12">
                                    <h1 class="page-header">
                                        Analytics
                                    </h1>
                                    <ol class="breadcrumb hidden">
                                        <li class="active">
                                            <i class="fa fa-dashboard"></i> Analytics
                                        </li>
                                    </ol>
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="col-lg-12">
                                    

                                    <asp:UpdatePanel ID="AllChartsUpdatePanel" runat="server">
                                        <ContentTemplate>

                                            
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <asp:Button runat="server" ID="btnUpdatePanels" OnClick="btnUpdatePanels_Click" CssClass="hidden"/>
                                                    <p class="medium-text">Student</p>
                                                    <asp:DropDownList CssClass="form-control" ID="ddlFilterStudent" runat="server" AutoPostBack="true"
                                                        DataTextField="studentName" DataValueField="objectId" OnSelectedIndexChanged="btnUpdatePanels_Click"></asp:DropDownList>
                                                    <br/>
                                                </div>
                                                <div class="col-lg-6">
                                                </div>
                                            </div>
                                            
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
                                    
                                    
                                    <div class="row">
                                    </div>
                                    

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">

                                </div>
                            </div>



                        </div>
                        <!-- /.container-fluid -->

                    </div>
                    <!-- /#page-wrapper -->

                </div>
                <!-- /#wrapper -->

            </div>
        </div>
        <hr>
    </div>
            
    <!-- Footer -->
    <COG:Footer runat="server" />
    </form>

</body>

</html>

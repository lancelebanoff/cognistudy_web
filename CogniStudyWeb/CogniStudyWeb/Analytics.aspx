﻿<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Analytics.aspx.cs" Inherits="CogniTutor.Analytics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>
<%@ Register TagPrefix="COG" TagName="AnalyticsPanel" Src="~/UserControls/AnalyticsPanel.ascx" %>

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
                                    
                                    <COG:AnalyticsPanel runat="server" id="pnlAnalytics" ShowStudentDropdown="true"></COG:AnalyticsPanel>
                                    
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

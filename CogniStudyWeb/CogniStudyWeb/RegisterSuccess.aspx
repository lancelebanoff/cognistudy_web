<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="RegisterSuccess.aspx.cs" Inherits="CogniTutor.RegisterSuccess" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>CogniTutor</title>

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

    <!-- Script to Activate the Carousel -->
    <script>
        $('.carousel').carousel({
            interval: 5000 //changes the speed
        })
    </script>
    
    <!-- Our custom javascript -->
    <script src="js/Custom.js"></script>

</head>

<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <!-- Navigation bar -->
        <COG:NavigationBar runat="server"/>

        <!--Login modal-->
        <COG:LoginWindow runat="server" />

        <!-- Page Content -->
        <div class="container">
            
            <!-- Search Bar -->
            <div class="row">
                <div class="col-lg-2">

                </div>
                <div class="col-lg-8">
                    <h1 class="page-header align-center">
                        Registration Successful!</h1>
                    <div class="panel-body align-center">
                        <p class="panel-text"><span style="color: rgb(0, 0, 0); font-family: segoe_uiregular; 
                            font-size: 16px; font-style: normal; font-variant: normal; font-weight: normal; 
                            letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; 
                            text-indent: 0px; text-transform: none; white-space: normal; widows: auto; 
                            word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; 
                            float: none;">An email verification was sent to your mail box. Please check your email 
                            and click on the verification link in the email. If you do not receive an email within 
                            5 - 10 minutes, please contact us at support@cognitutor.com.
                    </div>
                    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                </div>
            </div>
            
        </div>
        <!-- /.container -->
            
        <!-- Footer -->
        <COG:Footer runat="server" />
    </form>

</body>

</html>


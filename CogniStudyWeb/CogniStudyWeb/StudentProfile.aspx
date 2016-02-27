<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="CogniTutor.StudentProfile" %>

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

    <title>Profile</title>

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

        <div class="row">
            <div class="col-lg-1"></div>
            <div class="container target col-lg-11">
                <hr class="">
                <div class="row float-bottom">
                    <div class="col-sm-2 col-xs-5">
                        <asp:Image ID="Image1" class="img-rounded img-responsive" runat="server" />
                        <asp:Image class="hidden cursor-pointer" ID="edit" src="Images/edit.png" Style="position: absolute; bottom: 5px; right: 20px;" runat="server"
                            onclick="picClick()" onmouseenter="mouseOverProfilePicture()" onmouseleave="mouseLeaveProfilePicture()" />
                        <asp:FileUpload class="hidden" ID="FileUpload1" runat="server" onchange="fileChosen()" />
                        <!--<asp:Button class="hidden" ID="btnUpload" runat="server" Text="Upload" />-->
                    </div>
                    <div class="col-sm-10 col-xs-7">
                        <h1><%= StudentName %></h1>
                        <asp:Button runat="server" ID="btnRequestStudent" OnClick="btnRequestStudent_Click" class="btn btn-success" Text="Add student" />
                        <asp:Button runat="server" ID="btnSendMessage" OnClick="btnSendMessage_Click" class="btn btn-info" Text="Send a message" />
                        <asp:Button runat="server" ID="btnBlockStudent" OnClick="btnBlockStudent_Click" class="btn btn-danger" Text="Block student" />
                    </div>
                </div>
            </div>
        </div>
            
    <!-- Footer -->
    <COG:Footer runat="server" />
    </form>

</body>

</html>

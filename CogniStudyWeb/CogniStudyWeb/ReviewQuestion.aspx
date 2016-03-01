﻿<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="ReviewQuestion.aspx.cs" Inherits="CogniTutor.ReviewQuestion" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>
<%@ Register TagPrefix="COG" TagName="QuestionBlock" Src="~/UserControls/QuestionBlock.ascx" %>

<!DOCTYPE html>
<html lang="en">
     
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Question Arena</title>

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

    <script>
        function showhide(checkboxid, layerid) {
            if ($('#' + checkboxid).checked == true) {
                $('#' + layerid).removeClass('hidden');
            }
            else {
                $('#' + layerid).addClass('hidden');
            }
        }
    </script>

    <link href="css/question.css" rel="stylesheet">

    <script type="text/javascript" async
        src="https://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-MML-AM_CHTML">
    </script>


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
                                        Question Arena
                                    </h1>
                                    <ol class="breadcrumb hidden">
                                        <li class="active">
                                            <i class="fa fa-dashboard"></i> Question Arena
                                        </li>
                                    </ol>
                                </div>
                            </div>
                            <!-- /.row -->
                            <asp:Label ID="lbNoResults" runat="server" Visible="false" class="large-text" Text="No questions to be reviewed."></asp:Label>
                            <asp:Panel ID="pnlAll" runat="server" class="row">
                                <div class="col-lg-12">
                                    <% if(Bundle != null) { %>
                                    <div class="row">
                                        <div class="col-lg-3"></div>
                                        <div class="col-lg-6">
                                            <h3 class="align-center">Passage</h3><br />
                                            <img src="<%= Bundle.Image.Url.ToString() %>" style="max-width:100%; max-height:100%;" />
                                            <p><%= passageText %></p>
                                            <hr />
                                        </div>
                                    </div>
                                    <% } %>
                                    <asp:Panel runat="server" id="pnlQuestions"></asp:Panel>
                                    
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <asp:TextBox runat="server" ID="tbComments" placeholder="Type your comments here..." 
                                                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <br />
                                            <div class="row align-center">
                                                <asp:Button runat="server" id="btnApprove" CssClass="btn btn-success btn-lg" Width="150" Text="Approve" OnClick="btnApprove_Click" />
                                                <asp:Button runat="server" id="btnDeny" CssClass="btn btn-danger btn-lg" Width="150" Text="Deny" OnClick="btnDeny_Click" />
                                                <asp:Button runat="server" id="btnSkip" CssClass="btn btn-default btn-lg" Width="150" Text="Skip" OnClick="btnSkip_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <!-- /.row -->



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

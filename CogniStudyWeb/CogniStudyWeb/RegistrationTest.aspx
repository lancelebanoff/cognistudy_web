<%@ Page ValidateRequest="false" Async="true" Language="C#" AutoEventWireup="true" CodeBehind="RegistrationTest.aspx.cs" Inherits="CogniTutor.RegistrationTest" %>

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

    <title>Registration</title>

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
                                        Registration Test
                                    </h1>
                                    <ol class="breadcrumb hidden">
                                        <li class="active">
                                            <i class="fa fa-dashboard"></i> Question Arena
                                        </li>
                                    </ol>
                                    <p class="medium-text align-center">Please complete the following test. You must score at least 70% to complete registration.</p>
                                </div>
                            </div>

                            <asp:Panel runat="server" ID="pnlError" class="alert alert-danger" Visible="false">
                              One or more questions are unanswered
                            </asp:Panel>
                            <!-- /.row -->
                            <asp:Panel ID="pnlAll" runat="server" class="row">
                                <div class="col-lg-12">
                                    <asp:Panel runat="server" id="pnlQuestions">
                                        <COG:QuestionBlock runat="server" id="QuestionBlock0" Checkable="true"></COG:QuestionBlock>
                                        <COG:QuestionBlock runat="server" id="QuestionBlock1" Checkable="true"></COG:QuestionBlock>
                                        <COG:QuestionBlock runat="server" id="QuestionBlock2" Checkable="true"></COG:QuestionBlock>
                                        <COG:QuestionBlock runat="server" id="QuestionBlock3" Checkable="true"></COG:QuestionBlock>
                                        <COG:QuestionBlock runat="server" id="QuestionBlock4" Checkable="true"></COG:QuestionBlock>
                                        <COG:QuestionBlock runat="server" id="QuestionBlock5" Checkable="true"></COG:QuestionBlock>
                                        <COG:QuestionBlock runat="server" id="QuestionBlock6" Checkable="true"></COG:QuestionBlock>
                                        <COG:QuestionBlock runat="server" id="QuestionBlock7" Checkable="true"></COG:QuestionBlock>
                                        <COG:QuestionBlock runat="server" id="QuestionBlock8" Checkable="true"></COG:QuestionBlock>
                                        <COG:QuestionBlock runat="server" id="QuestionBlock9" Checkable="true"></COG:QuestionBlock>
                                    </asp:Panel>
                                    <asp:Button CssClass="btn btn-success center-block" runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit"/>
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

<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="QuestionArena.aspx.cs" Inherits="CogniTutor.QuestionArena" %>

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

    <title>Question Arena</title>

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
                            
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Button runat="server" ID="btnUploadQuestion" OnClick="btnUploadQuestion_Click" class="btn btn-default" Text="Upload New Question"></asp:Button>
                                    <a href="ReviewQuestion.aspx" class="btn btn-default <%= CogniTutor.Constants.UserType.IsQualifiedToReviewQuestions(UserType) ? "" : "hidden" %>">
                                        Review Questions</a>
                                </div>
                            </div>
                            <!-- /.row -->
                            
                            <div class="row">
                                <div class="col-lg-12">
                                    <h3 class="page-header">My Question Status</h3>
                                    <asp:Label runat="server" ID="lblNoQuestions" Text="No questions to show" CssClass="medium-text" Visible="false"></asp:Label>
                                    <asp:GridView ID="grdStatus" runat="server" OnRowCommand="grdStatus_RowCommand" 
                                        CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="objectId">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Subject/Category">
                                                <ItemTemplate>
                                                    <%# Eval("subject") + " - " + Eval("category") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Question">
                                                <ItemTemplate>
                                                    <%# Eval("questionText") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Answers">
                                                <ItemTemplate>
                                                    <%# Eval("answers") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <%# Eval("reviewStatus") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comments">
                                                <ItemTemplate>
                                                    <%# Eval("comments") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btn btn-default align-center btn"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
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

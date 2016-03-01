<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="AssignQuestion.aspx.cs" Inherits="CogniTutor.SuggestQuestion" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Assign Question</title>

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
    
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>
    
    <!-- Our custom javascript -->
    <script src="js/Custom.js"></script>

    <link href="css/question.css" rel="stylesheet">

    <script type="text/javascript" async
        src="https://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-MML-AM_CHTML">
    </script>

    <script type="text/javascript" src="js/bs.pagination.js"></script>

    <link href="css/customtooltip.css" rel="stylesheet"/>

    <script>
        $(document).on('mouseenter', ".row_dd", function (e) {
            var self = $(this).find(".dataDiv");
            var question = self.attr("data-question");
            var answer1 = self.attr("data-answer1");
            var answer2 = self.attr("data-answer2");
            var answer3 = self.attr("data-answer3");
            var answer4 = self.attr("data-answer4");
            var answer5 = self.attr("data-answer5");
            var explanation = self.attr("data-explanation");
            var correctAnswer = self.attr("data-correctAnswer");
            var image = self.attr("data-image");

            $("#p_question").html(question);
            $("#b_answer1").html(answer1);
            $("#b_answer2").html(answer2);
            $("#b_answer3").html(answer3);
            $("#b_answer4").html(answer4);
            $("#b_answer5").html(answer5);
            $("#p_explanation").html(explanation);
            $("#img_image").attr("src",image);
            if (answer5 == "") {
                $("#b_answer5").hide();
            }
            else {
                $("#b_answer5").show();
            }
            $("#b_answer1").removeClass("btn-success").addClass("btn-danger");
            $("#b_answer2").removeClass("btn-success").addClass("btn-danger");
            $("#b_answer3").removeClass("btn-success").addClass("btn-danger");
            $("#b_answer4").removeClass("btn-success").addClass("btn-danger");
            $("#b_answer5").removeClass("btn-success").addClass("btn-danger");
            $("#b_answer" + correctAnswer).removeClass("btn-danger").addClass("btn-success");
            MathJax.Hub.Queue(["Typeset", MathJax.Hub, "MathExample"]);
            $("#detailedData").show();
        });
    

        $(document).on('mouseleave', ".row_dd", function (e) {
            $("#detailedData").hide();
        });

        $(document).mousemove(function (e) {
            $("#detailedData").position({
                my: "left+10 top+10",
                of: e,
                collision: "flipfit"
            });
        });
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
                                        Assign Question
                                    </h1>
                                    
                            <div class="row">
                                <div class="col-lg-8">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Button CssClass="hidden" runat="server" ID="btnUpdatePanels" OnClick="btnUpdatePanels_Click" />
                                            <asp:Label runat="server" Text="Subject"/><br />
                                            <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" 
                                                onchange="$('#btnUpdatePanels').click();"></asp:DropDownList><br />
                                            <asp:Label runat="server" Text="Category"/><br />
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" 
                                                onchange="$('#btnUpdatePanels').click();"></asp:DropDownList><br />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                    
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdQuestions" runat="server" CssClass="table table-striped" AutoGenerateColumns="False"
                                                RowStyle-CssClass="row_dd" HeaderStyle-CssClass="header_dd" AllowPaging="True" 
                                                DataSourceID="ObjectDataSource1" PagerStyle-CssClass="bs-pagination">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Subject/Category">
                                                        <ItemTemplate>
                                                            <%# Eval("subject") + " - " + Eval("category") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="hideIT">
                                                        <HeaderStyle CssClass="hideIT"></HeaderStyle>
                                                        <ItemStyle CssClass="hideIT" />
                                                        <ItemTemplate>
                                                            <div class="dataDiv" data-question="<%# CogniTutor.Common.EscapeHTML(Eval("questionContents.questionText")) %>" 
                                                                data-answer1="<%# CogniTutor.Common.EscapeHTML(Eval("questionContents.Answer1")) %>" 
                                                                data-answer2="<%# CogniTutor.Common.EscapeHTML(Eval("questionContents.Answer2")) %>" 
                                                                data-answer3="<%# CogniTutor.Common.EscapeHTML(Eval("questionContents.Answer3")) %>" 
                                                                data-answer4="<%# CogniTutor.Common.EscapeHTML(Eval("questionContents.Answer4")) %>"
                                                                data-answer5="<%# CogniTutor.Common.EscapeHTML(Eval("questionContents.Answer5")) %>" 
                                                                data-explanation="<%# CogniTutor.Common.EscapeHTML(Eval("questionContents.explanation")) %>"
                                                                data-correctAnswer="<%# CogniTutor.Common.EscapeHTML(Convert.ToInt32(Eval("questionContents.correctAnswer"))+1) %>"
                                                                data-image="<%# CogniTutor.Common.EscapeHTML(Eval("questionContents.ImageUrl")) %>" 
                                                                ></div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="row_dd"></RowStyle>
                                            </asp:GridView>
                                            
                                            <div id="detailedData" style="display: none; padding: 15px;">
                                                <div class="row">
                                                <h3 class="align-center">Question</h3><br />
                                                <div class="col-lg-6 text-center" style="padding-right:20px; border-right: 1px solid #ccc;">
                                                    <img id="img_image" style="max-width:100%; max-height:100%;"/>
                                                    <p id="p_question"></p>
                                                </div>
                                                <div class="col-lg-6" id="divAnswers">
                                                    <a class="btn btn-danger btn-block" id="b_answer1"></a>
                                                    <a class="btn btn-danger btn-block" id="b_answer2"></a>
                                                    <a class="btn btn-danger btn-block" id="b_answer3"></a>
                                                    <a class="btn btn-danger btn-block" id="b_answer4"></a>
                                                    <a class="btn btn-danger btn-block" id="b_answer5"></a>
                                                </div>
                                                </div>
                                                <br />
                                                <div class="row" id="divExplanation">
                                                    <div class="col-lg-2" style="padding-right:20px; border-right: 1px solid #ccc;">
                                                        <p class="align-center">Explanation</p>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <p id="p_explanation"></p>
                                                    </div>
                                                </div>
                                            </div>
            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
                                        SelectMethod="QueryQuestions" TypeName="CogniTutor.Question">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlSubject" Name="subject" PropertyName="SelectedValue" Type="String" />
                                            <asp:ControlParameter ControlID="ddlCategory" Name="category" PropertyName="SelectedValue" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </div>
                            </div>
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

    </form>

</body>

</html>

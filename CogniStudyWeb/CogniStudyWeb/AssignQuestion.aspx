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

    <link rel="shortcut icon" type="image/x-icon" href="Images/CogniTutor5.jpg" />

    <script src="https://code.jquery.com/jquery-2.2.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.11.4/jquery-ui.min.js"></script>

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" integrity="sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7" crossorigin="anonymous">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap-theme.min.css" integrity="sha384-fLW2N01lMqjakBkx3l/M9EahuwpSfeNvV63J5ezn3uZzapT0u7EYsXMjQV+0En5r" crossorigin="anonymous">

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>

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
    
    <!-- Our custom javascript -->
    <script src="js/Custom.js"></script>

    <link href="css/question.css" rel="stylesheet">

    <script type="text/javascript" async
        src="https://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-MML-AM_CHTML">
    </script>

    <link href="css/modal.css" rel="stylesheet" type="text/css" /> 

    <script type="text/javascript" src="js/bs.pagination.js"></script>

    <link href="css/customtooltip.css" rel="stylesheet"/>

    <script>
        //$(document).on('mouseenter', ".row_dd", function (e) {
        //    var self = $(this).find(".dataDiv");
        //    var question = self.attr("data-question");
        //    var answer1 = self.attr("data-answer1");
        //    var answer2 = self.attr("data-answer2");
        //    var answer3 = self.attr("data-answer3");
        //    var answer4 = self.attr("data-answer4");
        //    var answer5 = self.attr("data-answer5");
        //    var explanation = self.attr("data-explanation");
        //    var correctAnswer = self.attr("data-correctAnswer");
        //    var image = self.attr("data-image");
        //    var bundleText = self.attr("data-bundleText");
        //    var bundleImage = self.attr("data-bundleImage");

        //    $("#p_question").html(question);
        //    $("#b_answer1").html(answer1);
        //    $("#b_answer2").html(answer2);
        //    $("#b_answer3").html(answer3);
        //    $("#b_answer4").html(answer4);
        //    $("#b_answer5").html(answer5);
        //    $("#p_explanation").html(explanation);
        //    $("#p_bundletext").html(bundleText);
        //    $("#img_image").attr("src", image);
        //    $("#img_bundleimage").attr("src", bundleImage);
        //    if (answer5 == "") {
        //        $("#b_answer5").hide();
        //    }
        //    else {
        //        $("#b_answer5").show();
        //    }
        //    $("#b_answer1").removeClass("btn-success").addClass("btn-danger");
        //    $("#b_answer2").removeClass("btn-success").addClass("btn-danger");
        //    $("#b_answer3").removeClass("btn-success").addClass("btn-danger");
        //    $("#b_answer4").removeClass("btn-success").addClass("btn-danger");
        //    $("#b_answer5").removeClass("btn-success").addClass("btn-danger");
        //    $("#b_answer" + correctAnswer).removeClass("btn-danger").addClass("btn-success");
        //    MathJax.Hub.Queue(["Typeset", MathJax.Hub, "MathExample"]);
        //    $("#detailedData").show();
        //});
    

        //$(document).on('mouseleave', ".row_dd", function (e) {
        //    $("#detailedData").hide();
        //});

        //$(document).mousemove(function (e) {
        //    $("#detailedData").position({
        //        my: "left+10 top+10",
        //        of: e,
        //        collision: "flipfit"
        //    });
        //});
    </script>
    
<script>
    function showCorrect(outer, explanation) {
        $('#' + outer + ' > a').each(function (i) {
            $(this).removeClass('btn-default');
            if ($(this).attr('correct') == 'True') {
                $(this).addClass('btn-success');
            }
            else {
                $(this).addClass('btn-danger');
            }
        });
        $('#' + explanation).removeClass('hidden');
    }

    function pageLoad() {
        var mpe = $find("MPE");
        mpe.add_shown(onShown);
    }
    function onShown() {
        var background = $find("MPE")._backgroundElement;
        background.onclick = function () { $find("MPE").hide(); }
    }
</script>

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

        <div class="row">
            <div class="col-lg-12">
                <div id="wrapper">

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" class="alert alert-success" ID="pnlSuccess" Visible="false">
                              <strong>Success!</strong> You have successfully assigned the question to your student(s).
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div id="page-wrapper">
                        
                        
                        <div class="container-fluid">
                            
                            <!-- Page Heading -->
                            <div class="row">
                                <div class="col-lg-12">
                                    <h1 class="page-header">
                                        Assign Question
                                    </h1>
                                </div>
                            </div>
                                    
                            <div class="row">
                                <div class="col-lg-8">
                                    <p>Assign specific questions to your students to answer. You can see their responses on their profile.</p>
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
                                                DataSourceID="ObjectDataSource1" PagerStyle-CssClass="bs-pagination" OnRowCommand="grdQuestions_RowCommand"
                                                DataKeyNames="ObjectId" OnRowDataBound="grdQuestions_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Subject/Category">
                                                        <ItemTemplate>
                                                            <%# Eval("subject") + " - " + Eval("category") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-default center-block" CommandName="View" runat="server" Text="View" id="btnView"
                                                                CommandArgument='<%# Eval("ObjectId") %>'
                                                                />
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
                                                                data-correctAnswer="<%# Convert.ToInt32(Eval("questionContents.correctAnswer"))+1 %>"
                                                                data-image="<%# CogniTutor.Common.EscapeHTML(Eval("questionContents.ImageUrl")) %>" 
                                                                data-bundleText="<%# Convert.ToBoolean(Eval("inBundle")) ? CogniTutor.Common.EscapeHTML(Eval("bundle.passageText")) : "" %>" 
                                                                data-bundleImage="<%# Convert.ToBoolean(Eval("inBundle")) ? CogniTutor.Common.EscapeHTML(Eval("bundle.ImageUrl")) : "" %>" 
                                                                ></div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="row_dd"></RowStyle>
                                            </asp:GridView>
                                            
                                            <div id="detailedData" style="display: none; padding: 15px;">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="row">
                                                            <div class="col-lg-10 col-lg-offset-1">
                                                                <br />
                                                                <img id="img_bundleimage" style="max-width:100%; max-height:100%;"/>
                                                                <p id="p_bundletext"></p>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12 text-center">
                                                                <h3>Question</h3>
                                                            </div>
                                                        </div>
                                                        <div class="row">
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
                                            
                                            <asp:Panel ID="pnlSend" runat="server" CssClass="modalPopup row" style = "display:none" ScrollBars="Vertical" Width="1000" Height="500">
                                                <div class="row">
                                                    <div class="col-lg-11 col-lg-offset-1">
                                                        <asp:Panel runat="server" ID="pnlBundle" class="row">
                                                            <div class="col-lg-2"></div>
                                                            <div class="col-lg-8">
                                                                <h3 class="text-center">Passage</h3><br />
                                                                <asp:Image runat="server" ID="Image1" class="center-block" style="max-width:100%; max-height:100%;" />
                                                                <asp:Label runat="server" ID="lbBundleText"></asp:Label>
                                                                <hr />
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel runat="server" id="pnlQuestions"></asp:Panel>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                </div>
                                                <div class="col-lg-6">
                                                    <h3 class="">Send to Students</h3><br />
                                                    <asp:CheckBoxList CssClass="" runat="server" ID="cblMyStudents" DataTextField="DisplayName" DataValueField="ObjectId"></asp:CheckBoxList>
                                                    <br />

                                                    <asp:Button CssClass="btn btn-success" runat="server" ID="btnSend" Text="Send to students" OnClick="btnSend_Click" />
                                                    <asp:Button CssClass="btn btn-default" ID="btnCancel" runat="server" Text="Cancel" OnClientClick = "return Hidepopup()"/>
                                                </div>
                                            </asp:Panel>
                                            <asp:Button id="btnFake" runat="server" CssClass="hidden" />
                                            <ajax:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                                            PopupControlID="pnlSend" TargetControlID = "btnFake"
                                            BackgroundCssClass="modalBackground" BehaviorID="MPE">
                                            </ajax:ModalPopupExtender>
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

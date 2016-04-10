<%@ Page ValidateRequest="false" Async="true" Language="C#" AutoEventWireup="true" CodeBehind="UploadQuestion.aspx.cs" Inherits="CogniTutor.UploadQuestion" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>
<%@ Register TagPrefix="COG" TagName="SubjectCategoryDropdown" Src="~/UserControls/SubjectCategoryDropdown.ascx" %>
<%@ Register TagPrefix="COG" TagName="LatexEditor" Src="~/UserControls/LatexEditor.ascx" %>
<%@ Register TagPrefix="COG" TagName="SwitchEditor" Src="~/UserControls/SwitchEditor.ascx" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Upload Question</title>

    <link rel="shortcut icon" type="image/x-icon" href="Images/CogniTutor5.jpg" />

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/modern-business.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- Question underline -->
    <link href="css/question.css" rel="stylesheet" type="text/css">

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
                    
                    <asp:Panel runat="server" class="alert alert-success" ID="pnlSuccess" Visible="false">
                      <strong>Success!</strong> Your question(s) were successfully uploaded. Your question(s) must be approved before students will be able to view them. Check the approval status on the Question Arena page.
                    </asp:Panel>
                    <asp:Panel runat="server" class="alert alert-danger" ID="pnlError" Visible="false">
                      <asp:Label runat="server" ID="lbError" Text="Your question(s) were successfully uploaded. 
                          Your question(s) must be approved before students will be able to view them. 
                          Check the approval status on the Question Arena page."></asp:Label>
                    </asp:Panel>

                    <div id="page-wrapper">

                        <div class="container-fluid">

                            <!-- Page Heading -->
                            <div class="row">
                                <div class="col-lg-12">
                                    <h1 class="page-header">
                                        Upload Question
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

                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <asp:Label runat="server" Text="Subject"/><br />
                                                    <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList><br />
                                                    <asp:Label runat="server" Text="Category"/><br />
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList><br />
                                                
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:CheckBox runat="server" CssClass="" ID="cbInBundle" onclick="$('#divPassage').toggleClass('hidden'); $('#divPassageExtraQuestions').toggleClass('hidden');" Text="Make 3-question bundle"/><br /><br />


                                    <div id="divPassage" class='<%= cbInBundle.Checked ? "" : "hidden" %>'>
                                        Image (Optional):<br /><asp:FileUpload ID="FileUpload0" runat="server" /><br />
                                        <COG:SwitchEditor runat="server" OriginalText="Enter passage text here..." ID="tbPassage" />
                                    </div>
                                    <hr />
                                    
                                    

                                    
                                    <div class="row">
                                        <h3 class="align-center">Question</h3><br />
                                        <div class="col-lg-6" style="padding-right:20px; border-right: 1px solid #ccc;">
                                            Image (Optional):<br /><asp:FileUpload ID="FileUpload1" runat="server" /><br />
                                            <COG:SwitchEditor runat="server" OriginalText="Enter question text here..." ID="tbQuestion" />
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="row">
                                                <div class="col-lg-9">
                                                    <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tbAnswer1" />
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:RadioButton runat="server" ID="rbAnswer1" GroupName="Answers" Text="Correct"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-9">
                                                    <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tbAnswer2" />
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:RadioButton runat="server" ID="rbAnswer2" GroupName="Answers" Text="Correct"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-9">
                                                    <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tbAnswer3" />
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:RadioButton runat="server" ID="rbAnswer3" GroupName="Answers" Text="Correct"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-9">
                                                    <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tbAnswer4" />
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:RadioButton runat="server" ID="rbAnswer4" GroupName="Answers" Text="Correct"/>
                                                </div>
                                            </div>
                                            <div id="divShowAnswer5" class='row <%= cbAnswer5.Checked ? "" : "hidden" %>'>
                                                <div class="col-lg-9">
                                                    <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tbAnswer5" />
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:RadioButton runat="server" ID="rbAnswer5" GroupName="Answers" Text="Correct"/>
                                                </div>
                                            </div>
                                            <asp:CheckBox id="cbAnswer5" runat="server" Text="Use 5 answers" onclick="$('#divShowAnswer5').toggleClass('hidden');"/><br /><br />

                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" id="divAllExplanation">
                                        <div class="col-lg-2" style="padding-right:20px; border-right: 1px solid #ccc;">
                                            <p class="align-center">Explanation</p>
                                        </div>
                                        <div class="col-lg-10">
                                            <COG:SwitchEditor runat="server" OriginalText="Enter explanation text here..." ID="tbExplanation" />
                                        </div>
                                    </div>
                                    <br /><hr />

                                    <div id="divPassageExtraQuestions" class='<%= cbInBundle.Checked ? "" : "hidden" %>'>
                                        <hr />
                                        <div class="row">
                                            <h3 class="align-center">Question</h3><br />
                                            <div class="col-lg-6" style="padding-right:20px; border-right: 1px solid #ccc;">
                                                Image (Optional):<br /><asp:FileUpload ID="FileUpload2" runat="server" /><br />
                                                <COG:SwitchEditor runat="server" OriginalText="Enter question text here..." ID="tbQuestion2" />
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="row">
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb2Answer1" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb2Answer1" GroupName="Answers2" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb2Answer2" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb2Answer2" GroupName="Answers2" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb2Answer3" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb2Answer3" GroupName="Answers2" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb2Answer4" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb2Answer4" GroupName="Answers2" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <div id="div2ShowAnswer5" class='row <%= cb2Answer5.Checked ? "" : "hidden" %>'>
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb2Answer5" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb2Answer5" GroupName="Answers2" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <asp:CheckBox id="cb2Answer5" runat="server" Text="Use 5 answers" onclick="$('#div2ShowAnswer5').toggleClass('hidden');"/><br /><br />

                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" id="div2AllExplanation">
                                            <div class="col-lg-2" style="padding-right:20px; border-right: 1px solid #ccc;">
                                                <p class="align-center">Explanation</p>
                                            </div>
                                            <div class="col-lg-10">
                                                <COG:SwitchEditor runat="server" OriginalText="Enter explanation text here..." ID="tbExplanation2" />
                                            </div>
                                        </div>
                                        <br /><hr />

                                        <div class="row">
                                            <h3 class="align-center">Question</h3><br />
                                            <div class="col-lg-6" style="padding-right:20px; border-right: 1px solid #ccc;">
                                                Image (Optional):<br /><asp:FileUpload ID="FileUpload3" runat="server" /><br />
                                                <COG:SwitchEditor runat="server" OriginalText="Enter question text here..." ID="tbQuestion3" />
                                            </div>
                                            
                                            <div class="col-lg-6">
                                                <div class="row">
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb3Answer1" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb3Answer1" GroupName="Answers3" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb3Answer2" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb3Answer2" GroupName="Answers3" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb3Answer3" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb3Answer3" GroupName="Answers3" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb3Answer4" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb3Answer4" GroupName="Answers3" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <div id="div3ShowAnswer5" class='row <%= cb3Answer5.Checked ? "" : "hidden" %>'>
                                                    <div class="col-lg-9">
                                                        <COG:SwitchEditor runat="server" OriginalText="Enter answer text here..." ID="tb3Answer5" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:RadioButton runat="server" ID="rb3Answer5" GroupName="Answers3" Text="Correct"/>
                                                    </div>
                                                </div>
                                                <asp:CheckBox id="cb3Answer5" runat="server" Text="Use 5 answers" onclick="$('#div3ShowAnswer5').toggleClass('hidden');"/><br /><br />

                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" id="div3AllExplanation">
                                            <div class="col-lg-2" style="padding-right:20px; border-right: 1px solid #ccc;">
                                                <p class="align-center">Explanation</p>
                                            </div>
                                            <div class="col-lg-10">
                                                <COG:SwitchEditor runat="server" OriginalText="Enter explanation text here..." ID="tbExplanation3" />
                                            </div>
                                        </div>
                                        <br /><hr />

                                    </div>

                                </div>
                            </div>
                            <!-- /.row -->



                        </div>
                        <!-- /.container-fluid -->

                    </div>
                    <!-- /#page-wrapper -->

                </div>
                <!-- /#wrapper -->
                <asp:Button CssClass="btn btn-success center-block" runat="server" ID="btnSubmitQuestion" OnClick="btnSubmitQuestion_Click" Text="Submit"/><br /><br />


            </div>
        </div>
        <hr>
    </div>
            
    <!-- Footer -->
    <COG:Footer runat="server" />
    </form>

</body>

</html>

<%@ Page ValidateRequest="false" Async="true" Language="C#" AutoEventWireup="true" CodeBehind="UploadQuestion.aspx.cs" Inherits="CogniTutor.UploadQuestion" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>
<%@ Register TagPrefix="COG" TagName="SubjectCategoryDropdown" Src="~/UserControls/SubjectCategoryDropdown.ascx" %>
<%@ Register TagPrefix="COG" TagName="LatexEditor" Src="~/UserControls/LatexEditor.ascx" %>

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
        function fileChosen() {
            if (this.value !== '') {
                $('#btnUpload').click();
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
                            
                            <div class="row">
                                <div class="col-lg-12"><!--aaaaaaaaaaaaaaaa\(\frac{a}{b}+2 = a\)bababababa-->

                                    <asp:Label runat="server" Text="Subject"/><br />
                                    <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" 
                                        OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList><br /><br />
                                    <asp:Label runat="server" Text="Category"/><br />
                                    <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList><br /><br />
                                    
                                    <asp:CheckBox runat="server" ID="cbInBundle" onclick="$('#divPassage').toggleClass('hidden'); $('#divPassageExtraQuestions').toggleClass('hidden');" Text="Make 3-question bundle"/><br /><br />

                                    <asp:CheckBox runat="server" ID="cbImage" onclick="$('#divImage').toggleClass('hidden');" Text="Include Image"/><br /><br />
                                    <div id="divImage" class="hidden">
                                        <asp:FileUpload ID="FileUpload1" runat="server" onchange="fileChosen()" />
                                        <asp:Button class="hidden" ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" /><br />
                                    </div>
                                    <div id="divPassage" class="hidden">
                                        Passage:<br /><COG:LatexEditor runat="server" ID="tbPassage" Width="700" Height="100" TextMode="MultiLine"></COG:LatexEditor><br /><br />
                                    </div>

                                    Question:<br /><COG:LatexEditor runat="server" id="tbQuestion" Width="700" Height="100"></COG:LatexEditor><br /><br />
                                    
                                    
                                    Answer1:<br /><COG:LatexEditor runat="server" ID="tbAnswer1" Width="500"></COG:LatexEditor>
                                    <asp:RadioButton runat="server" ID="rbAnswer1" GroupName="Answers" Text="Select as correct"/><br /><br />
                                    Answer2:<br /><COG:LatexEditor runat="server" ID="tbAnswer2" Width="500"></COG:LatexEditor>
                                    <asp:RadioButton runat="server" ID="rbAnswer2" GroupName="Answers" Text="Select as correct"/><br /><br />
                                    Answer3:<br /><COG:LatexEditor runat="server" ID="tbAnswer3" Width="500"></COG:LatexEditor>
                                    <asp:RadioButton runat="server" ID="rbAnswer3" GroupName="Answers" Text="Select as correct"/><br /><br />
                                    Answer4:<br /><COG:LatexEditor runat="server" ID="tbAnswer4" Width="500"></COG:LatexEditor>
                                    <asp:RadioButton runat="server" ID="rbAnswer4" GroupName="Answers" Text="Select as correct"/><br /><br />
                                    <asp:CheckBox id="cbAnswer5" runat="server" Text="Use 5 answers" onclick="$('#divAnswer5').toggleClass('hidden');"/><br /><br />
                                    <div id="divAnswer5" class="hidden">
                                        Answer5:<br /><COG:LatexEditor runat="server" ID="tbAnswer5" Width="500"></COG:LatexEditor>
                                        <asp:RadioButton runat="server" ID="rbAnswer5" GroupName="Answers" Text="Select as correct"/><br /><br />
                                    </div>
                                    
                                    Explanation:<br /><COG:LatexEditor runat="server" ID="tbExplanation" Width="700" Height="100" TextMode="MultiLine"></COG:LatexEditor><br /><br />

                                    <div id="divPassageExtraQuestions" class="hidden">
                                        Question:<br /><COG:LatexEditor runat="server" ID="tbQuestion2" Width="700" Height="100" TextMode="MultiLine"></COG:LatexEditor><br /><br />
                                    
                                        Answer1:<br /><COG:LatexEditor runat="server" ID="tb2Answer1" Width="500"></COG:LatexEditor>
                                        <asp:RadioButton runat="server" ID="rb2Answer1" GroupName="Answers2" Text="Select as correct"/><br /><br />
                                        Answer2:<br /><COG:LatexEditor runat="server" ID="tb2Answer2" Width="500"></COG:LatexEditor>
                                        <asp:RadioButton runat="server" ID="rb2Answer2" GroupName="Answers2" Text="Select as correct"/><br /><br />
                                        Answer3:<br /><COG:LatexEditor runat="server" ID="tb2Answer3" Width="500"></COG:LatexEditor>
                                        <asp:RadioButton runat="server" ID="rb2Answer3" GroupName="Answers2" Text="Select as correct"/><br /><br />
                                        Answer4:<br /><COG:LatexEditor runat="server" ID="tb2Answer4" Width="500"></COG:LatexEditor>
                                        <asp:RadioButton runat="server" ID="rb2Answer4" GroupName="Answers2" Text="Select as correct"/><br /><br />
                                        <asp:CheckBox id="cb2Answer5" runat="server" Text="Use 5 answers" onclick="$('#div2Answer5').toggleClass('hidden');"/><br /><br />
                                        <div id="div2Answer5" class="hidden">
                                            Answer5:<br /><COG:LatexEditor runat="server" ID="tb2Answer5" Width="500"></COG:LatexEditor>
                                            <asp:RadioButton runat="server" ID="rb2Answer5" GroupName="Answers2" Text="Select as correct"/><br /><br />
                                        </div>
                                    
                                        Explanation:<br /><COG:LatexEditor runat="server" ID="tbExplanation2" Width="700" Height="100" TextMode="MultiLine"></COG:LatexEditor><br /><br />
                                        
                                        Question:<br /><COG:LatexEditor runat="server" ID="tbQuestion3" Width="700" Height="100" TextMode="MultiLine"></COG:LatexEditor><br /><br />
                                    
                                        Answer1:<br /><COG:LatexEditor runat="server" ID="tb3Answer1" Width="500"></COG:LatexEditor>
                                        <asp:RadioButton runat="server" ID="rb3Answer1" GroupName="Answers3" Text="Select as correct"/><br /><br />
                                        Answer2:<br /><COG:LatexEditor runat="server" ID="tb3Answer2" Width="500"></COG:LatexEditor>
                                        <asp:RadioButton runat="server" ID="rb3Answer2" GroupName="Answers3" Text="Select as correct"/><br /><br />
                                        Answer3:<br /><COG:LatexEditor runat="server" ID="tb3Answer3" Width="500"></COG:LatexEditor>
                                        <asp:RadioButton runat="server" ID="rb3Answer3" GroupName="Answers3" Text="Select as correct"/><br /><br />
                                        Answer4:<br /><COG:LatexEditor runat="server" ID="tb3Answer4" Width="500"></COG:LatexEditor>
                                        <asp:RadioButton runat="server" ID="rb3Answer4" GroupName="Answers3" Text="Select as correct"/><br /><br />
                                        <asp:CheckBox id="cb3Answer5" runat="server" Text="Use 5 answers" onclick="$('#div3Answer5').toggleClass('hidden');"/><br /><br />
                                        <div id="div3Answer5" class="hidden">
                                            Answer5:<br /><COG:LatexEditor runat="server" ID="tb3Answer5" Width="500"></COG:LatexEditor>
                                            <asp:RadioButton runat="server" ID="rb3Answer5" GroupName="Answers2" Text="Select as correct"/><br /><br />
                                        </div>
                                    
                                        Explanation:<br /><COG:LatexEditor runat="server" ID="tbExplanation3" Width="700" Height="100" TextMode="MultiLine"></COG:LatexEditor><br /><br />
                                    </div>

                                    <asp:Button runat="server" ID="btnSubmitQuestion" OnClick="btnSubmitQuestion_Click" Text="Submit"/><br /><br />
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

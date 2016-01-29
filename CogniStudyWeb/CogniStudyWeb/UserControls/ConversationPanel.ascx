<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConversationPanel.ascx.cs" Inherits="CogniTutor.UserControls.ConversationPanel" %>

<asp:Button ID="btnChangeConversation" runat="server" OnClick="btnChangeConversation_Click" />

<div class="row">
    <a class="btn-block btn-default" id="btnConversation" onclick="document.getElementById('btnChangeConversation').click()" href="#">
        <div class="col-sm-12 col-xs-12">
            <asp:Label ID="lbTheirName" runat="server"></asp:Label>
            <asp:Label runat="server" ID="lbLastMessage" class="medium-text"></asp:Label>
            <asp:Label ID="lbTheirUserID" class="hidden" runat="server"></asp:Label>
        </div>
    </a>
</div>
<hr />


                                                    <!--<div class="col-sm-2 col-xs-4">
                                                        <asp:Image ID="Image1" class="img-rounded img-responsive" runat="server"
                                                            ImageUrl='<%# Eval("PictureFileName").ToString() == "" ?  
                                                                "Images/default_prof_pic.png" :"Images/profile pictures/" + Eval("PictureFileName") + ".png" %>' />
                                                    </div>-->
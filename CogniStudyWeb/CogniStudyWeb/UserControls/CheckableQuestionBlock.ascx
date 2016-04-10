<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckableQuestionBlock.ascx.cs" Inherits="CogniTutor.UserControls.CheckableQuestionBlock" %>

<div class="row">
    <h3 class="align-center">Question</h3><br />
    <div class="col-lg-6 text-center" style="padding-right:20px; border-right: 1px solid #ccc;">
        <asp:Image runat="server" ID="Image1" style="max-width:100%; max-height:100%;" />
        <asp:Label runat="server" ID="lbQuestion"></asp:Label>
    </div>
    <div class="col-lg-6" id="divAnswers">
        <asp:Panel runat="server" ID="pnlCheckable">
            <asp:RadioButtonList runat="server" ID="rblAnswers">
                <asp:ListItem id="item1"></asp:ListItem>
                <asp:ListItem id="item2"></asp:ListItem>
                <asp:ListItem id="item3"></asp:ListItem>
                <asp:ListItem id="item4"></asp:ListItem>
                <asp:ListItem id="item5" class="hidden"></asp:ListItem>
            </asp:RadioButtonList>
        </asp:Panel>
    </div>
</div>
<br /><hr />
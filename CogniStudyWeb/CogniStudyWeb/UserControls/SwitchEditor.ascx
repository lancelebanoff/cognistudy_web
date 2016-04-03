<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SwitchEditor.ascx.cs" Inherits="CogniTutor.UserControls.SwitchEditor" %>
<%@ Register TagPrefix="COG" TagName="LatexEditor" Src="~/UserControls/LatexEditor.ascx" %>

<asp:Hyperlink runat="server" id="btnQuestion" class="btn btn-default btn-block" replace="<%# divQuestion.ClientID %>" justHidden="false">
    <p><%= OriginalText %></p>
</asp:Hyperlink>
<asp:Panel runat="server" class="hidden" id="divQuestion">
    <COG:LatexEditor runat="server" id="tbQuestion"></COG:LatexEditor>
</asp:Panel>

<script>
    $(document).ready(function () {
        var btn = $('#<%= btnQuestion.ClientID %>');
        var div = $('#<%= divQuestion.ClientID %>');

        div.addClass('hidden');
        btn.removeClass('hidden');
        var myEditor;
        for (var i in CKEDITOR.instances) {
            if ($('#' + i).parent().attr('id') == div.attr('id')) {
                myEditor = CKEDITOR.instances[i];
            }
        }
        var text = myEditor.getData();
        if (text == "") {
            text = '<p><%= OriginalText %></p>';
        }
        btn.html(text);

        div.on('click', '*', function (e) {
            e.stopPropagation();
        });
        btn.on('click', function (e) {
            btn.addClass('hidden');
            div.removeClass('hidden');
            btn.attr('justHidden', true);
        });
        $('#wrapper').on('click', '*', function (e) {
            if (btn.attr('justHidden') != 'true') {
                div.addClass('hidden');
                btn.removeClass('hidden');
                var myEditor;
                for (var i in CKEDITOR.instances) {
                    if ($('#' + i).parent().attr('id') == div.attr('id')) {
                        myEditor = CKEDITOR.instances[i];
                    }
                }
                var text = myEditor.getData();
                if (text == "") {
                    text = '<p><%= OriginalText %></p>';
                }
                btn.html(text);
            }
            btn.attr('justHidden', false);
            e.stopPropagation();
        });
    });
</script>
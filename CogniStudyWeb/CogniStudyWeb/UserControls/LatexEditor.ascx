<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatexEditor.ascx.cs" Inherits="CogniTutor.UserControls.LatexEditor" %>

<script src="/js/ckeditor/ckeditor.js"></script>

<asp:TextBox runat="server" ID="tb" TextMode="MultiLine"></asp:TextBox><br /><br />
                                    
<script>
    CKEDITOR.config.contentsCss = '/../css/ckeditor_question.css';
    editor = CKEDITOR.replace('<%= AcutalID %>', {
        // Load the timestamp plugin.
        extraPlugins: 'numunderline,eqneditor,timestamp',
        // Rearrange toolbar groups and remove unnecessary plugins.
        toolbarGroups: [
            { name: 'clipboard', groups: ['clipboard', 'undo'] },
            { name: 'document', groups: ['mode'] },
            //'/',
            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
            { name: 'numunderline' },
            { name: 'eqneditor' },
            //{ name: 'timestamp' },
            { name: 'about' }
        ],
        removePlugins: 'font,iframe,pagebreak,flash,stylescombo,print,preview,save,smiley,pastetext,pastefromword',
        removeButtons: 'Anchor,Font,Strike,Subscript,Superscript',
        allowedContent: 'span(ques1,ques2,ques3); img[*]',
        height: '<%= Height.ToString() %>',
        width: '<%= Width.ToString() %>'

    });
</script>
CKEDITOR.plugins.add('numunderline3', {
    icons: 'numunderline3',
    init: function (editor) {
        editor.addCommand('wrapUnderline3', {
            exec: function (editor) {
                var selected_text = editor.getSelection().getSelectedText(); // Get Text
                //var newElement = '{UNDERLINE}' + selected_text + '{UNDERLINE NUM=1}';
                //var newElement = '<span>' + selected_text + '</span>';
                //editor.insertHtml(newElement);
                var newElement = new CKEDITOR.dom.element("span");              // Make Paragraff
                newElement.setAttributes({ class: 'ques ques3' })                 // Set Attributes
                newElement.setText(selected_text);                           // Set text to element
                editor.insertElement(newElement);
            }
        });
        editor.ui.addButton('Numunderline3', {
            label: 'Underline with Number 3',
            command: 'wrapUnderline3',
            toolbar: 'numunderline'
        });
    }
});
CKEDITOR.plugins.add('numunderline1', {
    icons: 'numunderline1',
    init: function (editor) {
        editor.addCommand('wrapUnderline1', {
            exec: function (editor) {
                var selected_text = editor.getSelection().getSelectedText(); // Get Text
                //var newElement = '{UNDERLINE}' + selected_text + '{UNDERLINE NUM=1}';
                //var newElement = '<span>' + selected_text + '</span>';
                //editor.insertHtml(newElement);
                var newElement = new CKEDITOR.dom.element("span");              // Make Paragraff
                newElement.setAttributes({ class: 'ques ques1' })                 // Set Attributes
                newElement.setText(selected_text);                           // Set text to element
                editor.insertElement(newElement);
            }
        });
        editor.ui.addButton('Numunderline1', {
            label: 'Underline with Number 1',
            command: 'wrapUnderline1',
            toolbar: 'numunderline'
        });
    }
});
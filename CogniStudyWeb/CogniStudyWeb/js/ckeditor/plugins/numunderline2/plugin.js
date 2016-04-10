CKEDITOR.plugins.add('numunderline2', {
    icons: 'numunderline2',
    init: function (editor) {
        editor.addCommand('wrapUnderline2', {
            exec: function (editor) {
                var selected_text = editor.getSelection().getSelectedText(); // Get Text
                //var newElement = '{UNDERLINE}' + selected_text + '{UNDERLINE NUM=1}';
                //var newElement = '<span>' + selected_text + '</span>';
                //editor.insertHtml(newElement);
                var newElement = new CKEDITOR.dom.element("span");              // Make Paragraff
                newElement.setAttributes({ class: 'ques ques2' })                 // Set Attributes
                newElement.setText(selected_text);                           // Set text to element
                editor.insertElement(newElement);
            }
        });
        editor.ui.addButton('Numunderline2', {
            label: 'Underline with Number 2',
            command: 'wrapUnderline2',
            toolbar: 'numunderline'
        });
    }
});
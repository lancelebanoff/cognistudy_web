CKEDITOR.plugins.add('timestamp', {
    icons: 'timestamp',
    init: function (editor) {
        editor.addCommand('insertTimestamp', {
            exec: function (editor) {
                var selected_text = editor.getSelection().getSelectedText(); // Get Text
                var newElement = '**Underline**' + selected_text + '**/Underline**';
                editor.insertHtml(newElement);
            }
        });
        editor.ui.addButton('Timestamp', {
            label: 'Insert Timestamp',
            command: 'insertTimestamp',
            toolbar: 'timestamp'
        });
    }
});
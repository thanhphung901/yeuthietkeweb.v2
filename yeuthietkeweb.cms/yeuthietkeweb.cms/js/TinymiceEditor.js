tinymce.init({
    selector: "textarea",
    plugins: [
        "advlist autolink lists link image charmap print preview hr anchor pagebreak",
        "searchreplace wordcount visualblocks visualchars code fullscreen",
        "insertdatetime media nonbreaking save table contextmenu directionality",
        "emoticons template paste textcolor colorpicker textpattern media youtube"
            ],
    image_advtab: true,
    style_formats: [
                        {
                            title: "Headers",
                            items: [
                                { title: "Header 1", format: "h1" },
                                { title: "Header 2", format: "h2" },
                                { title: "Header 3", format: "h3" },
                                { title: "Header 4", format: "h4" },
                                { title: "Header 5", format: "h5" },
                                { title: "Header 6", format: "h6" }
                            ]
                        },
                        {
                            title: "Inline", items: [{ title: "Bold", icon: "bold", format: "bold" }, { title: "Italic", icon: "italic", format: "italic" },
            { title: "_Underline", icon: "underline", format: "underline" }, { title: "Strikethrough", icon: "strikethrough", format: "strikethrough" }, { title: "Superscript", icon: "superscript", format: "superscript" }, { title: "Subscript", icon: "subscript", format: "subscript" }, { title: "Code", icon: "code", format: "code"}]
                        },
            { title: "_Blocks", items: [{ title: "Paragraph", format: "p" }, { title: "Blockquote", format: "blockquote" }, { title: "Div", format: "div" }, { title: "Pre", format: "pre"}] },
            { title: "_Alignment", items: [{ title: "Left", icon: "alignleft", format: "alignleft" }, { title: "Center", icon: "aligncenter", format: "aligncenter" }, { title: "Right", icon: "alignright", format: "alignright" }, { title: "Justify", icon: "alignjustify", format: "alignjustify"}] },
            {
                title: "Font Family",
                items: [
                    { title: 'Arial', inline: 'span', styles: { 'font-family': 'arial'} },
                    { title: 'Book Antiqua', inline: 'span', styles: { 'font-family': 'book antiqua'} },
                    { title: 'Comic Sans MS', inline: 'span', styles: { 'font-family': 'comic sans ms,sans-serif'} },
                    { title: 'Courier New', inline: 'span', styles: { 'font-family': 'courier new,courier'} },
                    { title: 'Georgia', inline: 'span', styles: { 'font-family': 'georgia,palatino'} },
                    { title: 'Helvetica', inline: 'span', styles: { 'font-family': 'helvetica'} },
                    { title: 'Impact', inline: 'span', styles: { 'font-family': 'impact,chicago'} },
                    { title: 'Open Sans', inline: 'span', styles: { 'font-family': 'Open Sans'} },
                    { title: 'Symbol', inline: 'span', styles: { 'font-family': 'symbol'} },
                    { title: 'Tahoma', inline: 'span', styles: { 'font-family': 'tahoma'} },
                    { title: 'Terminal', inline: 'span', styles: { 'font-family': 'terminal,monaco'} },
                    { title: 'Times New Roman', inline: 'span', styles: { 'font-family': 'times new roman,times'} },
                    { title: 'Verdana', inline: 'span', styles: { 'font-family': 'Verdana'} }
                ]
            },
    { title: "Font Size", items: [
                                { title: '8pt', inline: 'span', styles: { fontSize: '12px', 'font-size': '8px'} },
                                { title: '10pt', inline: 'span', styles: { fontSize: '12px', 'font-size': '10px'} },
                                { title: '12pt', inline: 'span', styles: { fontSize: '12px', 'font-size': '12px'} },
                                { title: '14pt', inline: 'span', styles: { fontSize: '12px', 'font-size': '14px'} },
                                { title: '16pt', inline: 'span', styles: { fontSize: '12px', 'font-size': '16px'} }
]
    }],
    file_browser_callback: function (field_name, url, type, win) {
        fileBrowserURL = "../Manager/Filemanager/Default.aspx?sessionid=1&field_name=" + field_name + "&url=" + url + "&win=" + win;


        tinyMCE.activeEditor.windowManager.open({
            title: "File Manager NTT",
            url: fileBrowserURL,
            width: 950,
            height: 650,
            inline: 0,
            maximizable: 1,
            close_previous: 0
        }, {
            window: win,
            input: field_name,
            sessionid: '1'
        }
        );
    },
    toolbar: "insertfile undo redo | styleselect | bold italic forecolor backcolor emoticons| alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image media preview | youtube"
});
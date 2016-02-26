tinymce.PluginManager.add('youtube', function (editor, url) {
    // Add a button that opens a window
    editor.addButton('youtube', {
        text: 'Youtube',
        icon: false,
        onclick: function () {
            editor.windowManager.open({

                title: 'Select YouTube Video',
                width: 900,
                height: 655,
                url: url + '/youtube.aspx'
            })
        }
    });


    // Adds a menu item to the tools menu
    editor.addMenuItem('youtube', {
        text: 'YouTube plugin',
        context: 'tools',
        onclick: function () {
            // Open window with a specific url
            editor.windowManager.open({
                title: 'YouTube plugin',
                url: '/youtube.aspx',
                width: 800,
                height: 600,
                buttons: [{
                    text: 'Close',
                    onclick: 'close'
                }]
            });
        }
    });
});
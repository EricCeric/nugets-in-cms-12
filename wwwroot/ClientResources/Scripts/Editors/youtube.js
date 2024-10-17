tinymce.PluginManager.add('youtube', function (editor, url) {
    var insertYouTubeEmbed = function (videoId) {
        // Construct the YouTube embed code
        var embedCode = '<div class="video__container">' +
            '<iframe src="https://www.youtube.com/embed/' + videoId + '" ' +
            'frameborder="0" allowfullscreen></iframe>' +
            '</div>';

        editor.insertContent(embedCode);
    };

    editor.ui.registry.addIcon('youtubeIcon', '<svg xmlns="http://www.w3.org/2000/svg" width="1.43em" height="1em" viewBox="0 0 256 180"><path fill="#f00" d="M250.346 28.075A32.18 32.18 0 0 0 227.69 5.418C207.824 0 127.87 0 127.87 0S47.912.164 28.046 5.582A32.18 32.18 0 0 0 5.39 28.24c-6.009 35.298-8.34 89.084.165 122.97a32.18 32.18 0 0 0 22.656 22.657c19.866 5.418 99.822 5.418 99.822 5.418s79.955 0 99.82-5.418a32.18 32.18 0 0 0 22.657-22.657c6.338-35.348 8.291-89.1-.164-123.134"/><path fill="#fff" d="m102.421 128.06l66.328-38.418l-66.328-38.418z"/></svg>');

    editor.ui.registry.addButton('youtube', {
        icon: 'youtubeIcon',
        tooltip: "Embed YouTube Video",
        onAction: function () {
            editor.windowManager.open({
                title: 'Embed YouTube Video',
                body: {
                    type: 'panel',
                    items: [
                        { type: 'input', name: 'videoId', label: 'YouTube Video ID' }
                    ]
                },
                buttons: [
                    {
                        type: 'submit',
                        text: 'Insert',
                        primary: true
                    },
                    {
                        type: 'cancel',
                        text: 'Cancel'
                    }
                ],
                onSubmit: function (api) {
                    var data = api.getData();
                    insertYouTubeEmbed(data.videoId);
                    api.close();
                }
            });
        }
    });

    return {
        getMetadata: function () {
            return {
                name: 'YouTube Embed Plugin',
                url: 'https://www.youtube.com/'
            };
        }
    };
});

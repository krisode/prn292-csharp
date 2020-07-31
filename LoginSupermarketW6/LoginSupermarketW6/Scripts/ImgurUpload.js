$('document').ready(function () {
    $('input[type=file]').on('change', function () {

        var $files = $(this).get(0).files;
        if ($files.length) {
            // Reject big files
            if ($files[0].size > $(this).data('max-size') * 1024) {
                console.log('Please select a smaller file');
                return false;
            }
            // Begin file upload
            console.log('Uploading file to Imgur..');
            try {
                var apiUrl = 'https://api.imgur.com/3/image';
                var apiKey = '52e1b37e0da96e9';
                var settings = {
                    async: false,
                    crossDomain: true,
                    processData: false,
                    contentType: false,
                    type: 'POST',
                    url: apiUrl,
                    headers: {
                        Authorization: 'Client-ID ' + apiKey,
                        Accept: 'application/json',
                    },
                    mimeType: 'multipart/form-data',
                };
                var formData = new FormData();
                formData.append('image', $files[0]);
                settings.data = formData;
                // Response contains stringified JSON
                // Image URL available at response.data.link
                $.ajax(settings).done(function (response) {
                    var x = JSON.parse(response);
                    console.log(x.status);
                    document.getElementById("Image").value = x.data.link;
                    document.getElementById("showImg").src = document.getElementById("Image").value;
                });
            } catch {
                console.getElementById("img22").value = '';
            }
        }
    });
});
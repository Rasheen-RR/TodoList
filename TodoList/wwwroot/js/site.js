// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    let update_status = () => {
        if (navigator.onLine) {
            $('.offline').css({ "pointer-events": "all" });
            $('.check-box').attr('disabled', false);
        } else {
            $('.offline').css({ "pointer-events": "none" });
            $('.check-box').attr('disabled', true);
        }
    }
    window.addEventListener('online', update_status);
    window.addEventListener('offline', update_status);


function copyToClipboard(text) {
    if ('clipboard' in navigator) {
        let clipboardAccess = false;
        navigator.permissions.query({ name: 'clipboard-write' }).then((perms) => {
            clipboardAccess = perms.state;
            if (clipboardAccess === 'granted') {
                navigator.clipboard.writeText(text)
                console.log(text)
            } else if (clipboardAccess === 'prompt') {

            } else {
                return
            }
        })
    } else {

    }
}

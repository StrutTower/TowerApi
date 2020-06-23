$(document).ready(function () {

    $('.theme-switch').on('click', function (e) {
        e.preventDefault();
        //TODO can add ajax call to save selected theme here
        if ($('#light-theme-link').prop('disabled')) {
            $('#light-theme-link').prop('disabled', false);
            $('#dark-theme-link').prop('disabled', true);
        } else {
            $('#light-theme-link').prop('disabled', true);
            $('#dark-theme-link').prop('disabled', false);
        }
    });
});
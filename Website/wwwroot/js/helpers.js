$(document).ready(function () {
    // Focus an input with autofocus when opening a modal
    $('.modal').on('shown.bs.modal', function () {
        $(this).find('[autofocus]').focus();
    });

    // Activate Alex's disable forms on submit plugin
    $('form[method="post"]').disableOnSubmit({
        buttonTemplate: '<span class="mdi mdi-loading mdi-spin-fast"></span>'
    });


    $('#tag-filter').on('change', function (e) {
        var value = $(this).val();
        if (value === "All") {
            $('tr').show();
        } else {
            $('tr').not('[data-tag-ids*=-' + value + '-]').hide();
            $('tr[data-tag-ids*=-' + value + '-]').show();
        }
    });
});
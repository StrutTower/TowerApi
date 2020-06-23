$(document).ready(function () {
    $('.select2-basic').each(function () {
        var placeholder = "";
        if (typeof $(this).data('placeholder') !== undefined) {
            placeholder = $(this).data('placeholder');
        }

        var allowClear = false;
        if (typeof $(this).data('allow-clear') !== undefined) {
            allowClear = $(this).data('allow-clear');
        }

        var closeOnSelect = true;
        if (typeof $(this).data('close-on-select') !== undefined) {
            allowClear = $(this).data('close-on-select');
        }

        $(this).select2({
            width: '100%',
            placeholder: placeholder,
            allowClear: allowClear,
            closeOnSelect: closeOnSelect
        });
    });

    // Select2 AD Person Searcher
    $('.select2-ad-person-searcher').each(function () {
        var element = $(this);

        var allowClear = false;
        if (typeof element.data('allow-clear') !== undefined) {
            allowClear = element.data('allow-clear');
        }

        element.select2({
            width: '100%',
            allowClear: allowClear,
            placeholder: 'Search for an employee. (Lastname, Firstname)',
            minimumInputLength: 2,
            templateResult: formatADPersonResult,
            ajax: {
                url: element.data('url'),
                dataType: 'JSON',
                delay: 500,
                data: function (params) {
                    var query = {
                        q: params.term,
                        page: params.page || 1
                    };
                    return query;
                }
            }
        });
    });

    function formatADPersonResult(person) {
        if (!person.id) {
            return person.text;
        }
        var html = '<div class="select2-person-container">';
        if (person.photo.length > 0) {
            html += '<div class="select2-person-picture"><img src="data:image/png;base64,' + person.photo + '" alt="No Photo" /></div>';
        } else {
            html += '<div class="select2-no-picture"></div>';
        }
        html += '<div class="d-inline-block pl-2 align-middle">' +
            '<div>' + person.text + '</div>' +
            '<div class="text-faded pl-2">' + person.title + '</div>' +
            '</div>' +
            '</div>';

        var $person = $(html);
        return $person;
    }
});
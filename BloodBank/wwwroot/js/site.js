// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function contains(first, second) {
    if (first.indexOf(second) != -1) {
        return true;
    }
    else {
        return false;
    }
}

function retrieve() {
    var searchedValue = $('.search-bar').val().toLowerCase();
    var selectedValue = $('.selection-bar').find(':selected').text();

    $('.table-to-search tr').each(function () {
        if (contains($(this).children('td:first').text().toLowerCase(), searchedValue)
            && selectedValue == "All" || $(this).find('td:eq(1)').text().trim() == selectedValue) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
}

$('.search-bar').keyup(function () {
    retrieve();
});

$('.selection-bar').change(function () {
    retrieve();
});
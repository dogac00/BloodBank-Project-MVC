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

$('.search-bar').keyup(function () {
    var textToSearch = $('.search-bar').val().toLowerCase();
    $('.table-to-search tr').each(function () {
        var formName = $(this).children('td:first').text().trim().toLowerCase();
        if (contains(formName, textToSearch)) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
});
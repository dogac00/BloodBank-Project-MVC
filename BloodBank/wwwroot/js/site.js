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

var creditCardValidated, amountValidated;

$('#NameOnCard').keyup(function () {
    $(this).val($(this).val().toUpperCase());
});

$('#CreditCardNumber').keyup(function () {
    var inputValue = $(this).val();
    if (!$.isNumeric(inputValue) || inputValue.length != 16) {
        $('#CreditCardValidation').text("Credit Card Number must be a 16 character number.");
        creditCardValidated = false;
    }
    else {
        $('#CreditCardValidation').text("");
        creditCardValidated = true;
    }
});

$('#Amount').keyup(function () {
    var amountInput = $(this).val();

    if (!$.isNumeric(amountInput)) {
        $('#AmountValidation').text("Please enter a valid number.");
        amountValidated = false;
    }
    else {
        $('#AmountValidation').text("");
        amountValidated = true;
    }
});

$('#DonationForm').submit(function (e) {
    if (!creditCardValidated || !amountValidated) {
        $('#ValidationCheckText').text("Please check your inputs.");
        e.preventDefault();
    }
});
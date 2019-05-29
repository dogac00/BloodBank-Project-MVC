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

    $('.divToSearch').each(function () {
        if (contains($(this).find('#description').text().toLowerCase(), searchedValue)
            && selectedValue == "All" || $(this).find('#bloodType').text().trim() == selectedValue) {
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

$('#NameOnCard').keyup(function () {
    if (/[^a-zA-Z\s]/.test($('#NameOnCard').val())) {
        $('#NameOnCardValidation').text("Name should only consist of letters.");
    }
    else {
        $('#NameOnCardValidation').text("");
    }
});

$('#DonateButton').click(function () {
    $('#ValidationCheckText').text("");
});
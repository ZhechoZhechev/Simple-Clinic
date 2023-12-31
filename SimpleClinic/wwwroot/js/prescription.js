﻿
$(document).ready(function () {
    var url = $("#selectPatient").data("url");

    $("#selectPatient").select2({
        ajax: {
            url: url,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            },
            cache: true
        },
        minimumInputLength: 3
    });
});
$(document).ready(function () {
    var url = $("#selectMedicament").data("url");

    $("#selectMedicament").select2({
        ajax: {
            url: url,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            },
            cache: true
        },
        minimumInputLength: 3,
        language: {
            inputTooShort: function (args) {
                return "Please enter at least 3 characters";
            },
            noResults: function () {
                return "No such medicament, add it first";
            }
        }
    });
});

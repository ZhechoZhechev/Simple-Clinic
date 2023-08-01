document.getElementById('addMedicament').addEventListener('click', function () {
    var medicamentName = document.getElementById('medicamentName').value;
    var medicamentQuantity = parseInt(document.getElementById('medicamentQuantity').value);

    var medicamentData = {
        Name: medicamentName,
        QuantityPerDayMilligrams: medicamentQuantity
    };

    $.ajax({
        url: $("#medicamentModal").data("url"),
        type: 'POST',
        data: medicamentData,
        success: function (response) {
            if (response.success) {
                alert(response.message);
                $('#medicamentModal').modal('hide');
            } else {
                alert('Failed to add medicament. Please try again.');
            }
        },
        error: function () {
            alert('An error occurred while adding the medicament. Please try again.');
        }
    });
});
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

$(document).ready(function () {
    addOnChangeSpecialty();

    addOnChangeDate();

    $("#DoctorId").on("change", function () {
        $("#Date").removeAttr("disabled");
    });
});


function addOnChangeSpecialty() {
    $("#SpecialtyId").on("change", function (e) {
        $.ajax({
            url: "../Appointment/AllDoctorsInSpecialty",
            method: "get",
            contentType: "application/json",
            data: {
                specialtyId: this.value,
            }
        })
            .done(res => {
                fillDoctors(res);
            });
    });
}

function fillDoctors(data) {
    $("#DoctorId option").each(function () {
        $(this).remove();
    });

    if (data.length > 0) {
        $("#DoctorId").append($("<option></option>").attr("value", "").text("Doctor"));
        for (let el of data) {
            $("#DoctorId").append($("<option></option>").attr("value", el.doctorId).text(el.fullName));
        }
        $("#DoctorId").removeAttr("disabled");
    } else {
        $("#DoctorId").attr("disabled", true);
    }
}

function addOnChangeDate() {
    $("#Date").on("change", function (e) {
        $.ajax({
            url: "../Appointment/HoursAvailability",
            method: "get",
            contentType: "application/json",
            data: {
                date: this.value,
            }
        })
            .done(res => {
                $('#HourId').removeAttr('disabled');
                for (let hour of res) {
                    if (hour.available === true) {
                        $('#HourId').append('<option value="' + hour.hourId + '">' + hour.hour + '</option>');
                    }
                }
            });
    });
}
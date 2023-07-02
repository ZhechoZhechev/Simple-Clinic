$(function () {
    $("#calendar").datepicker({
        dateFormat: "dd/mm/yy",
        onSelect: function () {
            let pickedDate = this.value;
            $("#calendar-value").val(pickedDate);
            $.ajax({
                url: "../Appointment/DoctorAvailability",
                method: "get",
                contentType: "application/json",
                data: {
                    doctorId: $("#DoctorId").val(),
                    date: pickedDate
                }
            })
                .done(res => {
                    $("#hours").empty();
                    $.each(res, function (k, v) {
                        if (v.availability) {
                            $("#hours").append(function () {
                                return $("<p class='available'>" + v.hour + "</p>").on("click", function () {
                                    $("#hour-value").val(v.hour);
                                });
                            })
                        }
                        else {
                            $("#hours").append("<p class='not-available'>" + v.hour + "</p>");
                        }
                    });
                
            });
        }
    });
});
$(document).ready(function () {
    var doctorId = $("#doctorId").data("doctor-id");

    $.ajax({
        url: "/Patient/Appointment/GetAvailableDatesService",
        type: "GET",
        data: { doctorId: doctorId },
        success: function (availableDates) {
            $("#datepicker").datepicker({
                minDate: 0,
                beforeShowDay: function (date) {
                    var dateString = $.datepicker.formatDate("mm/dd/yy", date);
                    var formattedAvailableDates = availableDates.map(function (date) {
                        return $.datepicker.formatDate("mm/dd/yy", new Date(date));
                    });
                    var isAvailable = formattedAvailableDates.includes(dateString);
                    return [isAvailable, isAvailable ? 'available-date' : ''];
                },
                onSelect: function (selectedDate) {
                    $.ajax({
                        url: "/Patient/Appointment/GetServiceSchedule",
                        type: "GET",
                        data: { selectedDate: selectedDate, doctorId: doctorId },
                        success: function (schedule) {
                            $("#doctor-schedule").html(schedule);
                        },
                        error: function () {
                            alert("Error fetching doctor's schedule.");
                        }
                    });
                }
            });

            var currentDate = $.datepicker.formatDate("mm/dd/yy", new Date());
            $("#datepicker").datepicker("setDate", currentDate);
        },
        error: function () {
            alert("Error fetching available dates.");
        }
    });
});
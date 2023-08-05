﻿namespace SimpleClinic.Core.Models.DoctorModels;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


public class ServiceScheduleViewModel
{
    public ServiceScheduleViewModel()
    {
        Day = DateTime.Today;
        TimeSlots = new List<TimeSlotViewModel>();

        var startTime = TimeZoneInfo.ConvertTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0), TimeZoneInfo.Local);

        while (startTime.Hour < 16)
        {
            TimeSlots.Add(new TimeSlotViewModel
            {
                StartTime = startTime,
                IsAvailable = false
            });

            startTime = startTime.AddHours(1);
        }
    }

    public string? Id { get; set; }

    [Required]
    public DateTime Day { get; set; }

    public List<TimeSlotViewModel> TimeSlots { get; set; }

}

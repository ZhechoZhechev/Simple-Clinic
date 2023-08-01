﻿namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;
public class TimeSlotViewModel
{

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    public bool IsAvailable { get; set; }
}
﻿using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeCreateVM
    {
        [Required]
        [Length(4, 150, ErrorMessage = "Name must be between 4 and 150 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1, 30, ErrorMessage = "Number of days must be between 1 and 30.")]
        [Display(Name = "Maximum Allocation of Days")]
        public int NumberOfDays { get; set; }
    }
}

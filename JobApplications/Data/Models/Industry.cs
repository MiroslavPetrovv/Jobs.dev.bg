﻿using System.ComponentModel.DataAnnotations;

namespace JobApplications.Data.Models
{
    public class Industry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; } 
    }
}
﻿using _3rdBackendProject.Models.Base;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3rdBackendProject.Models
{
    public class Slide:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace SpravaPenezDeti.Dtos
{
    public class DiteUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Jmeno { get; set; }
    }
}

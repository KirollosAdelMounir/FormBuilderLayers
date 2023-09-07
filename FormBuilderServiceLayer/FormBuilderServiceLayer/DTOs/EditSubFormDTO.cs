﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FormBuilderServiceLayer.DTOs
{
    public class EditSubFormDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1, 12)]
        public int Size { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Order { get; set; }
    }
}

﻿using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.DTOs
{
    public class EditFormDataDTO
    {
        public int Id { get; set; }
        public int SubFormId { get; set; }
        public string FieldQuestion { get; set; } = null!;

        public bool IsMandatory { get; set; }
        [Range(1, 12)]
        public int Size { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Order { get; set; }
        [Range(1, 14)]
        public int FieldType { get; set; }
/*        public enum FieldType { Label, TextBox, DatePicker, TextArea, Paragraph, CheckBox, H1, H2, H3, H4, Sperator, Table, Div }
*/    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormBuilderDB.Models;

public partial class FormsDatum
{
    public int Id { get; set; }
    [ForeignKey("SubFormID")]
    public int SubFormId { get; set; }

    public string FieldQuestion { get; set; } = null!;

    public bool IsMandatory { get; set; }
    [Range(1,12)]
    public int Size { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Order { get; set; }

    public FieldType Fieldtype { get; set; }

    public virtual SubForm SubForm { get; set; } = null!;
}
    public enum FieldType:int { Label =1 , TextBox , DatePicker , TextArea, Paragraph, CheckBox, H1, H2, H3, H4, Sperator, Table, Div , ComboBox}

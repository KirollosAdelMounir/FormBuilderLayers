using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormBuilderDB.Models;

public partial class SubForm
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [ForeignKey("MainFormID")]
    public int MainFormId { get; set; }
    [Range(1,12)]
    public int Size { get; set; }

    public int Order { get; set; }
    public virtual MainForm MainForm { get; set; } = null!;
}

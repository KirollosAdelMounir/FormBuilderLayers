using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormBuilderDB.Models;

public partial class Response
{
    public int Id { get; set; }
    [ForeignKey("MainFormID")]
    public int MainFormId { get; set; }

    public DateTime DateOfResponse { get; set; }
    public virtual MainForm MainForm { get; set; } = null!;
}

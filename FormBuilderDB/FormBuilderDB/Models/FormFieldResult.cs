using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormBuilderDB.Models;

public partial class FormFieldResult
{
    public int Id { get; set; }

    [ForeignKey("FormDataID")]
    public int FormDataId { get; set; }

    public string Response { get; set; } = null!;
    [ForeignKey("ResponseID")]
    public int ResponseId { get; set; }

    public virtual FormsDatum FormData { get; set; } = null!;

    public virtual Response ResponseNavigation { get; set; } = null!;
}

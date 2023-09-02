using System;
using System.Collections.Generic;

namespace FormBuilderDB.Models;

public partial class MainForm
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; } = false;

    public DateTime DateOfCreation { get; set; } = DateTime.Now;

    public int NumberOfResponses { get; set; } = 0;


}

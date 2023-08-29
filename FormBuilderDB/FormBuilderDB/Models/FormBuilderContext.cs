using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderDB.Models;

public partial class FormBuilderContext : DbContext
{
    public FormBuilderContext()
    {
    }

    public FormBuilderContext(DbContextOptions<FormBuilderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FormFieldResult> FormFieldResults { get; set; }

    public virtual DbSet<FormsDatum> FormsData { get; set; }

    public virtual DbSet<MainForm> MainForms { get; set; }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<SubForm> SubForms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FormBuilder;Trusted_Connection=True;");

   
}

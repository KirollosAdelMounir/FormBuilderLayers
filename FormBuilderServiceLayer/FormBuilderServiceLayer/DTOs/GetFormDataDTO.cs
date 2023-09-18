using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.DTOs
{
    public class GetFormDataDTO
    {
        public int Id { get; set; }
        public int SubFormId { get; set; }

        public string FieldQuestion { get; set; }

        public bool IsMandatory { get; set; }
        public int Size { get; set; }
        public int Order { get; set; }

        public int Fieldtype { get; set; }

        public List<string>? ComboBoxItems { get; set; }


    }
}

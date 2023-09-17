using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.DTOs
{
    public class EditMainFormDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EditSubFormDTO> Subforms { get; set; }
    }
}

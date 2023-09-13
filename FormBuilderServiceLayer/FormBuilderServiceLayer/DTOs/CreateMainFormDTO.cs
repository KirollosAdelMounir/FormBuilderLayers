using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.DTOs
{
    public class CreateMainFormDTO
    {
        public string Name { get; set; }  
        public List<CreateSubFormDTO> Subforms { get; set; }

    }
}

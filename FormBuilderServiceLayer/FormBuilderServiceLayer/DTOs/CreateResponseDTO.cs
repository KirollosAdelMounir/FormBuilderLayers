using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.DTOs
{
    public class CreateResponseDTO
    {
        public int MainFormID { get; set; }
        public List<CreateFormFieldResultDTO> fieldResultDTOs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.DTOs
{
    public class GetSubFormDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainFormId { get; set; }
        [Range(1, 12)]
        public int Size { get; set; }
        public int Order { get; set; }

        public List<GetFormDataDTO> formData { get; set; }
    }
}

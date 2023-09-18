using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.DTOs
{
    public class GetMainFormDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int NumberOfResponses { get; set; }
        public List<GetSubFormDTO> Subforms { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FormBuilderServiceLayer.DTOs
{
    public class CreateSubFormDTO
    {
        public string Name { get; set; }
        public int MainFormId { get; set; }
        [Range(1, 12)]
        public int Size { get; set; }
        public int Order { get; set; }
    }
}

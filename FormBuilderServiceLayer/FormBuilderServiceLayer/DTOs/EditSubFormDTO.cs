using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FormBuilderServiceLayer.DTOs
{
    public class EditSubFormDTO
    {
        public string Name { get; set; }
        public int MainFormID { get; set; }
        [Range(1, 12)]
        public int Size { get; set; }
        public int Order { get; set; }
    }
}

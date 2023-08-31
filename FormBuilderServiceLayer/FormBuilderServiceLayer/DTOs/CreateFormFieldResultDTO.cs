using System.ComponentModel.DataAnnotations.Schema;

namespace FormBuilderServiceLayer.DTOs
{
    public class CreateFormFieldResultDTO
    {
        public int FormDataID { get; set; }
        public string Response { get; set; }
        public int ResponseID { get; set; }
    }
}

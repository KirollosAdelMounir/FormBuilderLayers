using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.DTOs
{
    public class CreateComboBoxDTO
    {
        public List<string> ValueNames { get; set; }
        public int FormsDatumID { get; set; }
    }
}

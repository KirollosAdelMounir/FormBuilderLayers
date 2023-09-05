using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderDB.Models
{
    public class ComboBoxFormData
    {
        [Key]
        public int Value { get; set; }
        public string ValueName { get; set; }
        [ForeignKey("FormsDatumId")]
        public int FormsDatumID { get; set; }
        public virtual FormsDatum? FormsDatum { get; set; } = null;
    }
}

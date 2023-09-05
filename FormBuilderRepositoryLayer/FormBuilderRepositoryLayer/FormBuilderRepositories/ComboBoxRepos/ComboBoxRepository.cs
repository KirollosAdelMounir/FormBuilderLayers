using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.ComboBoxRepos
{
    public class ComboBoxRepository : Repository<ComboBoxFormData, FormBuilderContext>, IComboBoxRepository
    {
        public ComboBoxRepository(FormBuilderContext d) : base(d)
        {
        }

        public async Task<List<ComboBoxFormData>> ListOfComboItems(int FormDataID)
        {
            var list = await GetAll();
            return list.Where(x=>x.FormsDatumID == FormDataID).ToList();
        }
    }
}

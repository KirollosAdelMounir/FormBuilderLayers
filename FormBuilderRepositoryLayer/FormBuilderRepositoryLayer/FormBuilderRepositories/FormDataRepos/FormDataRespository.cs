using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.FormDataRepos
{
    public class FormDataRespository : Repository<FormsDatum, FormBuilderContext>, IFormDataRepository
    {
        public FormDataRespository(FormBuilderContext d) : base(d)
        {
        }

        public List<FormsDatum> FetchWithSubID(int subID)
        {
            return GetAll().Where(x=>x.SubFormId == subID).ToList();
        }
    }
}

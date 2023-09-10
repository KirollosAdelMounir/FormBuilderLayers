using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<FormsDatum>> FetchWithSubID(int subID)
        {
            /*var list = await GetAll();
            return list.Where(x=>x.SubFormId == subID).ToList();*/
            var list = await _dbContext.FormsData.Where(x => x.SubFormId == subID).ToListAsync();
            return list;
        }
    }
}

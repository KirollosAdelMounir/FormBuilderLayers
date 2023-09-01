using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.FormFieldResultRepos
{
    public class FormFieldResultRepository : Repository<FormFieldResult, FormBuilderContext>, IFormFieldResultRepository
    {
        public FormFieldResultRepository(FormBuilderContext d) : base(d)
        {
        }

        public async Task<List<FormFieldResult>> AllFieldsInAResponse(int responseId)
        {
            var list = await GetAll();
            return list.Where(x=>x.ResponseId == responseId).ToList();
        }
    }
}

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

        public List<FormFieldResult> AllFieldsInAResponse(int responseId)
        {
            return GetAll().Where(x=>x.ResponseId == responseId).ToList();
        }
    }
}

using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.ResponseRepos
{
    public class ResponseRepository : Repository<Response, FormBuilderContext>, IResponseRepository
    {
        public ResponseRepository(FormBuilderContext d) : base(d)
        {
        }

        public async Task<List<Response>> AllResponsesToAForm(int formId)
        {
            var list = await GetAll();
            return list.Where(x=>x.MainFormId == formId).ToList();
        }
    }
}

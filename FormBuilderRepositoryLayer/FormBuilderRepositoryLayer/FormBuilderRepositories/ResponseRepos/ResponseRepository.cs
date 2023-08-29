using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.ResponseRepos
{
    internal class ResponseRepository : Repository<Response, FormBuilderContext>, IResponseRepository
    {
        public ResponseRepository(FormBuilderContext d) : base(d)
        {
        }

        public List<Response> AllResponsesToAForm(int formId)
        {
            return GetAll().Where(x=>x.MainFormId == formId).ToList();
        }
    }
}

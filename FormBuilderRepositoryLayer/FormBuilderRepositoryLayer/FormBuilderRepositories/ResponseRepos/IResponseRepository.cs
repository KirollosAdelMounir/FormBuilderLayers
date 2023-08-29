using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.ResponseRepos
{
    public interface IResponseRepository : IRepository<Response,FormBuilderContext>
    {
        List<Response> AllResponsesToAForm(int formId);
    }
}

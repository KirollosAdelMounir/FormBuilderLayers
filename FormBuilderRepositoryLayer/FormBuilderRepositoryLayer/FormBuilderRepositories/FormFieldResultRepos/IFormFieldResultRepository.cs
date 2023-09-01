using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.FormFieldResultRepos
{
    public interface IFormFieldResultRepository: IRepository<FormFieldResult, FormBuilderContext>
    {
        Task<List<FormFieldResult>> AllFieldsInAResponse(int responseId);
    }
}

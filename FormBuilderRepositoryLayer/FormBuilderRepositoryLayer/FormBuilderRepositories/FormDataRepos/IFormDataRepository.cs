using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.FormDataRepos
{
    public interface IFormDataRepository: IRepository<FormsDatum, FormBuilderContext>
    {
        Task<List<FormsDatum>> FetchWithSubID(int subID);
    }
}

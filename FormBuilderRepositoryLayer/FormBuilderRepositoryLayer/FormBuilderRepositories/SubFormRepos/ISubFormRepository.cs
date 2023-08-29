using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.SubFormRepos
{
    public interface ISubFormRepository : IRepository<SubForm,FormBuilderContext>
    {
    }
}

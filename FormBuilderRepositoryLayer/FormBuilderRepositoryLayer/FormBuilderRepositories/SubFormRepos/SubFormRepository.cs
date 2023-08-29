using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.SubFormRepos
{
    public class SubFormRepository : Repository<SubForm, FormBuilderContext>, ISubFormRepository
    {
        public SubFormRepository(FormBuilderContext d) : base(d)
        {
        }
    }
}

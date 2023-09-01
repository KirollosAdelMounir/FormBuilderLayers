using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.MainFormRepos
{
    public class MainFormRepository : Repository<MainForm, FormBuilderContext>, IMainFormRepository
    {
        public MainFormRepository(FormBuilderContext d) : base(d)
        {
        }

        public async Task<List<MainForm>> GetUndeleted()
        {
            var list = await GetAll();
            return list.Where(x=>x.IsDeleted== false).ToList();
        }

        public async Task IncrementResponse(MainForm mainForm)
        {
            mainForm.NumberOfResponses++;
            await UpdateAsync(mainForm);
        }

        public async Task SoftDelete(MainForm mainForm)
        {
            mainForm.IsDeleted = true;
            await UpdateAsync(mainForm);
        }
    }
}

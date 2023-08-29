using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.MainFormRepos
{
    public interface IMainFormRepository:  IRepository<MainForm,FormBuilderContext>
    {
        Task SoftDelete(MainForm mainForm);
        Task IncrementResponse(MainForm mainForm);
        List<MainForm> GetUndeleted();
    }
}

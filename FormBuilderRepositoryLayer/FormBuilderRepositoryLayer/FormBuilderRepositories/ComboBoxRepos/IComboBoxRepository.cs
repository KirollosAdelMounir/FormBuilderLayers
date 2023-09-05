using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.FormBuilderRepositories.ComboBoxRepos
{
    public interface IComboBoxRepository : IRepository<ComboBoxFormData,FormBuilderContext>
    {
        Task<List<ComboBoxFormData>> ListOfComboItems(int FormDataID);
    }
}

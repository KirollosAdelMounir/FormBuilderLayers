using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.Services
{
    public class MainFormService
    {
        private readonly IUnitOfRepositories _unitOfRepositories;
        public MainFormService(IUnitOfRepositories unitOfRepositories) 
        {
            _unitOfRepositories = unitOfRepositories;
        }

        public async Task CreateForm(string name)
        {
            MainForm form = new MainForm { Name = name };
            await _unitOfRepositories.mainFormRepository.AddAsync(form);
        }

        public MainForm GetForm(int id)
        {
            return _unitOfRepositories.mainFormRepository.GetById(id);
        }

        public List<MainForm> GetAllForms()
        {
            return _unitOfRepositories.mainFormRepository.GetUndeleted();
        }

        public async Task DeleteForm(int id) 
        {
            var mainForm = GetForm(id);
            if (mainForm != null)
            {
                await _unitOfRepositories.mainFormRepository.SoftDelete(mainForm);
            }
        }

        public async Task EditForm(int id,string name)
        {
            var mainform = GetForm(id);
            if(mainform != null)
            {
                mainform.Name = name;
                await _unitOfRepositories.mainFormRepository.UpdateAsync(mainform);
            }
        }
    }
}

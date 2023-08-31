using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.Services
{
    public class SubFormService
    {
        private readonly IUnitOfRepositories _unitOfRepositories;
        public SubFormService(IUnitOfRepositories unitOfRepositories) { _unitOfRepositories = unitOfRepositories; }
        public async Task Create(SubForm subForm) 
        {
            MainForm mainForm = _unitOfRepositories.mainFormRepository.GetById(subForm.MainFormId);
            if(mainForm != null && mainForm.IsDeleted ==false)
            {
                await _unitOfRepositories.subFormRepository.AddAsync(subForm);
            }
        }
        public SubForm ViewByID(int id)
        {
            return _unitOfRepositories.subFormRepository.GetById(id);
        }
        public List<SubForm> GetList(int mainformID) 
        {
            return _unitOfRepositories.subFormRepository.GetAllForms(mainformID);
        }
        public async Task Delete(int id)
        {
            var subform = ViewByID(id);
            if(subform!= null)
            {
                var formdata = _unitOfRepositories.formDataRepository.FetchWithSubID(subform.Id);
                if(formdata!= null)
                {
                    foreach(var form in formdata)
                    {
                       await _unitOfRepositories.formDataRepository.DeleteAsync(form);
                    }
                }
                await _unitOfRepositories.subFormRepository.DeleteAsync(subform);
            }
        }
        public async Task Edit(SubForm subForm)
        {
           await  _unitOfRepositories.subFormRepository.UpdateAsync(subForm);
        }
    }
}

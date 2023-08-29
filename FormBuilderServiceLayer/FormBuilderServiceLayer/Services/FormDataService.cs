using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.Services
{
    public class FormDataService
    {
        private readonly IUnitOfRepositories _unitOfRepositories;
        public FormDataService(IUnitOfRepositories unitofRespositories) 
        {
            _unitOfRepositories = unitofRespositories;
        }
        public FormsDatum FormDataByID(int Id)
        {
            return _unitOfRepositories.formDataRepository.GetById(Id);
        }
        public List<FormsDatum> GetAllFields(int SubFormId)
        {
            return _unitOfRepositories.formDataRepository.FetchWithSubID(SubFormId);
        }
        public async Task CreateField(FormsDatum formsDatum)
        {
            SubForm subForm = _unitOfRepositories.subFormRepository.GetById(formsDatum.SubFormId);
            if(subForm != null)
            {
                await _unitOfRepositories.formDataRepository.AddAsync(formsDatum);
            }
        }
        public async Task UpdateField(FormsDatum formsDatum) 
        {
            FormsDatum formData = FormDataByID(formsDatum.Id);
            if(formData != null)
            {
                await _unitOfRepositories.formDataRepository.UpdateAsync(formData);
            }
        }
        public async Task DeleteField(FormsDatum formsDatum)
        {
            FormsDatum formsDatum1 = FormDataByID(formsDatum.Id);
            if (formsDatum1 != null)
            {
                await _unitOfRepositories.formDataRepository.DeleteAsync(formsDatum1);
            }
        }
    }
}

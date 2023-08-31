using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.Services
{
    public class FormFieldResultService
    {
        private readonly IUnitOfRepositories _unitOfRepositories;
        public FormFieldResultService(IUnitOfRepositories unitofRespositories)
        {
            _unitOfRepositories = unitofRespositories;
        }
        public FormFieldResult GetFieldResponse(int fieldId) 
        {
            return _unitOfRepositories.formFieldResultRepository.GetById(fieldId);
        }
        public List<FormFieldResult> GetFieldResults(int responseId) 
        {
            return _unitOfRepositories.formFieldResultRepository.AllFieldsInAResponse(responseId);
        }
        public async Task Create(FormFieldResult formFieldResult)
        {
            Response response = _unitOfRepositories.responseRepository
                .GetById(formFieldResult.ResponseId);
            FormsDatum formsDatum = _unitOfRepositories.formDataRepository
                .GetById(formFieldResult.FormDataId);
            if(formsDatum != null && response != null)
            {
                await _unitOfRepositories.formFieldResultRepository.AddAsync(formFieldResult);
            }
        }
    }
}

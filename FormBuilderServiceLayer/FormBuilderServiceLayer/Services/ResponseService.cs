using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.Services
{
    public class ResponseService
    {
        private readonly IUnitOfRepositories _unitOfRepositories;
        public ResponseService(IUnitOfRepositories unitOfRepositories) 
        {
            _unitOfRepositories = unitOfRepositories;
        }
        public async Task Create(int mainFormID)
        {
            var mainform = _unitOfRepositories.mainFormRepository.GetById(mainFormID);
            if(mainform != null && mainform.IsDeleted ==false)
            {
                Response response = new Response {MainFormId = mainFormID };
                await _unitOfRepositories.responseRepository.AddAsync(response);
            }
        }

        public List<Response> GetAllResponses(int formId)
        {
            return _unitOfRepositories.responseRepository.AllResponsesToAForm(formId);
        }

        public Response GetResponse(int id)
        {
            return _unitOfRepositories.responseRepository.GetById(id);
        }
    }
}

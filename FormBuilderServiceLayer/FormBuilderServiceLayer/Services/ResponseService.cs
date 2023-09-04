using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.FormBuilderRepositories.MainFormRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.ResponseRepos;
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
        private readonly IResponseRepository responseRepository;
        private readonly IMainFormRepository mainFormRepository;
        public ResponseService(IResponseRepository responseRepository, IMainFormRepository mainFormRepository) 
        {
            this.responseRepository = responseRepository;
            this.mainFormRepository = mainFormRepository;
        }
        public async Task<GenericResponseModel<String>> Create(int mainFormID)
        {
            GenericResponseModel<String> responseModel = new();
            var mainform = await mainFormRepository.GetById(mainFormID);
            if (mainform != null && mainform.IsDeleted == false)
            {
                Response response = new Response { MainFormId = mainFormID };
                await mainFormRepository.IncrementResponse(mainform);
                await responseRepository.AddAsync(response);
                responseModel.Data = "Response Created";
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Item not found!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }

        public async Task<GenericResponseModel<List<Response>>> GetAllResponses(int formId)
        {
            GenericResponseModel<List<Response>> responseModel = new();
            List<Response> response = await responseRepository.AllResponsesToAForm(formId);
            responseModel.Data = response;
            return responseModel;
        }

        public async Task<GenericResponseModel<Response>> GetResponse(int id)
        {
            GenericResponseModel<Response> responseModel = new();
            Response response = await responseRepository.GetById(id);
            if (response != null)
            {
                responseModel.Data = response;
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Item not found!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }
    }
}

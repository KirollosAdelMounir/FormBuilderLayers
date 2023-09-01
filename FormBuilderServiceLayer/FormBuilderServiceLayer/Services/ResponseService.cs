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
        public async Task Create(int mainFormID)
        {
            var mainform = await mainFormRepository.GetById(mainFormID);
            if(mainform != null && mainform.IsDeleted ==false)
            {
                Response response = new Response {MainFormId = mainFormID };
                await responseRepository.AddAsync(response);
            }
        }

        public async Task<List<Response>> GetAllResponses(int formId)
        {
            return await responseRepository.AllResponsesToAForm(formId);
        }

        public async Task<Response> GetResponse(int id)
        {
            return await responseRepository.GetById(id);
        }
    }
}

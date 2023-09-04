using AutoMapper;
using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.FormBuilderRepositories.FormDataRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.FormFieldResultRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.ResponseRepos;
using FormBuilderRepositoryLayer.UnitOfWork;
using FormBuilderServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.Services
{
    public class FormFieldResultService
    {
        private readonly IFormFieldResultRepository formFieldResultRepository;
        private readonly IResponseRepository responseRepository;
        private readonly IFormDataRepository formDataRepository;
        private readonly IMapper mapper;
        public FormFieldResultService(IFormFieldResultRepository formFieldResultRepository,
            IResponseRepository responseRepository, IFormDataRepository formDataRepository,
            IMapper mapper)
        {
            this.responseRepository = responseRepository;
            this.formFieldResultRepository = formFieldResultRepository;
            this.formDataRepository = formDataRepository;
            this.mapper = mapper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FormFieldResult, CreateFormFieldResultDTO>().ReverseMap();
            });

            this.mapper = config.CreateMapper();
        }
        public async Task<GenericResponseModel<FormFieldResult>> GetFieldResponse(int fieldId) 
        {
            GenericResponseModel<FormFieldResult> responseModel = new();
            var response =  await formFieldResultRepository.GetById(fieldId);
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
        public async Task<GenericResponseModel<List<FormFieldResult>>> GetFieldResults(int responseId) 
        {
            GenericResponseModel<List<FormFieldResult>> responseModel = new();
            List<FormFieldResult> response = await formFieldResultRepository
                .AllFieldsInAResponse(responseId);
            responseModel.Data = response;
            return responseModel;
        }
        public async Task<GenericResponseModel<String>> Create(CreateFormFieldResultDTO FormFieldResultDTO)
        {
            GenericResponseModel<String> responseModel = new();
            FormFieldResult formFieldResult = mapper.Map<FormFieldResult>(FormFieldResultDTO);
            Response response = await responseRepository.GetById(formFieldResult.ResponseId);
            FormsDatum formsDatum = await formDataRepository.GetById(formFieldResult.FormDataId);
            if(formsDatum != null && response != null)
            {
                await formFieldResultRepository.AddAsync(formFieldResult);
                responseModel.Data = "Response Sent!";
            }
            return responseModel;
        }
    }
}

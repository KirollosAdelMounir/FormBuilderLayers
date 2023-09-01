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
        public async Task<FormFieldResult> GetFieldResponse(int fieldId) 
        {
            return await formFieldResultRepository.GetById(fieldId);
        }
        public async Task<List<FormFieldResult>> GetFieldResults(int responseId) 
        {
            return await formFieldResultRepository.AllFieldsInAResponse(responseId);
        }
        public async Task Create(CreateFormFieldResultDTO FormFieldResultDTO)
        {
            FormFieldResult formFieldResult = mapper.Map<FormFieldResult>(FormFieldResultDTO);
            Response response = await responseRepository.GetById(formFieldResult.ResponseId);
            FormsDatum formsDatum = await formDataRepository.GetById(formFieldResult.FormDataId);
            if(formsDatum != null && response != null)
            {
                await formFieldResultRepository.AddAsync(formFieldResult);
            }
        }
    }
}

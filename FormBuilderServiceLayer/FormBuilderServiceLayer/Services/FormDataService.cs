using AutoMapper;
using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.FormBuilderRepositories.FormDataRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.SubFormRepos;
using FormBuilderRepositoryLayer.UnitOfWork;
using FormBuilderServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.Services
{
    public class FormDataService
    {
        private readonly IFormDataRepository formDataRepository;
        private readonly ISubFormRepository subFormRepository;
        private readonly IMapper mapper;
        public FormDataService(IFormDataRepository formDataRepository, ISubFormRepository subFormRepository,
            IMapper mapper) 
        {
            this.formDataRepository = formDataRepository;
            this.subFormRepository = subFormRepository;
            this.mapper = mapper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FormsDatum, CreateFormDataDTO>().ReverseMap();
                cfg.CreateMap<FormsDatum, EditFormDataDTO>().ReverseMap();
            });

            this.mapper = config.CreateMapper();
        }
        public async Task<FormsDatum> FormDataByID(int Id)
        {
            return await formDataRepository.GetById(Id);
        }
        public async Task<List<FormsDatum>> GetAllFields(int SubFormId)
        {
            return await formDataRepository.FetchWithSubID(SubFormId);
        }
        public async Task CreateField(CreateFormDataDTO formDataDTO)
        {
            SubForm subForm = await subFormRepository.GetById(formDataDTO.SubFormId);
            if(subForm != null)
            {
                FormsDatum formsDatum = mapper.Map<FormsDatum>(formDataDTO);
                await formDataRepository.AddAsync(formsDatum);
            }
        }
        public async Task UpdateField(EditFormDataDTO formDataDTO) 
        {
            var formData = await FormDataByID(formDataDTO.Id);
            if(formData != null)
            {
                formData = mapper.Map<FormsDatum>(formDataDTO);
                await formDataRepository.UpdateAsync(formData);
            }
        }
        public async Task DeleteField(int id)
        {
            FormsDatum formsDatum = await FormDataByID(id);
            if (formsDatum != null)
            {
                await formDataRepository.DeleteAsync(formsDatum);
            }
        }
    }
}

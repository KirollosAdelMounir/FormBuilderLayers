using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.FormBuilderRepositories.FormDataRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.MainFormRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.SubFormRepos;
using FormBuilderRepositoryLayer.UnitOfWork;
using FormBuilderServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace FormBuilderServiceLayer.Services
{
    public class SubFormService
    {
        private readonly ISubFormRepository subFormRepository;
        private readonly IMainFormRepository mainFormRepository;
        private readonly IFormDataRepository formDataRepository;
        private readonly IMapper mapper;
        public SubFormService(ISubFormRepository subFormRepository, IMainFormRepository mainFormRepository,
            IFormDataRepository formDataRepository, IMapper mapper) 
        { 
            this.subFormRepository = subFormRepository;
            this.mainFormRepository = mainFormRepository;
            this.formDataRepository = formDataRepository;
            this.mapper = mapper;
            var config = new MapperConfiguration(cfg =>
            {
                /*cfg.CreateMap<FormsDatum, CreateFormDataDTO>().ReverseMap();
                cfg.CreateMap<FormsDatum, EditFormDataDTO>().ReverseMap();
                cfg.CreateMap<FormFieldResult, CreateFormFieldResultDTO>().ReverseMap();*/
                cfg.CreateMap<SubForm, CreateSubFormDTO>().ReverseMap();
                cfg.CreateMap<SubForm, EditSubFormDTO>().ReverseMap();
            });

            this.mapper = config.CreateMapper();
        }
        public async Task Create(CreateSubFormDTO createSubFormDTO) 
        {
            MainForm mainForm = await mainFormRepository.GetById(createSubFormDTO.MainFormId);
            if(mainForm != null && mainForm.IsDeleted ==false)
            {
                SubForm subForm = mapper.Map<SubForm>(createSubFormDTO);
                await subFormRepository.AddAsync(subForm);
            }
        }
        public async Task<SubForm> ViewByID(int id)
        {
            return await subFormRepository.GetById(id);
        }
        public async Task<List<SubForm>> GetList(int mainformID) 
        {
            return await subFormRepository.GetAllForms(mainformID);
        }
        public async Task Delete(int id)
        {
            var subform = await ViewByID(id);
            if(subform!= null)
            {
                var formdata = await formDataRepository.FetchWithSubID(subform.Id);
                if(formdata!= null)
                {
                    foreach(var form in formdata)
                    {
                       await formDataRepository.DeleteAsync(form);
                    }
                }
                await subFormRepository.DeleteAsync(subform);
            }
        }
        public async Task Edit(EditSubFormDTO subFormDTO)
        {
            SubForm edittedSubForm = mapper.Map<SubForm>(subFormDTO);
            await subFormRepository.UpdateAsync(edittedSubForm);
        }
    }
}

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
                cfg.CreateMap<SubForm, CreateSubFormDTO>().ReverseMap();
                cfg.CreateMap<SubForm, EditSubFormDTO>().ReverseMap();
            });

            this.mapper = config.CreateMapper();
        }
        public async Task<GenericResponseModel<String>> Create(CreateSubFormDTO createSubFormDTO) 
        {
            GenericResponseModel<String> responseModel = new();
            MainForm mainForm = await mainFormRepository.GetById(createSubFormDTO.MainFormId);
            if(mainForm != null && mainForm.IsDeleted ==false)
            {
                SubForm subForm = mapper.Map<SubForm>(createSubFormDTO);
                await subFormRepository.AddAsync(subForm);
                responseModel.Data = "Form Created";
            }
            return responseModel;
        }
        public async Task<GenericResponseModel<SubForm>> ViewByID(int id)
        {
            GenericResponseModel<SubForm> responseModel = new();
            var response = await subFormRepository.GetById(id);
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
        public async Task<GenericResponseModel<List<SubForm>>> GetList(int mainformID) 
        {
            GenericResponseModel<List<SubForm>> responseModel = new();
            var response = await subFormRepository.GetAllForms(mainformID);
            if (response != null)
            {
                responseModel.Data = response;
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Items not found!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }
        public async Task<GenericResponseModel<SubForm>> Delete(int id)
        {
            GenericResponseModel<SubForm> responseModel = new();
            var subform = await subFormRepository.GetById(id);
            if (subform!= null)
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
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Item not found!";
                responseModel.ErrorList.Add(model);
            }

            return responseModel;
        }
        public async Task<GenericResponseModel<SubForm>> Edit(EditSubFormDTO subFormDTO)
        {
            GenericResponseModel<SubForm> responseModel = new();
            SubForm edittedSubForm = mapper.Map<SubForm>(subFormDTO);

            var subform = await subFormRepository.GetById(subFormDTO.Id);
            if (subform != null)
            {
                subform = edittedSubForm;
                await subFormRepository.UpdateAsync(subform);
                responseModel.Data = subform;
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

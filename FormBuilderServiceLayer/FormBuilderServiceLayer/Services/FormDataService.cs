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
        public async Task<GenericResponseModel<FormsDatum>> FormDataByID(int Id)
        {
            GenericResponseModel<FormsDatum> responseModel = new();
            FormsDatum response = await formDataRepository.GetById(Id);
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
        public async Task<GenericResponseModel<List<FormsDatum>>> GetAllFields(int SubFormId)
        {
            GenericResponseModel<List<FormsDatum>> responseModel = new();
            List<FormsDatum> response = await formDataRepository.FetchWithSubID(SubFormId);
            responseModel.Data = response;
            return responseModel;
        }
        public async Task<GenericResponseModel<String>> CreateField(CreateFormDataDTO formDataDTO)
        {
            GenericResponseModel<String> responseModel = new();
            SubForm subForm = await subFormRepository.GetById(formDataDTO.SubFormId);
            if (subForm != null)
            {
                FormsDatum formsDatum = mapper.Map<FormsDatum>(formDataDTO);
                await formDataRepository.AddAsync(formsDatum);
                responseModel.Data = "Form Data Created";
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Invalid SubformId!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }
        public async Task<GenericResponseModel<FormsDatum>> UpdateField(EditFormDataDTO formDataDTO)
        {
            GenericResponseModel<FormsDatum> responseModel = new();
            var formData = await formDataRepository.GetById(formDataDTO.Id);
            if (formData != null)
            {
                formData = mapper.Map<FormsDatum>(formDataDTO);
                await formDataRepository.UpdateAsync(formData);
                responseModel.Data = formData;
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Item not found!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }
        public async Task<GenericResponseModel<FormsDatum>> DeleteField(int id)
        {
            GenericResponseModel<FormsDatum> responseModel = new();
            FormsDatum formsDatum = await formDataRepository.GetById(id);
            if (formsDatum != null)
            {
                await formDataRepository.DeleteAsync(formsDatum);
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

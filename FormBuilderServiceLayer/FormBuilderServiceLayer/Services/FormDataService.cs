using AutoMapper;
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

namespace FormBuilderServiceLayer.Services
{
    public class FormDataService
    {
        private readonly IFormDataRepository formDataRepository;
        private readonly ISubFormRepository subFormRepository;
        private readonly IMainFormRepository mainFormRepository;
        private readonly IMapper mapper;
        public FormDataService(IFormDataRepository formDataRepository, ISubFormRepository subFormRepository,
            IMainFormRepository mainFormRepository, IMapper mapper) 
        {
            this.formDataRepository = formDataRepository;
            this.subFormRepository = subFormRepository;
            this.mainFormRepository = mainFormRepository;
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
                MainForm mainForm = await mainFormRepository.GetById(subForm.MainFormId);
                if (mainForm.NumberOfResponses == 0)
                {
                    List<FormsDatum> forms = await formDataRepository.FetchWithSubID(formDataDTO.SubFormId);
                    FormsDatum forma = forms.FirstOrDefault(x => x.Order == formDataDTO.Order);
                    if (forma != null)
                    {
                        responseModel.Data = "Cannot have 2 forms with the same order " +
                            " because they will overlap!";
                    }
                    else
                    {
                        FormsDatum formsDatum = mapper.Map<FormsDatum>(formDataDTO);
                        await formDataRepository.AddAsync(formsDatum);
                        responseModel.Data = "Form Data Created";
                    }
                }
                else
                {
                    ErrorListModel model = new ErrorListModel();
                    model.Message = "Cannot add new questions after responses have been received!";
                    responseModel.ErrorList.Add(model);
                }
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
                SubForm subForm = await subFormRepository.GetById(formDataDTO.SubFormId);
                if (subForm != null)
                {
                    MainForm mainForm = await mainFormRepository.GetById(subForm.MainFormId);
                    if (mainForm.NumberOfResponses == 0)
                    {
                        List<FormsDatum> forms = await formDataRepository.FetchWithSubID(formDataDTO.SubFormId);
                        FormsDatum forma = forms.FirstOrDefault(x => x.Order == formDataDTO.Order);
                        if (forma != null)
                        {
                            ErrorListModel model = new ErrorListModel();
                            model.Message = "Cannot have 2 forms with the same order" +
                                " because they will overlap!";
                            responseModel.ErrorList.Add(model);
                        }
                        else
                        {
                            FormsDatum formsDatum = mapper.Map<FormsDatum>(formDataDTO);
                            await formDataRepository.UpdateAsync(formsDatum);
                            responseModel.Data = formsDatum;
                        }
                    }
                    else
                    {
                        ErrorListModel model = new ErrorListModel();
                        model.Message = "Cannot edit after responses have been received!";
                        responseModel.ErrorList.Add(model);
                    }
                }
                else
                {
                    ErrorListModel model = new ErrorListModel();
                    model.Message = "Invalid SubformId!";
                    responseModel.ErrorList.Add(model);
                }
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

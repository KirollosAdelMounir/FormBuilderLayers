using AutoMapper;
using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.FormBuilderRepositories.MainFormRepos;
using FormBuilderRepositoryLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FormBuilderServiceLayer.DTOs;
using FormBuilderRepositoryLayer.FormBuilderRepositories.SubFormRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.FormDataRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.ComboBoxRepos;

namespace FormBuilderServiceLayer.Services
{

    public class MainFormService
    {
        private readonly IMainFormRepository mainFormRepository;
        private readonly ISubFormRepository subFormRepository;
        private readonly IFormDataRepository formdatarepository;
        private readonly IComboBoxRepository comboBoxRepository;
        private readonly IMapper mapper;
        public MainFormService(IMainFormRepository mainFormRepository, IMapper mapper,ISubFormRepository subFormRepository,IFormDataRepository formDataRepository,IComboBoxRepository comboBoxRepository) 
        {
            this.mainFormRepository = mainFormRepository;
            this.mapper = mapper;
            this.subFormRepository = subFormRepository;
            this.formdatarepository = formDataRepository;
            this.comboBoxRepository = comboBoxRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubForm, GetSubFormDTO>().ReverseMap();
                cfg.CreateMap<MainForm, GetMainFormDTO>().ReverseMap();
                cfg.CreateMap<FormsDatum, GetFormDataDTO>().ReverseMap();
            });
            this.mapper = config.CreateMapper();
        }

        /*public async Task<GenericResponseModel<String>> CreateForm(string FormName)
        {
            GenericResponseModel<String> responseModel = new();
            MainForm form = new MainForm { Name = FormName };
            await mainFormRepository.AddAsync(form);
            responseModel.Data = "Form Created";
            return responseModel;
        }*/

        public async Task<GenericResponseModel<GetMainFormDTO>> GetForm(int id)
        {
            GenericResponseModel<GetMainFormDTO> responseModel = new();
            MainForm mainForm =  await mainFormRepository.GetById(id);
            if (mainForm != null && mainForm.IsDeleted==false)
            {
                List<SubForm> subForms = await subFormRepository.GetAllForms(id);
                List<GetSubFormDTO> subFormDTOs = new List<GetSubFormDTO>();
                if(subForms.Count > 0)
                {
                    List<GetFormDataDTO> formsDataDTO = new List<GetFormDataDTO>();
                    foreach(SubForm subForm in subForms)
                    {
                        List<FormsDatum> formsDatumList = await formdatarepository.FetchWithSubID(subForm.Id);
                        if(formsDatumList.Count > 0) 
                        {
                            foreach(FormsDatum formsDatum in formsDatumList)
                            {
                                GetFormDataDTO getFormDataDTO = mapper.Map<GetFormDataDTO>(formsDatum);
                                if (formsDatum.Fieldtype.ToString().Equals("ComboBox"))
                                {
                                    List<ComboBoxFormData> comboBox = await comboBoxRepository.ListOfComboItems(formsDatum.Id);
                                    if(comboBox.Count > 0)
                                    {
                                        List<string> comboBoxString = new List<string>();
                                        foreach (ComboBoxFormData comboBoxFormData in comboBox)
                                        {
                                            comboBoxString.Add(comboBoxFormData.ValueName);
                                        }
                                        getFormDataDTO.ComboBoxItems = comboBoxString;
                                    }
                                }
                                formsDataDTO.Add(getFormDataDTO);
                            }
                        }
                        GetSubFormDTO subFormDTO = mapper.Map<GetSubFormDTO>(subForm);
                        subFormDTO.formData = formsDataDTO;
                        subFormDTOs.Add(subFormDTO);
                    }
                }
                GetMainFormDTO mainFormDTO = mapper.Map<GetMainFormDTO>(mainForm);
                mainFormDTO.Subforms = subFormDTOs;
                responseModel.Data = mainFormDTO;
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Item not found!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }

        public async Task<GenericResponseModel<List<MainForm>>> GetAllForms()
        {
            GenericResponseModel<List<MainForm>> responseModel = new();
            List<MainForm> response = await mainFormRepository.GetUndeleted();
            responseModel.Data = response;
            return responseModel;
        }

        public async Task<GenericResponseModel<MainForm>> DeleteForm(int id) 
        {
            GenericResponseModel<MainForm> responseModel = new();
            var mainForm = await mainFormRepository.GetById(id);
            if (mainForm != null && mainForm.IsDeleted==false)
            {
                await mainFormRepository.SoftDelete(mainForm);
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Item not found!";
                responseModel.ErrorList.Add(model);
            }

            return responseModel;
        }

        public async Task<GenericResponseModel<MainForm>> EditForm(int id, EditMainFormDTO formDTO)
        {
            GenericResponseModel<MainForm> responseModel = new();
            var mainform = await mainFormRepository.GetById(id);
            if (mainform != null && mainform.IsDeleted == false && mainform.NumberOfResponses == 0)
            {
                mainform.Name = formDTO.Name;
                await mainFormRepository.UpdateAsync(mainform);
                foreach(var subform in formDTO.Subforms)
                {
                    var sub = await subFormRepository.GetById(subform.Id);
                    if(sub!= null)
                    {
                        sub = mapper.Map<SubForm>(subform);
                        await subFormRepository.UpdateAsync(sub);
                        foreach (var formData in subform.FormData)
                        {
                            var formData1 = await formdatarepository.GetById(formData.Id);
                            if (formData1 != null)
                            {
                                formData1 = mapper.Map<FormsDatum>(formData);
                                await formdatarepository.UpdateAsync(formData1);
                            }
                            else
                            {
                                ErrorListModel model = new ErrorListModel();
                                model.Message = "FormData with ID: " + formData.Id + " not found!";
                                responseModel.ErrorList.Add(model);
                                return responseModel;
                            }
                        }
                    }
                    else
                    {
                        ErrorListModel model = new ErrorListModel();
                        model.Message = "Subform with ID: "+ subform.Id +" not found!";
                        responseModel.ErrorList.Add(model);
                        return responseModel;
                    }
                }
                responseModel.Data = mainform;
            }
            else if (mainform != null && mainform.IsDeleted == false && mainform.NumberOfResponses > 0)
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Cannot edit form after responses have been received!";
                responseModel.ErrorList.Add(model);
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Form not found!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }
        public async Task<GenericResponseModel<MainForm>> CreateForm(CreateMainFormDTO createMainFormDTO)
        {
            GenericResponseModel<MainForm> responseModel = new();
            MainForm form = new MainForm { Name = createMainFormDTO.Name };
            await mainFormRepository.AddAsync(form);
            List<int> orders = new List<int>();
            foreach (var item in createMainFormDTO.Subforms)
            {
                int order = orders.FirstOrDefault(x=> x==item.Order);
                if(order!= null && order<0)
                {
                    ErrorListModel model = new ErrorListModel();
                    model.Message = "Cannot have 2 subforms with the same order because they " +
                        "will overlap!";
                    responseModel.ErrorList.Add(model);
                    responseModel.Data = form;
                    return responseModel;
                }
                else
                {
                    orders.Add(item.Order);
                    SubForm subForm = new SubForm
                    {
                        MainFormId = form.Id,
                        Name = item.Name,
                        Size = item.Size,
                        Order = item.Order
                    };
                    await subFormRepository.AddAsync(subForm);
                    List<int> formDataOrders = new List<int>();
                    foreach (var formdataitem in item.FormData)
                    {
                        int formDataOrder = formDataOrders.FirstOrDefault(x => x == formdataitem.Order);
                        if (formDataOrder != null && formDataOrder < 0)
                        {
                            ErrorListModel model = new ErrorListModel();
                            model.Message = "Cannot have form data with the same order because they " +
                                "will overlap!";
                            responseModel.ErrorList.Add(model);
                            responseModel.Data = form;
                            return responseModel;
                        }
                        else
                        {
                            formDataOrders.Add(formdataitem.Order);
                            FormsDatum formsDatum = new FormsDatum()
                            {
                                SubFormId = subForm.Id,
                                Order = formdataitem.Order,
                                Size = formdataitem.Size
                            ,
                                Fieldtype = (FieldType)formdataitem.FieldType,
                                FieldQuestion = formdataitem.FieldQuestion,
                                IsMandatory = formdataitem.IsMandatory
                            };
                            await formdatarepository.AddAsync(formsDatum);
                            if (formdataitem.FieldType == 14 && formdataitem.ComboBoxItems != null)
                            {
                                foreach (var comboitem in formdataitem.ComboBoxItems)
                                {
                                    ComboBoxFormData comboBoxFormData = new ComboBoxFormData()
                                    {
                                        FormsDatumID = formsDatum.Id,
                                        ValueName = comboitem
                                    };
                                    await comboBoxRepository.AddAsync(comboBoxFormData);
                                }
                            }
                        }  
                    }
                } 
            }
            responseModel.Data = form;
            return responseModel;
        }
    }
}

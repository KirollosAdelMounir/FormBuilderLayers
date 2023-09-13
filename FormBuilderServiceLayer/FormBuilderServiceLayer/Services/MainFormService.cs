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
        }

        public async Task<GenericResponseModel<String>> CreateForm(string FormName)
        {
            GenericResponseModel<String> responseModel = new();
            MainForm form = new MainForm { Name = FormName };
            await mainFormRepository.AddAsync(form);
            responseModel.Data = "Form Created";
            return responseModel;
        }

        public async Task<GenericResponseModel<MainForm>> GetForm(int id)
        {
            GenericResponseModel<MainForm> responseModel = new();
            MainForm response =  await mainFormRepository.GetById(id);
            if (response != null && response.IsDeleted==false)
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

        public async Task<GenericResponseModel<MainForm>> EditForm(int id, string name)
        {
            GenericResponseModel<MainForm> responseModel = new();
            var mainform = await mainFormRepository.GetById(id);
            if (mainform != null && mainform.IsDeleted == false && mainform.NumberOfResponses == 0)
            {
                mainform.Name = name;
                await mainFormRepository.UpdateAsync(mainform);
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
                model.Message = "Item not found!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }
        public async Task<GenericResponseModel<String>> CreateForm(CreateMainFormDTO createMainFormDTO)
        {
            GenericResponseModel<String> responseModel = new();
            MainForm form = new MainForm { Name = createMainFormDTO.Name };
            await mainFormRepository.AddAsync(form);
            List<int> orders = new List<int>();
            foreach (var item in createMainFormDTO.Subforms)
            {
                int order = orders.FirstOrDefault(x=> x==item.Order);
                if(order!= null && order!=0)
                {
                    responseModel.Data = "Cannot have 2 subforms with the same order because they " +
                        "will overlap!";
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
                        if (formDataOrder != null && formDataOrder != 0)
                        {
                            responseModel.Data = "Cannot have 2 items with the same order because they " +
                                "will overlap!";
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
            responseModel.Data = "Form Created";
            return responseModel;
        }
    }
}

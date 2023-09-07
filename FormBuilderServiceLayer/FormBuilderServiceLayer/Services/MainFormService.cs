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

namespace FormBuilderServiceLayer.Services
{
    public class MainFormService
    {
        private readonly IMainFormRepository mainFormRepository;
        private readonly IMapper mapper;
        public MainFormService(IMainFormRepository mainFormRepository, IMapper mapper) 
        {
            this.mainFormRepository = mainFormRepository;
            this.mapper = mapper;
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
    }
}

using AutoMapper;
using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.FormBuilderRepositories.ComboBoxRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.FormDataRepos;
using FormBuilderServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.Services
{
    public class ComboBoxService
    {
        IComboBoxRepository _repository;
        private readonly IMapper mapper;
        private readonly IFormDataRepository formRepository;
        public ComboBoxService(IComboBoxRepository repository, IMapper mapper, IFormDataRepository formRepository)
        {
            _repository = repository;
            this.formRepository = formRepository;
            this.mapper = mapper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FormsDatum, CreateFormDataDTO>().ReverseMap();
                cfg.CreateMap<FormsDatum, EditFormDataDTO>().ReverseMap();
            });

            this.mapper = config.CreateMapper();
        }
        public async Task<GenericResponseModel<List<ComboBoxFormData>>> GetAllComboBoxFields(int FormDataID)
        {
            GenericResponseModel<List<ComboBoxFormData>> responseModel = new();
            List<ComboBoxFormData> response = await _repository.ListOfComboItems(FormDataID);
            responseModel.Data = response;
            return responseModel;
        }

        public async Task<GenericResponseModel<ComboBoxFormData>> Get(int Id)
        {
            GenericResponseModel<ComboBoxFormData> responseModel = new();
            ComboBoxFormData response = await _repository.GetById(Id);
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
        public async Task<GenericResponseModel<string>> Add(CreateComboBoxDTO createComboBox)
        {
            GenericResponseModel<String> responseModel = new();
            FormsDatum formsDatum = await formRepository.GetById(createComboBox.FormsDatumID);
            if(formsDatum != null)
            {
                ComboBoxFormData comboBox = mapper.Map<ComboBoxFormData>(createComboBox);
                await _repository.AddAsync(comboBox);
                responseModel.Data = "ComboBoxField Created!";
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Invalid FormData ID!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }
        public async Task<GenericResponseModel<ComboBoxFormData>> UpdateComboBox(EditComboBoxDTO comboBoxDTO)
        {
            GenericResponseModel<ComboBoxFormData> responseModel = new();
            var comboBox = await _repository.GetById(comboBoxDTO.Value);
            var formData = await formRepository.GetById(comboBoxDTO.FormsDatumID);
            if (formData != null && comboBox !=null)
            {
                comboBox = mapper.Map<ComboBoxFormData>(comboBoxDTO);
                await _repository.UpdateAsync(comboBox);
                responseModel.Data = comboBox;
            }
            else
            {
                ErrorListModel model = new ErrorListModel();
                model.Message = "Item not found!";
                responseModel.ErrorList.Add(model);
            }
            return responseModel;
        }
        public async Task<GenericResponseModel<ComboBoxFormData>> DeleteComboBoxField(int id)
        {
            GenericResponseModel<ComboBoxFormData> responseModel = new();
            ComboBoxFormData comboBox = await _repository.GetById(id);
            if (comboBox != null)
            {
                await _repository.DeleteAsync(comboBox);
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

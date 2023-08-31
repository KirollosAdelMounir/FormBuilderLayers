using AutoMapper;
using FormBuilderDB.Models;
using FormBuilderServiceLayer.DTOs;

namespace FormBuilderAppLayer
{
    public class FormProfile: Profile
    {
        public FormProfile()
        {
            CreateMap<FormsDatum,CreateFormDataDTO>();
            CreateMap<FormsDatum,EditFormDataDTO>();
            CreateMap<FormFieldResult,CreateFormFieldResultDTO>();
            CreateMap<SubForm,CreateSubFormDTO>();
            CreateMap<SubForm,EditSubFormDTO>();
        }
    }
}

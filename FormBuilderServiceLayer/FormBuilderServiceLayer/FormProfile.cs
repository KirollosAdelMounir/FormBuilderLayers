using AutoMapper;
using FormBuilderDB.Models;
using FormBuilderServiceLayer.DTOs;

namespace FormBuilderServiceLayer
{
    public class FormProfile: Profile
    {
        public FormProfile()
        {
            CreateMap<FormsDatum,CreateFormDataDTO>().ReverseMap();
            CreateMap<FormsDatum,EditFormDataDTO>().ReverseMap();
            CreateMap<FormFieldResult,CreateFormFieldResultDTO>().ReverseMap();
            CreateMap<SubForm,CreateSubFormDTO>().ReverseMap();
            CreateMap<SubForm,EditSubFormDTO>().ReverseMap();

        }
    }
}

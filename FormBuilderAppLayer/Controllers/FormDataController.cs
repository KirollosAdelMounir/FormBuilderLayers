using AutoMapper;
using FormBuilderDB.Models;
using FormBuilderServiceLayer.DTOs;
using FormBuilderServiceLayer.UnitOfServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderAppLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormDataController : ControllerBase
    {
        private IUnitOfServices unitOfServices;
        private readonly IMapper mapper;

        public FormDataController(IUnitOfServices unitOfServices, IMapper mapper)
        {
            this.unitOfServices = unitOfServices;
            this.mapper = mapper;
        }

        [HttpGet("GetFormData{id}")]
        public IActionResult Get(int id)
        {
            FormsDatum formsDatum = unitOfServices.FormDataService.FormDataByID(id);
            if (formsDatum != null)
            {
                return Ok(formsDatum);
            }
            return BadRequest("Invalid Id");
        }

        [HttpGet("GetAllFormData{subFormId}")]
        public IActionResult GetAll(int subFormId)
        {
            List<FormsDatum> formsData = unitOfServices.FormDataService.GetAllFields(subFormId);
            if(formsData != null) 
            { 
                return Ok(formsData); 
            }
            return BadRequest("Invalid SubForm Id");
        }

        [HttpPost("CreateFormData")]
        public async Task<IActionResult> CreateFormData(CreateFormDataDTO formDataDTO)
        {
            FormsDatum formsDatum = mapper.Map<FormsDatum>(formDataDTO);
            await unitOfServices.FormDataService.CreateField(formsDatum);
            return Ok("Form Data Created");
        }

        [HttpPut("EditFormData")]
        public async Task<IActionResult> EditFormData(EditFormDataDTO formDataDTO)
        {
            FormsDatum formsDatum = mapper.Map<FormsDatum>(formDataDTO);
            await unitOfServices.FormDataService.UpdateField(formsDatum);
            return Ok("Form Data Edited");
        }

        [HttpDelete("DeleteFormData{id}")]
        public async Task<IActionResult> DeleteFormData(int id)
        {
            FormsDatum formsDatum = unitOfServices.FormDataService.FormDataByID(id);
            await unitOfServices.FormDataService.DeleteField(formsDatum);
            return Ok("Form Data Deleted");
        }
    }
}

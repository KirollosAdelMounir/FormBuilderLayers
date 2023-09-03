using AutoMapper;
using FormBuilderDB.Models;
using FormBuilderServiceLayer.DTOs;
using FormBuilderServiceLayer.Services;
using FormBuilderServiceLayer.UnitOfServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderAppLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormDataController : ControllerBase
    {
        private FormDataService formDataService;
        public FormDataController(FormDataService formDataService)
        {
            this.formDataService = formDataService;
        }

        [HttpGet("GetFormData{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var formsDatum = await formDataService.FormDataByID(id);
            if (formsDatum != null)
            {
                return Ok(formsDatum);
            }
            return BadRequest("Invalid Id");
        }

        [HttpGet("GetAllFormData{subFormId}")]
        public async Task<ActionResult> GetAll(int subFormId)
        {
            List<FormsDatum> formsData = await formDataService.GetAllFields(subFormId);
            if(formsData != null) 
            { 
                return Ok(formsData); 
            }
            return BadRequest("Invalid SubForm Id");
        }

        [HttpPost("CreateFormData")]
        public async Task<IActionResult> CreateFormData(CreateFormDataDTO formDataDTO)
        {
            await formDataService.CreateField(formDataDTO);
            return Ok("Form Data Created");
        }

        [HttpPut("EditFormData")]
        public async Task<IActionResult> EditFormData(EditFormDataDTO formDataDTO)
        {
            await formDataService.UpdateField(formDataDTO);

            return Ok("Form Data Edited");
        }

        [HttpDelete("DeleteFormData{id}")]
        public async Task<IActionResult> DeleteFormData(int id)
        {
            await formDataService.DeleteField(id);
            return Ok("Form Data Deleted");
        }
    }
}

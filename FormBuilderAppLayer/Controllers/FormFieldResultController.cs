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
    public class FormFieldResultController : ControllerBase
    {
        private FormFieldResultService formFieldResultService;

        public FormFieldResultController(FormFieldResultService formFieldResultService)
        {
            this.formFieldResultService = formFieldResultService;
        }

        [HttpGet("GetFieldResponse{id}")]
        public async Task<IActionResult> Get(int id)
        {
            FormFieldResult formFieldResult = await formFieldResultService.GetFieldResponse(id);
            if (formFieldResult != null)
            {
                return Ok(formFieldResult);
            }
            return BadRequest("Invalid Id");
        }
        [HttpGet("GetAllResponses")]
        public async Task<IActionResult> GetAll (int ResponseId)
        {
            List<FormFieldResult> results = await formFieldResultService.GetFieldResults(ResponseId);
            if(results != null) 
            { 
                return Ok(results); 
            }
            return BadRequest("Invalid Id");
        }

        [HttpPost("CreateFormFieldResult")]
        public async Task<IActionResult> CreateFormFieldResult(CreateFormFieldResultDTO FormFieldResultDTO)
        {
            await formFieldResultService.Create(FormFieldResultDTO);
            return Ok("Form Field Result Created");
        }

    }
}

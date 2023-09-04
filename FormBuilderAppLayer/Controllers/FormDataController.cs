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
            var res = await formDataService.FormDataByID(id);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("GetAllFormData{subFormId}")]
        public async Task<ActionResult> GetAll(int subFormId)
        {
            var res = await formDataService.GetAllFields(subFormId);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("CreateFormData")]
        public async Task<IActionResult> CreateFormData(CreateFormDataDTO formDataDTO)
        {
            var res = await formDataService.CreateField(formDataDTO);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("EditFormData")]
        public async Task<IActionResult> EditFormData(EditFormDataDTO formDataDTO)
        {
            var res = await formDataService.UpdateField(formDataDTO);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }

        [HttpDelete("DeleteFormData{id}")]
        public async Task<IActionResult> DeleteFormData(int id)
        {
            var res = await formDataService.DeleteField(id);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
    }
}

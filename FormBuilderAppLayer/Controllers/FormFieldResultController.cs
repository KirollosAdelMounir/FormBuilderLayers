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
            var res = await formFieldResultService.GetFieldResponse(id);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("GetAllResponses")]
        public async Task<IActionResult> GetAll(int ResponseId)
        {
            var res = await formFieldResultService.GetFieldResults(ResponseId);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("CreateFormFieldResult")]
        public async Task<IActionResult> CreateFormFieldResult(CreateFormFieldResultDTO FormFieldResultDTO)
        {
            var res = await formFieldResultService.Create(FormFieldResultDTO);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }

    }
}

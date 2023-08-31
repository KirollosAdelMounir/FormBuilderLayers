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
    public class FormFieldResultController : ControllerBase
    {
        private IUnitOfServices unitOfServices;
        private readonly IMapper mapper;

        public FormFieldResultController(IUnitOfServices unitOfServices, IMapper mapper)
        {
            this.unitOfServices = unitOfServices;
            this.mapper = mapper;
        }

        [HttpGet("GetFieldResponse{id}")]
        public IActionResult Get(int id)
        {
            FormFieldResult formFieldResult = unitOfServices.FormFieldResultService.GetFieldResponse(id);
            if (formFieldResult != null)
            {
                return Ok(formFieldResult);
            }
            return BadRequest("Invalid Id");
        }

        public IActionResult GetAll (int ResponseId)
        {
            List<FormFieldResult> results = unitOfServices.FormFieldResultService.GetFieldResults(ResponseId);
            if(results != null) 
            { 
                return Ok(results); 
            }
            return BadRequest("Invalid Id");
        }

        [HttpPost("CreateFormFieldResult")]
        public async Task<IActionResult> CreateFormFieldResult(CreateFormFieldResultDTO FormFieldResultDTO)
        {
            FormFieldResult formFieldResult = mapper.Map<FormFieldResult>(FormFieldResultDTO);
            await unitOfServices.FormFieldResultService.Create(formFieldResult);
            return Ok("Form Field Result Created");
        }

    }
}

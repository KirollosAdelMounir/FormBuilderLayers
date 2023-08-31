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
    public class MainFormController : ControllerBase
    {
        private readonly IUnitOfServices _unitOfServices;
        private readonly IMapper _mapper;
        public MainFormController(IUnitOfServices unitOfServices,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfServices = unitOfServices;
        }
        [HttpPost("CreateForm")]
        public async Task<IActionResult> CreateForm(string name , [FromBody] CreateSubFormDTO subFormDTO)
        {
            await _unitOfServices.MainFormService.CreateForm(name);
            SubForm subForm = _mapper.Map<SubForm>(subFormDTO);
            await _unitOfServices.SubFormService.Create(subForm);
            return Ok("Form Created Successfully");
        }
        [HttpGet("ViewForm")]
        public IActionResult GetForm(int id)
        {
            return Ok(_unitOfServices.MainFormService.GetForm(id));
        }
        [HttpGet("ViewAllForms")]
        public IActionResult GetAllForms()
        {
            return Ok(_unitOfServices.MainFormService.GetAllForms());
        }
        [HttpPut("EditFormName")]
        public async Task <IActionResult> EditForm(int id , string name)
        {
            await _unitOfServices.MainFormService.EditForm(id, name);
            return Ok("Name Changed Successfully");
        }
        [HttpDelete("DeleteForm")]
        public async Task<IActionResult> DeleteForm (int id)
        {
            await _unitOfServices.MainFormService.DeleteForm(id);
            return Ok("Form Deleted Successfully");
        }
    }
}

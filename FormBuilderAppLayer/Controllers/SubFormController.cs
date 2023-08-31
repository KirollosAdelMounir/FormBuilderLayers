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
    public class SubFormController : ControllerBase
    {
        private readonly IUnitOfServices _unitOfServices;
        private readonly IMapper _mapper;
        public SubFormController(IUnitOfServices unitOfServices , IMapper mapper)
        {
            _unitOfServices = unitOfServices;
            _mapper = mapper;
        }
        [HttpPost("CreateNewSubForm")]
        public async Task<IActionResult> CreateNewSubform(CreateSubFormDTO subformdto)
        {
            SubForm subForm = _mapper.Map<SubForm>(subformdto);
            await _unitOfServices.SubFormService.Create(subForm);
            return Ok("New Sub Form Added");
        }
        [HttpGet("GetSubForm")]
        public IActionResult GetSubForm(int id)
        {
            return Ok(_unitOfServices.SubFormService.ViewByID(id));
        }
        [HttpGet("GetAllSubForms")]
        public IActionResult GetAllSubForms (int id)
        {
            return Ok (_unitOfServices.SubFormService.GetList(id));
        }
        [HttpPut("EditSubForm")]
        public async Task<IActionResult> EditSubForm(EditSubFormDTO subformdto) 
        {
            SubForm edittedSubForm = _mapper.Map<SubForm>(subformdto);
            await _unitOfServices.SubFormService.Edit(edittedSubForm);
            return Ok("Sub Form Edited");
        }
        [HttpDelete("DeleteSubForm")]
        public async Task<IActionResult> DeleteSubForm (int ID)
        {
            await _unitOfServices.SubFormService.Delete(ID);
            return Ok("Sub Form Deleted Successfully");
        }
    }
}

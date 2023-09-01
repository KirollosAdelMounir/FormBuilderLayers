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
    public class SubFormController : ControllerBase
    {
        private readonly SubFormService subFormService;
        private readonly IMapper _mapper;
        public SubFormController(SubFormService subFormService, IMapper mapper)
        {
            this.subFormService = subFormService;
            _mapper = mapper;
        }
        [HttpPost("CreateNewSubForm")]
        public async Task<IActionResult> CreateNewSubform(CreateSubFormDTO subformdto)
        {
            await subFormService.Create(subformdto);
            return Ok("New Sub Form Added");
        }
        [HttpGet("GetSubForm")]
        public async Task<IActionResult> GetSubForm(int id)
        {
            var result = await subFormService.ViewByID(id);
            if (result == null)
                return BadRequest("Subform Not Found");
            return Ok(result);
        }
        [HttpGet("GetAllSubForms")]
        public async Task<IActionResult> GetAllSubForms (int id)
        {
            var result = await subFormService.GetList(id);
            if (result == null)
                return BadRequest("No subforms found!");
            return Ok (result);
        }
        [HttpPut("EditSubForm")]
        public async Task<IActionResult> EditSubForm(EditSubFormDTO subformdto) 
        {
            await subFormService.Edit(subformdto);
            return Ok("Sub Form Edited");
        }
        [HttpDelete("DeleteSubForm")]
        public async Task<IActionResult> DeleteSubForm (int ID)
        {
            await subFormService.Delete(ID);
            return Ok("Sub Form Deleted Successfully");
        }
    }
}

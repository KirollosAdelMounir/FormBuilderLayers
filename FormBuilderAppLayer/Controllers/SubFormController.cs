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
            var res = await subFormService.Create(subformdto);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("GetSubForm")]
        public async Task<IActionResult> GetSubForm(int id)
        {
            var res = await subFormService.ViewByID(id);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("GetAllSubForms")]
        public async Task<IActionResult> GetAllSubForms(int id)
        {
            var res = await subFormService.GetList(id);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpPut("EditSubForm")]
        public async Task<IActionResult> EditSubForm(EditSubFormDTO subformdto)
        {
            var res = await subFormService.Edit(subformdto);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("DeleteSubForm")]
        public async Task<IActionResult> DeleteSubForm(int ID)
        {
            var res = await subFormService.Delete(ID);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
    }
}

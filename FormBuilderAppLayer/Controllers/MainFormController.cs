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
    public class MainFormController : ControllerBase
    {
        private readonly MainFormService mainFormService;
        private readonly SubFormService subFormService;
        
        public MainFormController(MainFormService mainFormService)
        {
            this.mainFormService = mainFormService;
        }
        [HttpPost("CreateForm")]
        public async Task<IActionResult> CreateForm(CreateMainFormDTO formDTO)
        {
            var res = await mainFormService.CreateForm(formDTO);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("ViewForm")]
        public async Task<IActionResult> GetForm(int id)
        {
            var res = await mainFormService.GetForm(id);
            if(res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("ViewAllForms")]
        public async Task<IActionResult> GetAllForms()
        {
            var result = await mainFormService.GetAllForms();
            if(result.ErrorList.Any())
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("EditForm")]
        public async Task <IActionResult> EditForm(int id , EditMainFormDTO mainFormDTO)
        {
            var res = await mainFormService.EditForm(id, mainFormDTO);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("DeleteForm")]
        public async Task<IActionResult> DeleteForm (int id)
        {
            var res = await mainFormService.DeleteForm(id);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
    }
}

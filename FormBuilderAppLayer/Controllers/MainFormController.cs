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
        public async Task<IActionResult> CreateForm(CreateFormDTO createFormDTO)
        {
            await mainFormService.CreateForm(createFormDTO);
            /*CreateSubFormDTO subFormDTO = createFormDTO.SubForm;
            await subFormService.Create(subFormDTO);*/
            return Ok("Form Created Successfully");
        }
        [HttpGet("ViewForm")]
        public async Task<IActionResult> GetForm(int id)
        {
            return Ok(await mainFormService.GetForm(id));
        }
        [HttpGet("ViewAllForms")]
        public async Task<IActionResult> GetAllForms()
        {
            var result = await mainFormService.GetAllForms();
            if(result == null)
                return BadRequest("No forms");
            return Ok(result);
        }
        [HttpPut("EditFormName")]
        public async Task <IActionResult> EditForm(int id , string name)
        {
            await mainFormService.EditForm(id, name);
            return Ok("Name Changed Successfully");
        }
        [HttpDelete("DeleteForm")]
        public async Task<IActionResult> DeleteForm (int id)
        {
            await mainFormService.DeleteForm(id);
            return Ok("Form Deleted Successfully");
        }
    }
}

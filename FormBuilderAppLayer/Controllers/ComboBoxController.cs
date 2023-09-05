using FormBuilderDB.Models;
using FormBuilderServiceLayer.DTOs;
using FormBuilderServiceLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderAppLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboBoxController : ControllerBase
    {
        private ComboBoxService comboBoxService;

        public ComboBoxController(ComboBoxService comboBoxService)
        {
            this.comboBoxService = comboBoxService;
        }

        [HttpGet("GetComboBox")]
        public async Task<IActionResult> GetAComboBoxField(int id)
        {
            var res = await comboBoxService.Get(id);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("GetAllComboBoxFields")]
        public async Task<IActionResult> GetAllComboBoxField(int formdataid)
        {
            var res = await comboBoxService.GetAllComboBoxFields(formdataid);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem(CreateComboBoxDTO createComboBoxDTO)
        {
            var res = await comboBoxService.Add(createComboBoxDTO);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpPut("EditItemName")]
        public async Task<IActionResult> EditName(EditComboBoxDTO editComboBoxDTO)
        {
            var res = await comboBoxService.UpdateComboBox(editComboBoxDTO);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> DeleteItem (int id)
        {
            var res = await comboBoxService.DeleteComboBoxField(id);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
    }
}

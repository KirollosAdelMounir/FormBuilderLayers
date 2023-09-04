using FormBuilderDB.Models;
using FormBuilderServiceLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FormBuilderServiceLayer.UnitOfServices;

namespace FormBuilderAppLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private ResponseService responseService;

        public ResponseController(ResponseService responseService)
        {
            this.responseService = responseService;
        }

        [HttpPost("CreateResponse")]
        public async Task<IActionResult> CreateResponse(int mainFormId)
        {
            var res = await responseService.Create(mainFormId);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("GetAllResponses")]
        public async Task<IActionResult> GetAll(int mainFormId)
        {
            var res = await responseService.GetAllResponses(mainFormId);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("GetResponse")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await responseService.GetResponse(id);
            if (res.ErrorList.Any())
                return BadRequest(res);
            return Ok(res);
        }
    }
}

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

        //Need to use generic response
        [HttpPost("CreateResponse")]
        public async Task<IActionResult> CreateResponse(int mainFormId)
        {
            await responseService.Create(mainFormId);
            return Ok("Response Created");
        }

        [HttpGet("GetAllResponses")]
        public async Task<IActionResult> GetAll(int mainFormId)
        {
            var result = await responseService.GetAllResponses(mainFormId);
            if (result == null)
                return BadRequest("No responses found!");
            return Ok(result);
        }

        [HttpGet("GetResponse")] 
        public async Task<IActionResult> Get(int id) 
        {
            var result = await responseService.GetResponse(id);
            if (result == null)
                return BadRequest("Response not found!");
            return Ok(result);
        }
    }
}

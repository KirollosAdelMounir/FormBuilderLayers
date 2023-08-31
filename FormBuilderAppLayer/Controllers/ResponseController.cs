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
        private IUnitOfServices unitOfServices;

        public ResponseController(IUnitOfServices unitOfServices)
        {
            this.unitOfServices = unitOfServices;
        }

        //Need to use generic response
        [HttpPost("CreateResponse")]
        public async Task<IActionResult> CreateResponse(int mainFormId)
        {
            await unitOfServices.ResponseService.Create(mainFormId);
            return Ok("Response Created");
        }

        [HttpGet("GetAllResponses")]
        public IActionResult GetAll(int mainFormId)
        {
            return Ok(unitOfServices.ResponseService.GetAllResponses(mainFormId));
        }

        [HttpGet("GetResponse")] 
        public IActionResult Get(int id) 
        {
            return Ok(unitOfServices.ResponseService.GetResponse(id));
        }
    }
}

using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResidentManageController : ControllerBase
    {
        private readonly IResidentInforManage _repository;

        public ResidentManageController(IResidentInforManage repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ResidentInfor>> GetResidentInfor()
        {
            return await _repository.GetAllResident();
        }

        [HttpGet("id")]
        public async Task<ResidentInfor> GetResidentById(int id)
        {
            return await _repository.GetResidentById(id);
        }

        [HttpPost] 
        public async Task<ActionResult<ResidentInfor>> AddResident([FromBody] ResidentInfor resident)
        {
            try
            {
                var newResident = await _repository.CreateResident(resident);
                return Ok(newResident); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
              
            }
        }

        [HttpDelete("id")]
        public async Task<ResidentInfor> RemoveResident(int id)
        {
            return await _repository.RemoveResident(id);
        }

        [HttpPut]
        public async Task<IActionResult> ModifyResident([FromBody] ResidentDto modifiedResident)
        {
            try
            {
                var resident = await _repository.ModifyResident(modifiedResident);
                return Ok(resident); // Wrap the modified resident in OkResult
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

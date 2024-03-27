using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApartmentManageController : ControllerBase
    {
        private readonly IApartmentInforManage _repository;

        public ApartmentManageController(IApartmentInforManage repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ApartmentsInfor>> GetApartmentInfor()
        {
            return await _repository.GetAllApartment();
        }

        [HttpGet("id")]
        public async Task<ApartmentsInfor> GetApartmentById ( int id)
        {
            return await _repository.GetApartmentById(id);
        }

        [HttpPost] 
        public async Task<ActionResult<ApartmentsInfor>> AddApartments([FromBody] ApartmentsInfor apartment)
        {
            try
            {
                var new_apartment = await _repository.CreateApartment(apartment);
                return Ok(new_apartment); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
              
            }
        }

        [HttpDelete("id")]
        public async Task<ApartmentsInfor> RemoveApartment(int id)
        {
            return await _repository.RemoveApartment(id);
        }

        [HttpPut]
        public async Task<IActionResult> ModifyApartment([FromBody] ApartmentsInforDto modifiedApartment)
        {
            try
            {
                var apartment = await _repository.ModifyApartment(modifiedApartment);
                return Ok(apartment); // Wrap the modified resident in OkResult
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

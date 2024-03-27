using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelationshipManageController : ControllerBase
    {
        private readonly IRelationship _repository;

        public RelationshipManageController(IRelationship repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ApartmentsOwner>> GetRelationship()
        {
            return await _repository.GetAllRelationship();
        }


        [HttpPost] 
        public async Task<ActionResult<ApartmentsOwner>> AddRelationship([FromBody] ApartmentsOwnerDto relation)
        {
            try
            {
                var new_relation = await _repository.CreateRelationship(relation);
                return Ok(new_relation); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
              
            }
        }

        [HttpDelete("id")]
        public async Task<ApartmentsOwner> RemoveApartment(int id)
        {
            return await _repository.RemoveRelation(id);
        }

        [HttpPut]
        public async Task<IActionResult> ModifyApartment([FromBody] ApartmentsOwnerDto modifiedRelation)
        {
            try
            {
                var relation = await _repository.ModifyRelation(modifiedRelation);
                return Ok(relation); // Wrap the modified resident in OkResult
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;

        public StateController(IStateRepository stateRepository) 
        {
            _stateRepository = stateRepository;
        }

        [HttpGet]
        public IActionResult GetState()
        {
            var state = _stateRepository.GetAllStates();
            return Ok(state);
        }

        [HttpGet("{id}")]
        public IActionResult GetState(int id)
        {
            var state = _stateRepository.GetStateById(id);
            return Ok(state);
        }

        [HttpPut("{id}")]
        public IActionResult PutState(int id, [FromBody] string newValue)
        {
            _stateRepository.UpdateState(id, newValue);

            return Ok(new { message = $"Изменено: {id} = {newValue}", id, newValue });
        }
    }

}

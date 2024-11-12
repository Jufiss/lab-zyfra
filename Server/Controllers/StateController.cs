using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly string filePath = "state.txt";

        [HttpGet]
        public IActionResult GetState()
        {
            var state = ReadState(filePath);
            return Ok(state);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateState(string id, [FromBody] string newValue)
        {
            var state = ReadState(filePath);
            if (!state.ContainsKey(id))
            {
                return NotFound("Номер не найден.");
            }

            UpdateState(filePath, state, id, newValue);
            return Ok(new { message = $"Изменено: {id} = {newValue}", id, newValue });
        }

        private Dictionary<string, string> ReadState(string filePath)
        {
            var state = new Dictionary<string, string>();

            if (System.IO.File.Exists(filePath))
            {
                foreach (var line in System.IO.File.ReadAllLines(filePath))
                {
                    var parts = line.Split(" = ");
                    if (parts.Length == 2)
                    {
                        state[parts[0]] = parts[1];
                    }
                }
            }

            return state;
        }

        private void UpdateState(string filePath, Dictionary<string, string> state, string id, string newValue)
        {
            state[id] = newValue;
            WriteState(filePath, state);
        }

        private void WriteState(string filePath, Dictionary<string, string> state)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var entry in state)
                {
                    writer.WriteLine($"{entry.Key} = {entry.Value}");
                }
            }
        }
    }

}

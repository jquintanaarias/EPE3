using Microsoft.AspNetCore.Mvc;  
using ClinicaEpe3jq.Repositorio;  

namespace ClinicaEpe3jq.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]  
    public class PacienteController : ControllerBase
    {
        private readonly Models.Paciente _pacienteRepositorio; 
        // Constructor que recibe una instancia de Paciente 
        public PacienteController(Models.Paciente pacienteRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;  // Se asigna la instancia del repositorio de Paciente proporcionada al campo privado
        }

        // Endpoint (método GET) para obtener todos los pacientes que atiende la clínica
        [HttpGet]
        public async Task<IActionResult> GetAllPacientes()
        {
            return Ok(await _pacienteRepositorio.GetAllPacientes());  // Retorna una respuesta HTTP 200 (OK) con la lista de pacientes
        }

        // (método GET) para obtener todos los datos de un paciente en particular por su ID
        [HttpGet("{idPaciente}")]
        public async Task<IActionResult> GetPacienteById(int idPaciente)
        {
            return Ok(await _pacienteRepositorio.GetPacienteById(idPaciente));  // Retorna una respuesta HTTP 200 (OK) con los datos del paciente correspondiente al ID
        }

        // (método POST) para crear y guardar un nuevo paciente
        [HttpPost]
        public async Task<IActionResult> CreatePaciente([FromBody] Models.Paciente paciente)
        {
            if (paciente == null)
                return BadRequest();  // Retorna una respuesta HTTP 400 (Bad Request) si el cuerpo de la solicitud es nulo

            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // Retorna una respuesta HTTP 400 (Bad Request) si el modelo no es válido

            var nuevoPacienteId = await _pacienteRepositorio.InsertPaciente(paciente);  // Inserta el nuevo paciente y obtiene su ID
            return Created($"api/Paciente/{nuevoPacienteId}", nuevoPacienteId);  // Retorna una respuesta HTTP 201 (Created) con la ubicación del nuevo recurso creado
        }

        //  (método PUT) para editar y guardar cambios en un paciente seleccionado por su ID
        [HttpPut("{idPaciente}")]
        public async Task<IActionResult> UpdatePaciente(int idPaciente, [FromBody] Models.Paciente paciente)
        {
            if (paciente == null || idPaciente != paciente.IdPaciente)
                return BadRequest();  // Retorna una respuesta HTTP 400 (Bad Request) si el cuerpo de la solicitud es nulo o si el ID no coincide

            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // Retorna una respuesta HTTP 400 (Bad Request) si el modelo no es válido

            await _pacienteRepositorio.UpdatePaciente(paciente);  // Actualiza el paciente en el repositorio
            return NoContent();  // Retorna una respuesta HTTP 204 (No Content) indicando que la operación fue exitosa
        }

        // (método DELETE) para eliminar un paciente seleccionado por su ID
        [HttpDelete("{idPaciente}")]
        public async Task<IActionResult> DeletePaciente(int idPaciente)
        {
            await _pacienteRepositorio.DeletePaciente(idPaciente);  // Elimina el paciente del repositorio
            return NoContent();  // Retorna una respuesta HTTP 204 (No Content) indicando que la operación fue exitosa
        }
    }
}
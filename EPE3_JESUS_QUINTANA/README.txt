PROFESOR AQUI ESTA MI EPE 3 NO ALCANCE A COPIAR TODOS LOS CODIGOS PERO HICE LO QUE MEJOR PUDE.



//CONTROLLER

using Microsoft.AspNetCore.Mvc;  
using System.Collections.Generic;  
using System.Threading.Tasks;  
using ClinicaEpe3jq.Models;  

namespace ClinicaEpe3jq.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly Medico _medicoRepositorio;  // Instancia del repositorio de datos para el controller Medico

        // Constructor que recibe una instancia de Medico
        public MedicoController(Medico medicoRepositorio)
        {
            _medicoRepositorio = medicoRepositorio;  // Asigna el repositorio de Medico proporcionado al campo privado
        }

        // metodo get para obtener todos los médicos
        [HttpGet]
        public async Task<IActionResult> GetAllMedicos()
        {
            return Ok(await _medicoRepositorio.GetAllMedicos());  // Retorna una respuesta HTTP 200 (OK) con la lista de médicos
        }

        // metodoget para obtener médico por su ID
        [HttpGet("{idMedico}")]
        public async Task<IActionResult> GetMedicoById(int idMedico)
        {
            return Ok(await _medicoRepositorio.GetMedicoById(idMedico));  // Retorna una respuesta HTTP 200 (OK) con el médico correspondiente al ID
        }

        // motodopost para crear un nuevo médico
        [HttpPost]
        public async Task<IActionResult> CreatePaciente([FromBody] Medico medico)
        {
            if (medico == null)
                return BadRequest();  // Retorna una respuesta HTTP 400 (Bad Request) si la solicitud es nulo

            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // Retorna una respuesta HTTP 400 (Bad Request) si el modelo no es válido

            var nuevoMedicoId = await _medicoRepositorio.InsertMedico(medico);  // agrega ujn nuevo médico y muestra ID
            return Created($"api/Medico/{nuevoMedicoId}", nuevoMedicoId);  // Retorna una respuesta HTTP 201 (Created) con la ubicación del nuevo recurso creado
        }

        // Endpoint para actualizar un médico por su ID
        [HttpPut("{idMedico}")]
        public async Task<IActionResult> UpdateMedico(int idMedico, [FromBody] Medico medico)
        {
            if (medico == null || idMedico != medico.IdMedico)
                return BadRequest();  // Retorna una respuesta HTTP 400 (Bad Request) si el cuerpo de la solicitud es nulo o si el ID no coincide

            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // Retorna una respuesta HTTP 400 (Bad Request) si no es válido

            await _medicoRepositorio.UpdateMedico(medico);  // Actualiza el médico en el repositorio
            return NoContent();  // Retorna respuesta HTTP 204 (No Content) indicando la operación fue exitosa
        }

        // metodo para eliminar un médico por su ID
        [HttpDelete("{idMedico}")]
        public async Task<IActionResult> DeleteMedico(int idMedico)
        {
            await _medicoRepositorio.DeleteMedico(idMedico);  // aqui elimina el médico del repositorio
            return NoContent();  // Retorna una respuesta 204 (No Content) indicando la operación fue exitosa
        }
    }
}

/////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

CONTROLLER

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

///////////////////////////////////////////////////////////////7
/////////////////////////////////////////////////////////////7


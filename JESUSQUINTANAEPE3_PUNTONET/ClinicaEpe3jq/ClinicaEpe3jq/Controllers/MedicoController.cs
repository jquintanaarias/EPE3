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
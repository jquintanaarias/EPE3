using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaEpe3jq.Models;
using ClinicaEpe3jq.Repositorio;


namespace ClinicaEpe3jq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly Reserva _reservaRepositorio;

        public ReservaController(Reserva reservaRepositorio)
        {
            _reservaRepositorio = reservaRepositorio; // Asigna la instancia del repositorio de Reserva
        }

        // Método GET para listar todas las reservas
        [HttpGet]
        public async Task<IActionResult> GetAllReservas()
        {
            return Ok(await _reservaRepositorio.GetAllReservas());
        }

        // Método GET para obtener los detalles de una reserva en particular
        [HttpGet("{idReserva}")]
        public async Task<IActionResult> GetReservaById(int idReserva)
        {
            return Ok(await _reservaRepositorio.GetReservaById(idReserva));
        }

        // Método POST para crear y guardar una nueva reserva
        [HttpPost]
        public async Task<IActionResult> CreateReserva([FromBody] Reserva reserva)
        {
            if (reserva == null)
                return BadRequest();// Retorna una respuesta HTTP 400 (Bad Request) si el cuerpo de la solicitud es nulo

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Retorna una respuesta HTTP 400 (Bad Request) si el modelo no es válido

            var nuevaReservaId = await _reservaRepositorio.InsertReserva(reserva);
            return Created($"api/Reserva/{nuevaReservaId}", nuevaReservaId);
        }

        // Método PUT para editar y guardar cambios en una reserva seleccionada
        [HttpPut("{idReserva}")]
        public async Task<IActionResult> UpdateReserva(int idReserva, [FromBody] Reserva reserva)
        {
            if (reserva == null || idReserva != reserva.IdReserva)
                return BadRequest(); // Retorna una respuesta HTTP 400 (Bad Request) si el cuerpo de la solicitud es nulo o si el ID no coincide

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Retorna una respuesta HTTP 400 (Bad Request) si el modelo no es válido

            await _reservaRepositorio.UpdateReserva(reserva); // Actualiza la reserva en el repositorio
            return NoContent();// Retorna una respuesta HTTP 204 (No Content) indicando que la operación fue exitosa
        }

        // Método DELETE para eliminar una reserva seleccionada
        [HttpDelete("{idReserva}")]
        public async Task<IActionResult> DeleteReserva(int idReserva)
        {
            await _reservaRepositorio.DeleteReserva(idReserva);
            return NoContent();
        }
    }
}

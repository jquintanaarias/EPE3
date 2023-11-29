using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ClinicaEpe3jq.Models;
using ClinicaEpe3jq.Controllers;

namespace ClinicaEpe3jq.Repositorio
{
    public class ReservaRepositorio
    {
        private readonly string connectionString; // Almacena la cadena de conexión a la base de datos

        // Constructor que inicializa el repositorio con la cadena de conexión proporcionada
        public ReservaRepositorio(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Método para obtener todas las reservas desde la base de datos
        public async Task<List<Reserva>> GetAllReservas()
        {
            // Lista que almacenará las reservas recuperadas
            List<Reserva> reservas = new List<Reserva>();

            // Utiliza la conexión a la base de datos para ejecutar una consulta SELECT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Reserva";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    // Lee cada fila del resultado y crea objetos Reserva
                    while (await reader.ReadAsync())
                    {
                        Reserva reserva = new Reserva
                        {
                            IdReserva = Convert.ToInt32(reader["idReserva"]),
                            Especialidad = reader["Especialidad"].ToString(),
                            DiaReserva = Convert.ToDateTime(reader["DiaReserva"]),
                            Paciente_idPaciente = Convert.ToInt32(reader["Paciente_idPaciente"])
                        };
                        reservas.Add(reserva);
                    }
                }
            }

            return reservas; // Devuelve la lista de reservas
        }

        // Método para obtener los detalles de una reserva por su ID desde la base de datos
        public async Task<Reserva> GetReservaById(int idReserva)
        {
            // Utiliza la conexión a la base de datos para ejecutar una consulta SELECT con un parámetro
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Reserva WHERE idReserva = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idReserva);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Lee la primera fila del resultado y crea un objeto Reserva
                        if (await reader.ReadAsync())
                        {
                            Reserva reserva = new Reserva
                            {
                                IdReserva = Convert.ToInt32(reader["idReserva"]),
                                Especialidad = reader["Especialidad"].ToString(),
                                DiaReserva = Convert.ToDateTime(reader["DiaReserva"]),
                                Paciente_idPaciente = Convert.ToInt32(reader["Paciente_idPaciente"])
                            };
                            return reserva;
                        }
                    }
                }
            }

            return null; // Devuelve null si la reserva no se encuentra
        }

        // Método para insertar una nueva reserva en la base de datos
        public async Task<int> InsertReserva(Reserva reserva)
        {
            // Utiliza la conexión a la base de datos para ejecutar una consulta INSERT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Reserva (Especialidad, DiaReserva, Paciente_idPaciente) " +
                               "VALUES (@especialidad, @diaReserva, @idPaciente); SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Asigna los parámetros de la consulta con los valores de la reserva
                    command.Parameters.AddWithValue("@especialidad", reserva.Especialidad);
                    command.Parameters.AddWithValue("@diaReserva", reserva.DiaReserva);
                    command.Parameters.AddWithValue("@idPaciente", reserva.Paciente_idPaciente);

                    // Ejecuta la consulta y devuelve el ID de la nueva reserva
                    int nuevaReservaId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return nuevaReservaId;
                }
            }
        }

        // Método para actualizar la información de una reserva en la base de datos
        public async Task UpdateReserva(Reserva reserva)
        {
            // Utiliza la conexión a la base de datos para ejecutar una consulta UPDATE
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Reserva SET Especialidad = @especialidad, DiaReserva = @diaReserva, Paciente_idPaciente = @idPaciente " +
                               "WHERE idReserva = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Asigna los parámetros de la consulta con los valores actualizados de la reserva
                    command.Parameters.AddWithValue("@especialidad", reserva.Especialidad);
                    command.Parameters.AddWithValue("@diaReserva", reserva.DiaReserva);
                    command.Parameters.AddWithValue("@idPaciente", reserva.Paciente_idPaciente);
                    command.Parameters.AddWithValue("@id", reserva.IdReserva);

                    // Ejecuta la consulta
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para eliminar una reserva de la base de datos por su ID
        public async Task DeleteReserva(int idReserva)
        {
            // Utiliza la conexión a la base de datos para ejecutar una consulta DELETE
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Reserva WHERE idReserva = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Asigna el parámetro de la consulta con el ID de la reserva a eliminar
                    command.Parameters.AddWithValue("@id", idReserva);

                    // Ejecuta la consulta
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

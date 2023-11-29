using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ClinicaEpe3jq.Models;
using ClinicaEpe3jq.Controllers;
using MySqlConnector;

namespace ClinicaEpe3jq.Repositorio
{
    public class PacienteRepositorio
    {
        private readonly string connectionString; // Almacena la cadena de conexión a la base de datos

        // Constructor que inicializa el repositorio con la cadena de conexión proporcionada
        public PacienteRepositorio(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Método para obtener todos los pacientes desde la base de datos
        public async Task<List<Paciente>> GetAllPacientes()
        {
            // Lista que almacenará los pacientes recuperados
            List<Paciente> pacientes = new List<Paciente>();

            // Utiliza la conexión a la base de datos para ejecutar una consulta SELECT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Paciente";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    // Lee cada fila del resultado y crea objetos Paciente
                    while (await reader.ReadAsync())
                    {
                        Paciente paciente = new Paciente
                        {
                            IdPaciente = Convert.ToInt32(reader["idPaciente"]),
                            NombrePac = reader["NombrePac"].ToString(),
                            ApellidoPac = reader["ApellidoPac"].ToString(),
                            RunPac = reader["RunPac"].ToString(),
                            Nacionalidad = reader["Nacionalidad"].ToString(),
                            Visa = reader["Visa"].ToString(),
                            Genero = reader["Genero"].ToString(),
                            SintomasPac = reader["SintomasPac"].ToString(),
                            Medico_idMedico = Convert.ToInt32(reader["Medico_idMedico"])
                        };
                        pacientes.Add(paciente);
                    }
                }
            }

            return pacientes; // Devuelve la lista de pacientes
        }

        // Método para obtener los detalles de un paciente por su ID desde la base de datos
        public async Task<Paciente> GetPacienteById(int idPaciente)
        {
            // Utiliza la conexión a la base de datos para ejecutar una consulta SELECT con un parámetro
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Paciente WHERE idPaciente = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idPaciente);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Lee la primera fila del resultado y crea un objeto Paciente
                        if (await reader.ReadAsync())
                        {
                            Paciente paciente = new Paciente
                            {
                                IdPaciente = Convert.ToInt32(reader["idPaciente"]),
                                NombrePac = reader["NombrePac"].ToString(),
                                ApellidoPac = reader["ApellidoPac"].ToString(),
                                RunPac = reader["RunPac"].ToString(),
                                Nacionalidad = reader["Nacionalidad"].ToString(),
                                Visa = reader["Visa"].ToString(),
                                Genero = reader["Genero"].ToString(),
                                SintomasPac = reader["SintomasPac"].ToString(),
                                Medico_idMedico = Convert.ToInt32(reader["Medico_idMedico"])
                            };
                            return paciente;
                        }
                    }
                }
            }

            return null; // Devuelve null si el paciente no se encuentra
        }

        // Método para insertar un nuevo paciente en la base de datos
        public async Task<int> InsertPaciente(Paciente paciente)
        {
            // Utiliza la conexión a la base de datos para ejecutar una consulta INSERT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Paciente (NombrePac, ApellidoPac, RunPac, Nacionalidad, Visa, Genero, SintomasPac, Medico_idMedico) " +
                               "VALUES (@nombre, @apellido, @run, @nacionalidad, @visa, @genero, @sintomas, @idMedico); SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Asigna los parámetros de la consulta con los valores del paciente
                    command.Parameters.AddWithValue("@nombre", paciente.NombrePac);
                    command.Parameters.AddWithValue("@apellido", paciente.ApellidoPac);

                    // Ejecuta la consulta y devuelve el ID del nuevo paciente
                    int nuevoId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return nuevoId;
                }
            }
        }

        // Método para actualizar la información de un paciente en la base de datos
        public async Task UpdatePaciente(Paciente paciente)
        {
            // Utiliza la conexión a la base de datos para ejecutar una consulta UPDATE
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Paciente SET NombrePac = @nombre, ApellidoPac = @apellido, RunPac = @run, Nacionalidad = @nacionalidad, " +
                               "Visa = @visa, Genero = @genero, SintomasPac = @sintomas, Medico_idMedico = @idMedico WHERE idPaciente = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Asigna los parámetros de la consulta con los valores actualizados del paciente
                    command.Parameters.AddWithValue("@nombre", paciente.NombrePac);
                    command.Parameters.AddWithValue("@apellido", paciente.ApellidoPac);

                    // Asigna el parámetro con el ID del paciente a actualizar
                    command.Parameters.AddWithValue("@id", paciente.IdPaciente);

                    // Ejecuta la consulta
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para eliminar un paciente de la base de datos por su ID
        public async Task DeletePaciente(int idPaciente)
        {
            // Utiliza la conexión a la base de datos para ejecutar una consulta DELETE
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Paciente WHERE idPaciente = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Asigna el parámetro con el ID del paciente a eliminar
                    command.Parameters.AddWithValue("@id", idPaciente);

                    // Ejecuta la consulta
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
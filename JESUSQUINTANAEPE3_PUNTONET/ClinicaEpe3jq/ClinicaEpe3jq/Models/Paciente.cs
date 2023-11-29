
namespace ClinicaEpe3jq.Models
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string? NombrePac { get; set; }
        public string? ApellidoPac { get; set; }
        public string? RunPac { get; set; }
        public string? Nacionalidad { get; set; }
        public string? Visa { get; set; }
        public string? Genero { get; set; }
        public string? SintomasPac { get; set; }
        public int Medico_idMedico { get; set; }

        internal Task DeletePaciente(int idPaciente)
        {
            throw new NotImplementedException();
        }

        internal Task<object?> GetAllPacientes()
        {
            throw new NotImplementedException();
        }

        internal Task<object?> GetPacienteById(int idPaciente)
        {
            throw new NotImplementedException();
        }

        internal Task InsertPaciente(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        internal Task TaskInsertPaciente(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        internal Task UpdatePaciente(Paciente paciente)
        {
            throw new NotImplementedException();
        }
    }
}

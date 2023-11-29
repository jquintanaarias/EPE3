
namespace ClinicaEpe3jq.Models
{
    public class Medico
    {
        public int IdMedico { get; set; }
        public string? NombreMed { get; set; }
        public string? ApellidoMed { get; set; }
        public string? RunMed { get; set; }
        public string? Eunacom { get; set; }
        public string? NacionalidadMed { get; set; }
        public string? Especialidad { get; set; }
        public TimeSpan Horarios { get; set; }
        public int TarifaHr { get; set; }

        internal Task<object?> GetAllMedicos() // Este método devuelve una tarea (Task) que eventualmente producirá un objeto (posiblemente una lista de médicos).
        {
            throw new NotImplementedException();
        }

        internal Task<object?> GetMedicoById(int idMedico)// Este método devuelve una tarea (Task) que eventualmente producirá un objeto (posiblemente los detalles de un médico por su ID).
        {
            throw new NotImplementedException();
        }

        internal Task UpdateMedico(Medico medico)// Este método actualiza la información de un médico y devuelve una tarea (Task) que representa la finalización de la operación.
internal Task UpdateMedico(Medico medico)
        {
            throw new NotImplementedException();
        }

    }
}



namespace ClinicaEpe3jq.Models
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public string? Especialidad { get; set; }
        public DateTime DiaReserva { get; set; }
        public int Paciente_idPaciente { get; set; }

        internal Task DeleteReserva(int idReserva)
        {
            throw new NotImplementedException();
        }

        internal Task<object?> GetAllReservas()
        {
            throw new NotImplementedException();
        }

        internal Task<object?> GetReservaById(int idReserva)
        {
            throw new NotImplementedException();
        }

        internal Task InsertReserva(Reserva reserva)
        {
            throw new NotImplementedException();
        }

        internal Task UpdateReserva(Reserva reserva)
        {
            throw new NotImplementedException();
        }
    }
}

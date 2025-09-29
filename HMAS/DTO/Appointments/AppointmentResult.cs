namespace HMAS.DTO.Appointments
{
    public class AppointmentResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public AppointmentReadDTO? Appointment { get; set; }
    }
}
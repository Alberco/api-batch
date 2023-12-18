namespace DashboardBash.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public string NameProgram { get; set; }
        public bool Estado { get; set; }
        public DateTime HoraIni { get; set; }
        public DateTime HoraFin { get; set; }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
    }
}

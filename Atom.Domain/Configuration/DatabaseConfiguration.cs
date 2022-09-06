namespace Atom.Domain.Configuration
{
    public class DatabaseConfiguration
    {
        public static readonly string DatabaseSection = "Database";
        public string ConnectionString { get; set; }
    }
}

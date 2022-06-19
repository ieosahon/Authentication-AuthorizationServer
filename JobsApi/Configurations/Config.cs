namespace JobsApi.Configurations
{
    public class Config : IConfig
    {
        public bool RunDbMigrations { get; set; }
        public bool SeedDb { get; set; }
    }
}

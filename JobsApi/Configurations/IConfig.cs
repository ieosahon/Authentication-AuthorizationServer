namespace JobsApi.Configurations
{
    public interface IConfig
    {
        bool RunDbMigrations { get; set; }
        bool SeedDb { get; set; }
    }
}

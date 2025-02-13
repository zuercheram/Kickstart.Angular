namespace Kickstart.Angular.Domain.Migrations;

public class MigrationOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool IsProduction { get; set; }
    public bool Clear { get; set; }
}

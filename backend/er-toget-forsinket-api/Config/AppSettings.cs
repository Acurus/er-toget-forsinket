namespace er_toget_forsinket_api.Config;

public class AppSettings : IOptionsConfiguration
{
    public string PostgresHost { get; set; } = null!;
    public string PostgresUsername { get; set; } = null!;
    public string PostgresPassword { get; set; } = null!;

    public void ExplicitBinding(IConfiguration configuration)
    {
        PostgresHost = configuration?.GetValue<string>("postgres:host") ?? "";
        PostgresUsername = configuration?.GetValue<string>("postgres:username") ?? "";
        PostgresPassword = configuration?.GetValue<string>("postgres:password") ?? "";
    }
}

public interface IOptionsConfiguration
{
}
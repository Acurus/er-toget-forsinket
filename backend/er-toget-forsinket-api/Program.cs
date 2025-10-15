using er_toget_forsinket_api.Config;
using er_toget_forsinket_api.Endpoints;
using er_toget_forsinket_api.Repositories;
using er_toget_forsinket_api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

var erTogetForsinketOrigins = "_erTogetForsinketOrigins";
var builder = WebApplication.CreateBuilder(args);
var connectionString = Db.BuildConnectionString(builder);

builder.Services.AddOptions<AppSettings>()
    .Configure<IConfiguration>((settings, configuration) => { settings.ExplicitBinding(configuration); });
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy(erTogetForsinketOrigins,
        policy =>
        {
            policy.WithOrigins("https://er-toget-forsinket.no", "http://localhost:5174", "http://localhost:5173",
                "http://127.0.0.1:5173", "http://localhost:3000");
            policy.AllowAnyMethod();
            policy.AllowCredentials();
            policy.WithHeaders("Authorization", "Content-Type");
        });
});

builder.Services.AddDbContext<PostgresContext>(x => { x.UseNpgsql(connectionString!); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "er-toget-forsinket API", Version = "v1" });
});


var columnWriters = new Dictionary<string, ColumnWriterBase>
{
    { "message", new RenderedMessageColumnWriter() },
    { "message_template", new MessageTemplateColumnWriter() },
    { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
    { "raise_date", new TimestampColumnWriter() },
    { "exception", new ExceptionColumnWriter() },
    { "properties", new LogEventSerializedColumnWriter() },
    { "props_test", new PropertiesColumnWriter() }
};
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()

    // Console logs everything
    .WriteTo.Console(LogEventLevel.Information)

    // PostgreSQL logs only your namespace
    .WriteTo.PostgreSQL(
        connectionString,
        schemaName: "logs",
        tableName: "er-toget-forsinket-backend",
        columnOptions: columnWriters,
        needAutoCreateTable: false)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<ITrainDelaysService, TrainDelaysService>();

var app = builder.Build();
app.UseCors(erTogetForsinketOrigins);
app.UseSwagger();
app.UseSwaggerUI();


StatisticsEndpoints.RegisterEndpoints(app);
WebhookEndpoints.RegisterEndpoints(app);

app.Run();

internal class Db
{
    public static string BuildConnectionString(WebApplicationBuilder builder)
    {
        var host = builder.Configuration["POSTGRES_HOST"];
        var database = builder.Configuration["POSTGRES_DATABASE"];
        var username = builder.Configuration["POSTGRES_USERNAME"];
        var password = builder.Configuration["POSTGRES_PASSWORD"];
        if (host is null) throw new Exception("Host is null");

        if (database is null) throw new Exception("database is null");

        if (username is null) throw new Exception("username is null");

        if (password is null) throw new Exception("password is null");

        return $"Host={host}; Database={database}; Username={username}; Password={password};";
    }
}
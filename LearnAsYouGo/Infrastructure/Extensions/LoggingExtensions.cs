using Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace Infrastructure.Extensions;

public static class LoggingExtensions
{
    public static WebApplicationBuilder AddStructuredLogging(this WebApplicationBuilder builder)
    {
        LoggingSettings settings = builder.Configuration
            .GetSection(LoggingSettings.SectionName)
            .Get<LoggingSettings>()
            ?? throw new InvalidOperationException($"'{LoggingSettings.SectionName}' configuration section is missing.");

        IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
        {
            { "Message",         new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
            { "MessageTemplate", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
            { "Level",           new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
            { "TimeStamp",       new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
            { "Exception",       new ExceptionColumnWriter(NpgsqlDbType.Text) },
            { "Properties",      new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
        };

        builder.Host.UseSerilog((ctx, cfg) =>
        {
            cfg
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()

                // Console: всё уровни (Information и выше)
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")

                // PostgreSQL: только Warning и выше — не засоряем БД
                .WriteTo.PostgreSQL(
                    connectionString: settings.ConnectionString,
                    tableName: settings.TableName,
                    columnOptions: columnWriters,
                    needAutoCreateTable: true,
                    restrictedToMinimumLevel: LogEventLevel.Warning)

                // Минимальный глобальный уровень — переопределяется из appsettings
                .ReadFrom.Configuration(ctx.Configuration);
        });

        return builder;
    }
}

using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using rss_api.Contexts;
using rss_api.ControllerHandlers;
using rss_api.Mapping;
using rss_api.Services.Cache;
using rss_api.Services.Entities;
using rss_api.Services.Hangfire;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// logger:
Log.Logger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateLogger();

builder.Services.AddMapster();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IPushService, PushService>();
builder.Services.AddScoped<IPullService, PullService>();

builder.Services.AddScoped<IHangFireService, HangFireService>();

builder.Services.AddHttpClient();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

var connectionStringPostgresql = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddDbContext<RssDbContext>(options =>
{
	options.UseNpgsql(connectionStringPostgresql);
});

// hangfire START:
builder.Services.AddHangfire(config =>
{
	// Hangfire should be in a database before you connect to it.
	var connectionString = builder.Configuration.GetConnectionString("Connection");
	config.UsePostgreSqlStorage(connectionString);
});
builder.Services.AddHangfireServer();
// hangfire END:

var app = builder.Build();

// AddMapster the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

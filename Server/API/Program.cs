using API.Configuration;
using API.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConnectToDatabase(builder.Configuration);
builder.Services.RegisterCustomDependencies();
builder.Services.RegisterHostedServices();
builder.Services.RegisterCorsPolicy();

builder.Services.AddSignalR();

var app = builder.Build();

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<AirportHub>("/airportHub");

app.UseCors();

app.SetupAirportEventDrivenEngine();
app.SetupSignalR();

app.Run();

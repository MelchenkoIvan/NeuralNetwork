
using NeuralNetworkAPI.Endpoints.Authorization;
using NeuralNetworkAPI.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddServices()
    .AddDbContext(builder);
builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
);
app.UseMiddleware<BasicAuthMiddleware>();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();

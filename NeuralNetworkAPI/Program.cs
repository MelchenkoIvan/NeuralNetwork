global using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using NeuralNetworkAPI.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddCookieAuth(validFor: TimeSpan.FromMinutes(10));
builder.Services.AddSwaggerDoc(settings =>
{
    settings.Title = "NN API";
    settings.Version = "NN V1";
});
builder.Services
    .AddCore()
    .AddServices()
    .AddDbContext(builder);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}
app.Run();

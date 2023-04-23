global using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using NeuralNetworkAPI.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddCookieAuth(validFor: TimeSpan.FromDays(1));
builder.Services.AddSwaggerDoc(settings =>
{
    settings.Title = "NN API";
    settings.Version = "NN V1";
});

builder.Services
    .AddCors()
    .AddCore()
    .AddServices()
    .AddDbContext(builder);

var app = builder.Build();
app.UseCors(
    options => options.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod()
);
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}
app.Run();

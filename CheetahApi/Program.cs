using CheetahApi.Extensions;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddClassDependencies();
builder.Services.AddLiteDb(builder.Configuration);
builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p => { p.AllowAnyMethod(); p.AllowAnyOrigin(); p.AllowAnyHeader(); });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()
        ?.Error;
    var response = new { error = exception?.Message };
    await context.Response.WriteAsJsonAsync(response);
}));
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Run();

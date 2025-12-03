using ArcheryAcademy.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Register all services  for extension method
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// use Swagger

var enableSwagger = app.Configuration.GetValue<bool>("EnableSwagger");
if (app.Environment.IsDevelopment() || enableSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Routing
app.UseRouting();
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", time = DateTime.UtcNow }));
// Middlewares personalizados 
//app.UseMiddleware<ErrorHandlingMiddleware>();           
//app.UseMiddleware<ParameterValidationMiddleware>();

// Autenticación y Autorización
//app.UseAuthentication();
//app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();
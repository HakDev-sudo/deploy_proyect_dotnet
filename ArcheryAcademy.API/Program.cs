using ArcheryAcademy.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Register all services  for extension method
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// use Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

// Routing
app.UseRouting();

// Middlewares personalizados 
//app.UseMiddleware<ErrorHandlingMiddleware>();           
//app.UseMiddleware<ParameterValidationMiddleware>();

// Autenticación y Autorización
//app.UseAuthentication();
//app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();
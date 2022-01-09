using System.Text.Json.Serialization;
using Control_Dossier.Data;
var builder = WebApplication.CreateBuilder(args);
ConfigureMvc(builder);
ConfigureServices(builder);
var app = builder.Build();
app.MapControllers();
app.Run();
void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<AppDbContext>();
}

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(option => { option.SuppressModelStateInvalidFilter = true; })
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        });
        
}

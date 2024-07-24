using BusinessLogic.Infrastructure.SignalR;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Presentation.Extensions;
using Presentation.Filters;
using Presentation.Utils;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    options.Filters.Add(new ValidateModelAttribute());
});
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();


    app.UseSwagger()
        .UseSwaggerUI
            (options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sakanat.API v1");
                options.RoutePrefix = "swagger";
            });

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoint =>
    {
        endpoint.MapHub<SignalServer>("/signalServer");
        endpoint.MapControllers();
    }
);
app.Run();

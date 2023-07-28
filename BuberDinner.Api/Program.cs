using BuberDinner.Api.Errors;
using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();//(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddSingleton<ProblemDetailsFactory,BuberDinnerProblemDetailsFactory>();
}
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();-

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandlingMiddleware>();

    // Use Error Controller 
    //  app.UseExceptionHandler("/error1"); 
     app.UseExceptionHandler("/error"); 

    // Direct map to the rout 
    // app.Map("/error", (HttpContext httpcontext)=>{
    //     Exception? exception = httpcontext.Features.Get<IExceptionHandlerFeature>()?.Error;
    //     // add new property to the exception return object 
    //     IDictionary<string, object?>? extensions = new Dictionary<string , object?>();
    //     extensions.Add("newProb","new ProbValue");
        
    //     return Results.Problem(extensions:extensions);

    // });
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }


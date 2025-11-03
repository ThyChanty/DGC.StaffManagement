using DGC.StaffManagement.Shared.Commons;
using MediatR;
using Microsoft.OpenApi.Models;
using DGC.StaffManagement.WebApi.Middlewares;
using FluentValidation;
using DGC.StaffManagement.Shared.Validator;
using DGC.StaffManagement.Infrastructure.ServiceCollectionExtensions;
using DGC.StaffManagement.Application.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);


//------------------ http Request ------------//
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();


builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    builder.Services.AddValidatorsFromAssemblies(
        AppDomain.CurrentDomain.GetAssemblies(),
        ServiceLifetime.Scoped,
        result => typeof(IRequestCommandValidator).IsAssignableFrom(result.ValidatorType));

builder.Services.RegisterApplicationServices();
builder.Services.RegisterPersistenceStorage();


// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// -------------- Force to response snakes case -------------- //
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.IncludeFields = true; });



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Enable Annotation of Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Staff Management API",
        Version = "v1",
        Description = "Staff Management",
        Contact = new OpenApiContact
        {
            Name = "taff Management API"
        }
    });

});
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Staff Management API v1"); });
}

app.UseRouting();


app.MapControllers();

//app.UseHttpsRedirection();

await app.RunAsync();
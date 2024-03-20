using Microsoft.OpenApi.Models;
using OnboardingSIGDB1.Data;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddValidators();
builder.Services.AddControllers();
builder.Services.AddMediator();
builder.Services.AddServices();
builder.Services.AddAutoMapping();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "OnboardingSIGDB1"
        });
    }
);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1")
    );
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();
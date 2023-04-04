using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NoruBanner.Api.Services;
using NoruBanner.Infrastructure;
using NoruBanner.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserActionRepository, UserActionRepository>();
builder.Services.AddScoped<IUserActionService, UserActionService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NoruBanner", Version = "v1" });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "AppXml.xml");
    c.IncludeXmlComments(filePath);
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<NoruBannerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NoruBannerDb"),
        x => x.MigrationsAssembly("NoruBanner.Infrastructure")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var s = scope.ServiceProvider;
    var c = s.GetRequiredService<NoruBannerContext>();
    await TestData.CreateDataAsync(c);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAllHeaders");
app.Run();

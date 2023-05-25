using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using SpravaPenezDeti.Controllers;

var builder = WebApplication.CreateBuilder(args);


var stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.ConnectionString = builder.Configuration.GetConnectionString("SqlConnString");
stringBuilder.UserID = builder.Configuration["UserId"];
stringBuilder.Password = builder.Configuration["Password"];
// Add services to the container.
builder.Services.AddControllersWithViews().AddControllersAsServices().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
    
builder.Services.AddScoped<IRepo<Dite>, Repo<Dite>>();
builder.Services.AddScoped<IRepo<Ucet>, Repo<Ucet>>();
builder.Services.AddScoped<IRepo<Pohyb>, Repo<Pohyb>>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(stringBuilder.ConnectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options => options.AddPolicy(name: "AllowAny", policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); }));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    //options.ExcludeXmlComments();
    options.IgnoreObsoleteActions();
    options.IgnoreObsoleteProperties();
    //options.IgnoreNonPublicProperties();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
    options.RoutePrefix = "api";
});

app.MapControllers();
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});
app.Run();

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SpravaPenezDeti.Controllers;

var builder = WebApplication.CreateBuilder(args);


var stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.ConnectionString = builder.Configuration.GetConnectionString("SqlConnString");
stringBuilder.UserID = builder.Configuration["UserId"];
stringBuilder.Password = builder.Configuration["Password"];
// Add services to the container.
builder.Services.AddControllersWithViews().AddControllersAsServices().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddScoped<IRepo<Dite>, Repo<Dite>>();
builder.Services.AddScoped<IRepo<Ucet>, Repo<Ucet>>();
builder.Services.AddScoped<IRepo<Pohyb>, Repo<Pohyb>>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(stringBuilder.ConnectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();

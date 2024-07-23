using JobsCandidateRecords.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddCors();

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequiredLength = 5;   
        options.Password.RequireNonAlphanumeric = false;   
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false; 
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI().
    AddDefaultTokenProviders();

builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

builder.Services.AddSession();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty;
});

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

app.UseSession();

app.Run();





//Create roles and admin and order statuses
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    try
//    {
//        var context = services.GetRequiredService<ApplicationDbContext>();
//        await DBSeeder.SeedDefaultData(scope.ServiceProvider, context);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred seeding the DB.");
//    }
//}

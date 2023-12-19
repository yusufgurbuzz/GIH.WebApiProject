using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;
using GIH.Services;
using GIH.Services.Helper;
using GIH.WebApi.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurePostgreSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddScoped<IPersonValidateService,PersonValidateService>();
builder.Services.AddScoped<IRestaurantValidateService,RestaurantValidateService>();
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
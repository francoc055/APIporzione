using backendAPIPorzione;
using backendAPIPorzione.Datos;
using backendAPIPorzione.Repository;
using backendAPIPorzione.Repository.IRepository;
using backendAPIPorzione.Services.IService;
using backendAPIPorzione.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PorzioneapiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaConexion"));
});

//inyecto repository
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IDetalleRepository, DetalleRepository>();

//inyecto servicio mapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();


var key = builder.Configuration.GetValue<string>("JwtSettings:key"); //accedo al valor del .json
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    /*Dentro de la acción de configuración, se establece JwtBearerDefaults.AuthenticationScheme 
     como el esquema predeterminado tanto para la autenticación como para el desafío 
     (DefaultAuthenticateScheme y DefaultChallengeScheme). Esto significa que la autenticación se basará en tokens JWT.*/
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    /*Esta configuración indica si se requiere que las solicitudes sean HTTPS (conexión segura). 
     En este caso, se establece en false, lo que significa que no es obligatorio que las solicitudes sean HTTPS.*/
    config.RequireHttpsMetadata = false;

    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false, //no nos interesa quien solicita
        ValidateAudience = false, //no nos interesa desde donde esta solicitando el usuario
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero //no debe existir ningun tipo de desviacion del reloj en cuanto al tiempo de vida del token
    };

});





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

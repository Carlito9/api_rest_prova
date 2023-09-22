using apiRestProva.Db;
using apiRestProva.Entities;
using apiRestProva.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;
builder.Services.AddSingleton(Configuration);
//creo la dipendenza per jwt settings

//ottengo la sezione del file json chiamata JWTsettings
var section = Configuration.GetSection(nameof(JwtSettings));
//estraggo i valori e li inserisco in app options
var settings = section.Get<JwtSettings>();
//dep inj fatta apposta per i file options
var jwtSettings = builder.Services.Configure<JwtSettings>(section);
// Add services to the container.
//aggiungi chiamate httpclient
builder.Services.AddHttpClient<HttpClientService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new ()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ChiaveSegretaSuperSicura")),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
builder.Services.AddDbContext<ProvaDbContext>();
builder.Services.AddScoped<IArticleService,ArticleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

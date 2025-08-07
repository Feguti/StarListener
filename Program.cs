using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using STARListener.Api.Data;
using STARListener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adiciona o DbContext ao container de servi�os.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura��o para utiliza��o do Jira
builder.Services.AddHttpClient("Jira", client =>
{
    var jiraConfig = builder.Configuration.GetSection("Jira");
    var baseUrl = jiraConfig["BaseUrl"];
    var email = jiraConfig["Email"];
    var apiToken = jiraConfig["ApiToken"];

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

    // Configura a autentica��o Basic com email e token de API
    var authenticationString = $"{email}:{apiToken}";
    var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
});

builder.Services.AddScoped<JiraService>();

builder.Services.AddScoped<PriorizacaoService>();

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

using ALBackend.Persistence;
using ALBackend.Setup;
using ALBackend.Setup.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SecurityServices.Inject(builder);
ServicesInjector.Inject(builder);
JwtAuthentication.SetupJwtAuthentication(builder);

builder.Services.AddDbContext<MariaDbContext>();

const string allowedOrigins = "_allowedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowedOrigins, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowedOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await IdentityPopulation.Instance(app).PopulateAsync();
Console.WriteLine("https://localhost:7226/swagger/index.html");
await app.RunAsync();
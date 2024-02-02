using MediatR;
using WordFinder.Application.Interfaces;
using WordFinder.Application.Services;

const string APPLICATION_ASSEMBLY_NAME = "WordFinder.Application";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var applicationAssembly = AppDomain.CurrentDomain.Load(APPLICATION_ASSEMBLY_NAME);
builder.Services.AddControllers();
builder.Services.AddScoped<IWordFinderService, WordFinderService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(applicationAssembly);
builder.Services.AddSwaggerGen();

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

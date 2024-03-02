using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Application.MappingProfiles;
using Lumina.Authentification.Application.UtilisateurFeature.Commands;
using Lumina.Authentification.Application.UtilisateurFeature.Queries;
using Lumina.Authentification.Domain.Entities;
using Lumina.Authentification.Infrastructure;
using Lumina.Authentification.Infrastructure.Persistence;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LuminaContextConnection") ?? throw new InvalidOperationException("Connection string 'LuminaContextConnection' not found.");

builder.Services.AddDbContext<LuminaContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LuminaContext>();
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization();
builder.Services.AddRazorPages();
//builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<LuminaContext>().AddApiEndpoints();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options=> {
    options.AddPolicy("all", builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()); 
    }) ;
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(UtilisateurProfile));
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//builder.Services.AddTransient<IRequestHandler<CreateUtilisateurCommand, int>, CreateUtilisateurCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapRazorPages();
//app.UseAuthentication();
app.MapControllers();

app.Run();

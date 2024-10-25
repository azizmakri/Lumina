using Lumina.Authentification.Application.Commons.Email;
using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Domain.Entities;
using Lumina.Authentification.Infrastructure;
using Lumina.Authentification.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddIdentity<User,IdentityRole> ()
    .AddEntityFrameworkStores<LuminaContext>().AddDefaultTokenProviders();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options=> {
    options.AddPolicy("all", builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()); 
    }) ;
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHostedService<RabbitMQConsumer>();
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("all");
app.MapControllers();
app.UseDeveloperExceptionPage();
app.Run();

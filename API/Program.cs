using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.RepositoriesContexts;
using Infrastructure.RepositoryContexts;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

Logger.Initialize(builder.Services);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins().AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddDbContext<DbEntityFrameworkContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Host.ConfigureLogging(l => { l.ClearProviders(); l.AddConsole(); });

builder.Services.AddScoped(typeof(IGenericPersistenceLayer<>), (typeof(GenericPersistentLayer<>)));
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddScoped<IEfRepository, EntityFrameworkRepository>();
builder.Services.AddScoped<IPersistenceLayer, PersistenceLayer>();
builder.Services.AddScoped<IMemoryRepository, MemoryRepository>();
builder.Services.AddScoped<IDaRepository, DapperRepository>();

DbEntityFrameworkContext.OnStartUp(builder.Services);

builder.Services.AddSingleton<DbDapperContext>();
builder.Services.AddSingleton<MemoryContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

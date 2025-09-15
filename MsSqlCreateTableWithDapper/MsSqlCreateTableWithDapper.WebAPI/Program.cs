using MsSqlCreateTableWithDapper.Bll.Absract;
using MsSqlCreateTableWithDapper.Bll.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IAdminService, AdminService>();

builder.Services.AddCors(p => p.AddPolicy("MsSqlCreateTableWithDapperCors", bb => { bb.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors("MsSqlCreateTableWithDapperWebCors");
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

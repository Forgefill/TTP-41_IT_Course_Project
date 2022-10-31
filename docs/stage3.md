[Сторінка проекту](https://github.com/Forgefill/TTP-41_IT_Course_Project)

# Rest web-сервіси
# Розробка OpenApi Specification
# Asp.Net Web Api

Створимо Asp.Net Web Api проект та додамо в нього підтримку OpenApi. Також зареєструємо DBManager в DI контейнері.
```C#
using DAL;
using DAL.DBFileManagers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<DBManager>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```


[Сторінка проекту](https://github.com/Forgefill/TTP-41_IT_Course_Project)

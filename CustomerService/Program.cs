var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapPost("/newcustomer", async (Customer request) =>
{
    await File.AppendAllTextAsync("result.log", $"{DateTime.UtcNow}->{request.name}\n");
    return Results.Ok(new { code = 200, message = "Successful", content = new Customer(request.name, request.lastname, request.age) });
})
.WithName("newcustomer");

app.Run();

internal record Customer(string name, string lastname, int age)
{

}
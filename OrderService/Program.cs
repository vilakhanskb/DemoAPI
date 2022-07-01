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

app.MapPost("/openorder", async (OpenOrders request) =>
{
    await Task.Delay(TimeSpan.FromSeconds(10));
    await File.AppendAllTextAsync("result.log", $"{DateTime.UtcNow}->{request.id}\n");
    return Results.Ok(new { code = 200, message = "Successful", content = new OpenOrders(request.id, request.datetime, request.item, request.amount, request.price) });
})
.WithName("openorder");

app.Run();

internal record OpenOrders(string id, string datetime, string item, int amount, int price)
{

}
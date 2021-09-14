while (!System.Diagnostics.Debugger.IsAttached)
{
    System.Console.WriteLine($"Waiting to attach on ${Environment.ProcessId}");
    System.Threading.Thread.Sleep(1000);
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = builder.Environment.ApplicationName, Version = "v1" });
});

var app = builder.Build();

app.MapGet("/number/{value:int}", (int value) => Results.Text($"value: {value}"));
app.MapGet("/stringa/{value:minlength(8)}", (string value) => Results.Text(value));
app.MapGet("/stringb/{value:maxlength(12)}", (string value) => Results.Text(value));
app.MapGet("/stringc/{value:length(8)}", (string value) => Results.Text(value));
app.MapGet("/stringd/{value:length(8, 12)}", (string value) => Results.Text(value));
app.MapGet("/stringe/{value:alpha}", (string value) => Results.Text(value));
app.MapGet("/status/{code:range(200, 599)}", (int code) => Results.StatusCode(code));

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.EnableTryItOutByDefault();
    options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1");
});

app.Run();
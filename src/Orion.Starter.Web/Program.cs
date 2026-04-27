using Orion.Starter.ServiceDefaults;
using Orion.Starter.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<OrderApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:ApiBaseUrl"] ?? "https://localhost:5001");
});

var app = builder.Build();
app.MapGet("/", () => Results.Text("Orion Aspire Starter Web"));

app.MapDefaultEndpoints();

app.Run();

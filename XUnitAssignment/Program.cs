using ServiceContracts;
using Services;
using XUnitAssignment.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStockService, StockService>();
var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();

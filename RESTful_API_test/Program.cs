using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// 加入必要的服務
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
// Add services to the container.
// 載入配置
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*", // 所有路徑
            Limit = 5,      // 每個時間窗口允許的最大請求數
            Period = "10s"  // 時間窗口長度（10秒）
        }
    };
});
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

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

// 套用 IP 速率限制中介軟體
app.UseIpRateLimiting();

app.MapControllers();

app.Run();
